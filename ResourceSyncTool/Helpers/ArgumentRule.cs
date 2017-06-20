using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace ResourceSyncTool.Helpers
{
    /// <summary>
    /// Class that represents an argument rule.
    /// </summary>
    public class ArgumentRule
    {
        #region Fields
        /// <summary>
        /// The text of the argument.
        /// </summary>
        private readonly string _text;

        /// <summary>
        /// The list of parts of the argument.
        /// </summary>
        private List<Part> _partArray = new List<Part>();

        /// <summary>
        /// The regular expression.
        /// </summary>
        private Regex _regExp;

        /// <summary>
        /// The regular expression as a string.
        /// </summary>
        private string _regExpString;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentRule"/> class.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public ArgumentRule(string argument)
        {
            // Set properties.
            this._text = argument;

            // Prepare regex.
            ParseRegExp();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ArgumentRule"/> class from being created.
        /// </summary>
        private ArgumentRule()
        {
            // Nothing else to do.
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this argument is a valid rule.
        /// </summary>
        /// <value>A value indicating whether this argument is a valid rule.</value>
        public bool IsValid
        {
            get
            {
                // Is it valid?
                return this._partArray.Count > 0;
            }
        }

        /// <summary>
        /// Gets the count of argument parts.
        /// </summary>
        /// <value>The count of argument parts.</value>
        public int Count
        {
            get
            {
                return this._partArray.Count;
            }
        }

        /// <summary>
        /// Gets the names of the argument parts.
        /// </summary>
        /// <returns>The list of argument parts names.</returns>
        public IEnumerable<string> ArgumentNames
        {
            get
            {
                return this._partArray.Select(x => x.Name).ToList();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Checks if a translation arguments matches the reference text argument.
        /// </summary>
        /// <param name="reference">The reference text to check.</param>
        /// <param name="translation">The translation text to check.</param>
        /// <returns>A value indicating whether the argument match, do not match, or match with a different count.</returns>
        public static ArgumentMatch MatchTexts(string reference, string translation)
        {
            // Sanity check.
            if (reference == null)
                throw new ArgumentNullException("reference");
            if (translation == null)
                throw new ArgumentNullException("translation");

            // Both texts do not contain arguments.
            var referenceRule = new ArgumentRule(reference);
            var translationRule = new ArgumentRule(translation);
            if (!referenceRule.IsValid && !translationRule.IsValid)
                return ArgumentMatch.Match;

            // One of the text does not contain any argument.
            if (!translationRule.IsValid && !referenceRule.IsValid)
                return ArgumentMatch.NoMatch;

            // Get the argument names of each text.
            List<string> namesReference = referenceRule.ArgumentNames.ToList();
            List<string> namesTranslation = translationRule.ArgumentNames.ToList();

            // Check count mismatch.
            if (namesReference.Count < namesTranslation.Count)
                return ArgumentMatch.NoMatch;

            // Order lists.
            namesReference = namesReference.OrderBy(name => name).ToList();
            namesTranslation = namesTranslation.OrderBy(name => name).ToList();

            // Check every argument name of translation matches reference argument names.
            bool match = namesTranslation.TrueForAll(namesReference.Contains);

            // Final decision.
            if (!match)
                return ArgumentMatch.NoMatch;
            if (namesReference.Count == namesTranslation.Count)
                return ArgumentMatch.Match;
            return ArgumentMatch.MatchInferior;
        }

        /// <summary>
        /// Checks if the specified string match with the argument rule.
        /// </summary>
        /// <param name="input">The string to match.</param>
        /// <returns>True if the input is  match, false otherwise.</returns>
        public bool IsMatch(string input)
        {
            var matches = new List<string>();
            return IsRegExpMatch(input, matches);
        }

        /// <summary>
        /// Checks if the specified part of the argument rule should be translated.
        /// </summary>
        /// <param name="partIndex">The index of the part to check.</param>
        /// <returns>True if the part should be translated, false otherwise.</returns>
        public bool IsTranslatedPart(int partIndex)
        {
            // Check sanity.
            if (partIndex >= this._partArray.Count)
                return false;

            // Should we translate it?
            return this._partArray[partIndex].Translated;
        }

        /// <summary>
        /// Try to match the specified string with that argument rule and extract the variable parts.
        /// </summary>
        /// <param name="input">String to match.</param>
        /// <returns>The list of matches.</returns>
        public IEnumerable<string> GetMatches(string input)
        {
            var matches = new List<string>();
            IsRegExpMatch(input, matches);
            return matches;
        }

        /// <summary>
        /// Replace the specified arguments in the specified string.
        /// </summary>
        /// <param name="input">The string to match.</param>
        /// <param name="argReplaceWithList">The dictionary of part names/text to replace.</param>
        /// <returns>The processed input string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one or more parameters are null.</exception>
        public string Replace(string input, Dictionary<string, string> argReplaceWithList)
        {
            // Check sanity.
            if (argReplaceWithList == null)
                throw new ArgumentNullException("argReplaceWithList", "argReplaceWithList must not be null.");

            // Check the count of Substitution string.
            if (argReplaceWithList.Count != this._partArray.Count)
                return input;

            // Replace arguments.
            var replaceMatchedSubString = new ReplaceMatchedSubString(this, input, argReplaceWithList);
            return this._regExp.Replace(input, replaceMatchedSubString.Do);
        }

        /// <summary>
        /// Replace the specified arguments in the specified string.
        /// </summary>
        /// <param name="input">The string to match.</param>
        /// <param name="textReplaceWithList">The list of text to replace in the input string.</param>
        /// <returns>The processed input string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one or more parameters are null.</exception>
        public string Replace(string input, IEnumerable<string> textReplaceWithList)
        {
            // Check sanity.
            if (textReplaceWithList == null)
                throw new ArgumentNullException("textReplaceWithList", "textReplaceWithList must not be null.");
            if (this._regExp == null || this.Count == 0)
                return input;

            // Build the list of argument values.
            var listArgReplaceWith = new Dictionary<string, string>();
            int index = 0;
            this._partArray.ForEach(part => listArgReplaceWith.Add(part.Name, textReplaceWithList.ElementAt(index++)));

            // Replace arguments.
            return Replace(input, listArgReplaceWith);
        }

        /// <summary>
        /// Matches the specified string with the current text.
        /// </summary>
        /// <param name="input">The string to match.</param>
        /// <param name="matches">The found matches.</param>
        /// <returns>true if successful, false on any error.</returns>
        private bool IsRegExpMatch(string input, List<string> matches)
        {
            // Is it a valid argument.
            if (this._regExp == null || this.Count == 0)
            {
                // Make a string comparison.
                if (String.Compare(this._text, input) != 0)
                    return false;

                // Add to matches.
                if (matches != null)
                    matches.Add(input);

                // Success!
                return true;
            }

            // Try to match now!
            Match match = this._regExp.Match(input);
            if (!match.Success)
                return false;

            // Not enough matches.
            if (match.Groups.Count < 2)
                return false;

            // Do we have to fill the matched list or not ? 
            if (matches == null)
                return true;

            // Loop on all parts and extract the matched substring. 
            matches.AddRange(from part in this._partArray select match.Groups[part.RegExpGroupName] into @group from Capture capture in @group.Captures select capture.Value);

            // Success!
            return true;
        }

        /// <summary>
        /// Parses the current text.
        /// </summary>
        private void ParseRegExp()
        {
            // Parse all character and try to extract the argument.
            int index = 0;
            string regExp = String.Empty;
            var parts = new List<Part>();

            // Loop on all characters, escape the reserved ones, replace our argument with a regular expression.
            while (index < this._text.Length)
            {
                char currentChar = this._text[index];
                switch (currentChar)
                {
                    // Special reg exp character.
                    case '\\':
                    case '^':
                    case '$':
                    case '*':
                    case '?':
                    case '+':
                    case '(':
                    case '[':
                    case ')':
                    case ']':
                    case '}':
                    case '.':
                    case '|':
                        regExp += '\\';
                        regExp += currentChar;
                        index++;
                        break;

                    case '{':
                        {
                            // Move to next.
                            index++;

                            // Escape if this is a curly bracket.
                            if (index >= this._text.Length || this._text[index] == '{')
                            {
                                regExp += '\\';
                                regExp += currentChar;
                                index++;
                                break;
                            }

                            // An argument is expected.
                            int startArg = index - 1;
                            int endArg = -1;
                            bool translatedArg = _text[index] == '#';
                            if (translatedArg)
                                index++;
                            string argName = String.Empty;
                            for (; index < _text.Length; index++)
                            {
                                char curChar = _text[index];
                                if (curChar == '{')
                                    break;
                                if (curChar != '}')
                                {
                                    argName += curChar;
                                    continue;
                                }

                                endArg = index;
                                break;
                            }

                            // The argument exists.
                            if (endArg != -1 && argName.Length > 0)
                            {
                                // Check the closer argument looks like 'xxxx{0}{1}xxx'.
                                if (parts.Count > 0 && (parts.Last().End + 1) == startArg)
                                    return; // Invalid argument.

                                // Check the duplicated argument. 
                                if (parts.Exists(part => part.Name == argName))
                                    return; // Duplicated argument.

                                // Build the regular expression group name associated with this argument. 
                                string strRegExpGroupName = "Arg" + parts.Count;

                                // Store this argument in list.
                                parts.Add(new Part(argName, strRegExpGroupName, /*startArg,*/ endArg, translatedArg));

                                // Replace argument with ".*?".
                                regExp += "(?<";
                                regExp += strRegExpGroupName;
                                regExp += ">";
                                regExp += ".+)";
                            }
                            else
                            {
                                // No argument.
                                regExp += '\\';
                                regExp += this._text.Substring(startArg, index - startArg);
                                break;
                            }

                            // Move to next.
                            index++;
                            break;
                        }

                    default:
                        // Append char and move to next.
                        regExp += currentChar;
                        index++;
                        break;
                }
            }

            // Build the regular expression now!
            this._partArray = parts;
            this._regExpString = "^" + regExp + "$";
            this._regExp = new Regex(this._regExpString);
        }
        #endregion

        /// <summary>
        /// Helper class that replace arguments.
        /// </summary>
        private class ReplaceMatchedSubString
        {
            #region Fields
            /// <summary>
            /// Dictionary of argument parts names/values to replace.
            /// </summary>
            private readonly Dictionary<string, string> _argReplaceWithList;

            /// <summary>
            /// Input string.
            /// </summary>
            private readonly string _input;

            /// <summary>
            /// Argument rule to apply.
            /// </summary>
            private readonly ArgumentRule _argRule;
            #endregion

            #region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="ReplaceMatchedSubString"/> class.
            /// </summary>
            /// <param name="argRule">The argument rule.</param>
            /// <param name="input">The string which contains the arguments.</param>
            /// <param name="argReplaceWithList">The dictionary of parts/arguments to replace.</param>
            public ReplaceMatchedSubString(ArgumentRule argRule, string input, Dictionary<string, string> argReplaceWithList)
            {
                // Set properties.
                this._argRule = argRule;
                this._input = input;
                this._argReplaceWithList = argReplaceWithList;
            }
            #endregion

            #region Methods
            /// <summary>
            /// Replace the argument using the specified Match.
            /// </summary>
            /// <param name="match">The match.</param>
            /// <returns>The processed input string with its arguments replaced.</returns>
            public string Do(Match match)
            {
                string replace = String.Empty;
                int index = 0;

                // Loop on all part and extract the matched substring. 
                foreach (Part part in this._argRule._partArray)
                {
                    Group group = match.Groups[part.RegExpGroupName];
                    CaptureCollection groupCollection = group.Captures;
                    foreach (Capture capture in groupCollection)
                    {
                        replace += this._input.Substring(index, capture.Index - index);
                        replace += this._argReplaceWithList[part.Name];
                        index = capture.Index + capture.Length;
                    }
                }

                // Append the end part.
                replace += this._input.Substring(index);
                return replace;
            }
            #endregion
        }

        /// <summary>
        /// Helper class that represents a part of an argument.
        /// </summary>
        private class Part
        {
            #region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="Part"/> class.
            /// </summary>
            /// <param name="name">The name of part.</param>
            /// <param name="regExpGroupName">The name of the regular expression group.</param>
            /// <param name="end">The ending index.</param>
            /// <param name="translated">True if the text is translated.</param>
            public Part(string name, string regExpGroupName, int end, bool translated)
            {
                // Set properties.
                this.Name = name;
                this.RegExpGroupName = regExpGroupName;
                this.End = end;
                this.Translated = translated;
            }
            #endregion

            #region Properties
            /// <summary>
            /// Gets the name of the part.
            /// </summary>
            /// <value>The name of the part.</value>
            public string Name { get; private set; }

            /// <summary>
            /// Gets the regular expression group name.
            /// </summary>
            /// <value>The regular expression group name.</value>
            public string RegExpGroupName { get; private set; }

            /// <summary>
            /// Gets the ending index.
            /// </summary>
            /// <value>The ending index.</value>
            public int End { get; private set; }

            /// <summary>
            /// Gets a value indicating whether the part is translated or not.
            /// </summary>
            /// <value>A value indicating whether the part is translated or not.</value>
            public bool Translated { get; private set; }
            #endregion
        }

        public enum ArgumentMatch
        {
            /// <summary>
            /// The arguments do not match (different count, different names, ...).
            /// </summary>
            NoMatch = 0,

            /// <summary>
            /// The arguments all match.
            /// </summary>
            Match = 1,

            /// <summary>
            /// The argument matches but the second text misses at least one argument of the first text.
            /// </summary>
            MatchInferior = 2
        }
    }
}