using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common;
using Common.Enums;
using Common.POCOS;

namespace FileMonger
{
    public static class FileMonger
    {
        public static bool CheckDirectory(string directory)
        {
            return Directory.GetFiles(directory, "*.resx").Length > 0;
        }

        public static IEnumerable<string> GetSubDirectories(string directory)
        {
            return Directory.GetDirectories(directory).Select(x => x.Replace($"{directory}\\", "")).ToList();
        }

        public static async Task<List<ResXEntry>> AsyncLoadResXFiles(string directory, CultureContainer culture, string oldDir)
        {
            return await Task<List<ResXEntry>>.Run(() => LoadResXFiles(directory, culture, oldDir));
        }

        internal static List<ResXEntry> LoadResXFiles(string directory, CultureContainer culture, string oldDir)
        {
            var entries = new List<ResXEntry>();
            //Get Directory Info
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            //Get Main ResX Files
            FileInfo[] mainFiles = directoryInfo.EnumerateFiles("*.resx", SearchOption.TopDirectoryOnly).ToArray();
            //Itterate through main resx files and extract information
            foreach (var file in mainFiles)
            {
                ResXResourceReader source = new ResXResourceReader(file.OpenRead()) { UseResXDataNodes = true };
                entries.AddRange(ExtractResXEntries(source, file));
            }
            if (culture.Existing)
            {
                //Get Localized Directory Info
                DirectoryInfo localizedDirectoryInfo = new DirectoryInfo($"{directory}\\{culture.Value}");
                //Get Localized ResX Files
                FileInfo[] localizedFiles = localizedDirectoryInfo.EnumerateFiles("*.resx", SearchOption.TopDirectoryOnly).ToArray();
                var localizedEntries = new List<ResXEntry>();
                foreach (var file in localizedFiles)
                {
                    ResXResourceReader source = new ResXResourceReader(file.OpenRead()) { UseResXDataNodes = true };
                    localizedEntries.AddRange(ExtractResXEntries(source, file));
                    source.Close();
                }

                foreach (var resXEntry in entries)
                {
                    var localizedEntry = localizedEntries.FirstOrDefault(x =>
                        x.SourceFile == resXEntry.SourceFile
                        && x.Key == resXEntry.Key);

                    if (localizedEntry == null)
                    {
                        resXEntry.State = State.New;
                        continue;
                    }

                    resXEntry.LocalizedComment = localizedEntry.Comment;
                    resXEntry.LocalizedValue = localizedEntry.Value;
                }
            }
            else
            {
                entries.ForEach(x => x.State = State.New);
            }
            if (!string.IsNullOrWhiteSpace(oldDir))
            {
                //Get Localized Directory Info
                DirectoryInfo oldDirectoryInfo = new DirectoryInfo(oldDir);
                //Get Localized ResX Files
                FileInfo[] oldFiles = oldDirectoryInfo.EnumerateFiles("*.resx", SearchOption.TopDirectoryOnly).ToArray();
                var oldEntries = new List<ResXEntry>();
                foreach (var file in oldFiles)
                {
                    ResXResourceReader source = new ResXResourceReader(file.OpenRead()) { UseResXDataNodes = true };
                    oldEntries.AddRange(ExtractResXEntries(source, file));
                }

                foreach (var resXEntry in entries)
                {
                    var oldEntry = oldEntries.FirstOrDefault(x =>
                        x.SourceFile == resXEntry.SourceFile
                        && x.Key == resXEntry.Key);

                    if (oldEntry == null)
                    {
                        resXEntry.State = State.New;
                    }
                    else if (resXEntry.Comment != oldEntry.Comment || resXEntry.Value != oldEntry.Value)
                        resXEntry.State = State.MasterChanged;
                }
            }

            return entries;
        }

        public static void SaveResXChanges(List<ResXEntry> entries, string directory, CultureInfo selectedCulture)
        {
            //Get updated entries
            var editedEntries = entries.Where(x => x.State == State.Updated || x.State == State.GoogleTranslated).ToList();
            //Get files requiring updates
            var resxFiles = editedEntries.Select(x => x.SourceFile).Distinct().ToList();
            //Get Localized Directory Info
            DirectoryInfo localizedDirectoryInfo = new DirectoryInfo($"{directory}\\{selectedCulture.Name}");
            //Check it directory exists, if not, create it - handles "New ResX" use case
            if (!localizedDirectoryInfo.Exists)
            {
                localizedDirectoryInfo.Create();
            }
            //Get Localized ResX Files
            FileInfo[] localizedFiles = localizedDirectoryInfo.EnumerateFiles("*.resx", SearchOption.TopDirectoryOnly).ToArray();

            //Itterate resx files and update them accordingly
            foreach (var resxFile in resxFiles)
            {
                //Get file we're trying to update
                var file = localizedFiles.FirstOrDefault(x => x.Name.Split('.')[0] == resxFile);
                //If no file is found, create a new one - handles "New ResX" use case
                if (file == null)
                {
                    //Create new ResX file
                    CreateNewResX(localizedDirectoryInfo.ToString(), selectedCulture, resxFile);
                    //Get fileinfo for new file 
                    file = localizedDirectoryInfo.EnumerateFiles("*.resx", SearchOption.TopDirectoryOnly).FirstOrDefault(x => x.Name.Split('.')[0] == resxFile);
                }
                //Edit dat bitch
                EditExistingResxFile(directory, selectedCulture, resxFile, editedEntries, file);
            }
        }

        private static void CreateNewResX(string directory, CultureInfo selectedCulture, string resxFile)
        {
            using (FileStream fs = new FileStream($"{directory}\\{resxFile}.{selectedCulture.Name}.resx", FileMode.Create))
            {
                using (ResXResourceWriter writer = new ResXResourceWriter(fs))
                {
                    writer.Generate();
                    writer.Close();
                }
            }
        }

        private static void EditExistingResxFile(string directory, CultureInfo selectedCulture, string resxFile, List<ResXEntry> editedEntries, FileInfo file)
        {
            //Use XML doc to edit file since ResXResourceWriter is unable to edit existing entries 
            XElement doc = XElement.Load($"{directory}\\{selectedCulture.Name}\\{resxFile}.{selectedCulture.Name}.resx");
            //Get xml elements
            var xmlElements = doc.Elements();
            //itterate & update
            foreach (var entry in editedEntries.Where(x => x.SourceFile == resxFile).ToList())
            {
                var parentNode =
                    xmlElements.Attributes("name")
                        .Where(x => x.Value == entry.Key)
                        .Select(x => x.Parent)
                        .FirstOrDefault();
                if (parentNode == null)
                {
                    doc.Add(CreateXMLNodeFromResXEntry(entry));
                }
                else
                {
                    var value = parentNode.Elements("value").FirstOrDefault();
                    //check if node is not null, theoretically impossible, but OCD #DealWithIt
                    if (value != null)
                        value.Value = entry.LocalizedValue;
                    //Get comment node
                    var comment = parentNode.Elements("comment").FirstOrDefault();
                    //Check that we don't try to save nulls
                    entry.LocalizedComment = string.IsNullOrWhiteSpace(entry.LocalizedComment)
                        ? ""
                        : entry.LocalizedComment;
                    //check if node is not null, very much possible
                    if (comment != null)
                        comment.Value = entry.LocalizedComment;
                    else
                    {
                        parentNode.Add(new XElement("comment", entry.LocalizedComment));
                    }
                }
            }
            //Disable readonly - precautionary measure
            ToggleFileReadOnly(file, false);

            using (FileStream fs = new FileStream($"{directory}\\{selectedCulture.Name}\\{resxFile}.{selectedCulture.Name}.resx", FileMode.Open, FileAccess.ReadWrite))
            {
                doc.Save(fs);
            }
            //Reset readonly to original setting
            ToggleFileReadOnly(file, file.IsReadOnly);
        }

        private static void ToggleFileReadOnly(FileInfo fileInfo, bool value)
        {
            if (value)
            {
                // Make the file RO
                File.SetAttributes(fileInfo.FullName, File.GetAttributes(fileInfo.FullName) | FileAttributes.ReadOnly);
            }
            else
            {
                // Make the file RW
                FileAttributes attributes = File.GetAttributes(fileInfo.FullName);
                attributes = attributes & ~FileAttributes.ReadOnly;
                File.SetAttributes(fileInfo.FullName, attributes);
            }
        }

        private static XElement CreateXMLNodeFromResXEntry(ResXEntry entry)
        {
            return new XElement("data",
                        new XAttribute("name", entry.Key),
                        new XAttribute(XNamespace.Xml + "space", "preserve"),
                        new XElement("value", entry.LocalizedValue),
                        new XElement("comment", entry.LocalizedComment));
        }

        private static IEnumerable<ResXEntry> ExtractResXEntries(ResXResourceReader source, FileInfo file)
        {
            var entries = new List<ResXEntry>();
            IDictionaryEnumerator enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var node = (ResXDataNode)enumerator.Value;
                entries.Add(new ResXEntry()
                {
                    Comment = node.Comment,
                    Key = node.Name,
                    Value = node.GetValue(new AssemblyName[] { Assembly.GetExecutingAssembly().GetName() }).ToString(),
                    SourceFile = file.Name.Split('.')[0],
                    State = State.NoChange
                });
            }
            return entries;
        }
    }
}
