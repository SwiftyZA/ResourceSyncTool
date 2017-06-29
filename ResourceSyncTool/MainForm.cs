using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Chronos.Models;
using Common;
using Common.Enums;
using Common.POCOS;
using ResourceSyncTool.Models;
using ResourceSyncTool.Extenders;
using ResourceSyncTool.Helpers;

namespace ResourceSyncTool
{
    public partial class MainForm : Form
    {
        private string OldDirectory { get; set; }
        private string SelectedDirectory { get; set; }
        private List<CultureContainer> Cultures { get; set; }
        private CultureContainer SelectedCulture { get; set; }
        private BindingList<ResXEntry> ResXEntries { get; set; }
        public LoadingModal LoadingModal { get; set; }
        public GoogleTranslateModal GoogleTranslateModal { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Cultures = new List<CultureContainer>();
            ResXEntries = new BindingList<ResXEntry>();
            //Get all culture info from the .Net overlord
            Cultures.AddCultures(CultureInfo.GetCultures(CultureTypes.AllCultures));
            //Bind to chronos output events
            Chronos.Chronos.OutputEvent += Chronos_OutputEvent;
            Chronos.Chronos.CompletedEvent += Chronos_CompletedEvent;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            StartUpPolyglot(true);
        }

        private void StartUpPolyglot(bool quitOnCancel)
        {
            //Get ResX directory from user
            DialogResult result = ShowDirSelectPopup();
            if (result == DialogResult.Cancel)
            {
                if (quitOnCancel)
                    Application.Exit();
                return;
            }

            //Get the existing cultures
            GetExitingCultures();
            //Show Language Selector Popup
            ShowLanguageSelectorPopup(false);
            //Load Resources
            LoadResxFiles();
        }

        private async Task LoadResxFiles()
        {
            //Show loading Modal
            ToggleLoadingModel(true);
            //Load Resources
            var results = await FileMonger.FileMonger.AsyncLoadResXFiles(SelectedDirectory, SelectedCulture, OldDirectory);
            ResXEntries = new BindingList<ResXEntry>();
            results.ForEach(x => ResXEntries.Add(CheckArguments(x, false)));
            //Bind Drop down lists
            BindComboBoxes();
            //Bind Data Grid View
            BindDataGrid();
            //Show loading Modal
            ToggleLoadingModel(false);
        }

        private void BindComboBoxes()
        {
            List<string> fileOptions = new List<string>() { "All" };
            fileOptions.AddRange(ResXEntries.Select(x => x.SourceFile).Distinct().ToList());
            cboResxFiles.DataSource = fileOptions;


            List<ComboBoxItem<State>> items = new List<ComboBoxItem<State>>() { new ComboBoxItem<State>() { DisplayMember = "All" } };
            items.AddRange(Enum.GetValues(typeof(State)).Cast<State>().Select(x => new ComboBoxItem<State>() { DisplayMember = x.ToUserFriendlyString(), ValueMember = x }).ToList());
            cboState.DataSource = items;
            cboState.DisplayMember = "DisplayMember";
            cboState.ValueMember = "ValueMember";
        }

        private void BindDataGrid()
        {
            var source = GetListToBind();
            lblTotal.Text = ResXEntries.Count.ToString();
            lblFilteredTotal.Text = source.Count.ToString();

            dgvResX.DataSource = source;
            dgvResX.Columns["Key"].Visible = true;
            dgvResX.Columns["Value"].ReadOnly = true;
            dgvResX.Columns["Comment"].ReadOnly = true;
            dgvResX.Columns["State"].Visible = false;
            dgvResX.Columns["LocalizedValue"].HeaderText = "Localized Value";
            dgvResX.Columns["LocalizedComment"].HeaderText = "Localized Comment";
            dgvResX.Columns["SourceFile"].HeaderText = "Resource File";
            dgvResX.Columns["SourceFile"].ReadOnly = true;
            dgvResX.Columns["SourceFile"].DisplayIndex = 6;
            dgvResX.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewRow row in dgvResX.Rows)
            {
                var item = (ResXEntry)row.DataBoundItem;
                if (item == null) continue;
                switch (item.State)
                {
                    case State.Updated:
                        row.DefaultCellStyle.BackColor = Color.LimeGreen;
                        break;
                    case State.GoogleTranslated:
                        row.DefaultCellStyle.BackColor = Color.Aqua;
                        break;
                    case State.MasterChanged:
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case State.New:
                        row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        break;
                    case State.LocalizedChanged:
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        break;
                    case State.Faulted:
                        row.DefaultCellStyle.BackColor = Color.Red;
                        break;
                    case State.NoChange:
                    default:
                        break;
                }
            }
        }

        private void GetExitingCultures()
        {
            //Get sub directories from the file monger
            IEnumerable<string> subDirs = FileMonger.FileMonger.GetSubDirectories(SelectedDirectory);
            //Update cultures collection with existing cultures
            Cultures.Where(x => x.Existing).ToList().ForEach(x => x.Existing = false);
            Cultures.Where(x => subDirs.Contains(x.Culture.Name)).ToList().ForEach(x => x.Existing = true);
        }

        private DialogResult ShowLanguageSelectorPopup(bool allowExit)
        {
            DialogResult result;

            using (var popup = new LanguageSelectorPopup(Cultures, allowExit))
            {
                result = popup.ShowDialog();
                if (result == DialogResult.Cancel) return result;
                SelectedCulture = popup.SelectedLanguage;
                lblLang.Text = $"Currently Editing {SelectedCulture.Culture.DisplayName}";
            }

            return result;
        }

        private DialogResult ShowDirSelectPopup()
        {
            DialogResult result;
            do
            {
                using (var popup = new DirectorySelectorPopup(false))
                {
                    result = popup.ShowDialog();

                    if (result == DialogResult.Cancel)
                        break;

                    if (FileMonger.FileMonger.CheckDirectory(popup.SelectedDirectory))
                    {
                        SelectedDirectory = popup.SelectedDirectory;
                        LblCurrentDir.Text = SelectedDirectory;
                    }
                    else
                    {
                        MessageBox.Show("The selected directory contains no resource files", "No Resx files found",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } while (string.IsNullOrWhiteSpace(SelectedDirectory));

            return result;
        }

        private void ShowOldFileSelectPopup()
        {
            DialogResult result;

            do
            {
                using (var popup = new DirectorySelectorPopup(true))
                {
                    result = popup.ShowDialog();

                    if (result == DialogResult.Cancel)
                        return;

                    if (FileMonger.FileMonger.CheckDirectory(popup.SelectedDirectory))
                    {
                        OldDirectory = popup.SelectedDirectory;
                        lblPreviousDir.Text = OldDirectory;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("The selected directory contains no resource files", "No Resx files found",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            } while (true);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        private BindingSource GetListToBind()
        {
            var query = ResXEntries.Select(x => x);

            if (cboResxFiles.SelectedValue.ToString() != "All")
                query = query.Where(x => x.SourceFile == cboResxFiles.SelectedValue.ToString());

            ComboBoxItem<State> selectedState = cboState.SelectedItem as ComboBoxItem<State>;

            if (selectedState.DisplayMember != "All")
            {
                query = query.Where(x => x.State == selectedState.ValueMember);
            }

            if (!string.IsNullOrWhiteSpace(txtSearchPhrase.Text))
            {
                query =
                    query.Where(x => x.Value.ToLower().Contains(txtSearchPhrase.Text.ToLower())
                    || x.Comment.ToLower().Contains(txtSearchPhrase.Text.ToLower())
                    || (!string.IsNullOrEmpty(x.LocalizedValue) && x.LocalizedValue.ToLower().Contains(txtSearchPhrase.Text.ToLower()))
                    || (!string.IsNullOrEmpty(x.LocalizedComment) && x.LocalizedComment.ToLower().Contains(txtSearchPhrase.Text.ToLower())));
            }

            return new BindingSource(query.OrderByDescending(x => x.State).ToList(), null);
        }

        private void dgvResX_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var item = dgvResX.Rows[e.RowIndex].DataBoundItem as ResXEntry;
            CheckNulls(item);
            CheckArguments(item, true);

            switch (item.State)
            {
                case State.Faulted:
                    dgvResX.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    MessageBox.Show("The number of arguments ( {0} ) in the reference value / comment \ndoes not match the number in the localized value / comment", "Argument mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case State.Updated:
                    dgvResX.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LimeGreen;
                    break;
                case State.New:
                case State.NoChange:
                case State.GoogleTranslated:
                case State.MasterChanged:
                case State.LocalizedChanged:
                default:
                    return;
            }
        }

        private ResXEntry CheckArguments(ResXEntry entry, bool markUpdated)
        {
            var valueMatch = ArgumentRule.MatchTexts(entry.Value, entry.LocalizedValue);
            var commentMatch = ArgumentRule.ArgumentMatch.Match;
            if (!string.IsNullOrEmpty(entry.LocalizedComment))
            {
                commentMatch = ArgumentRule.MatchTexts(entry.Comment, entry.LocalizedComment);
            }

            if (valueMatch != ArgumentRule.ArgumentMatch.Match || commentMatch != ArgumentRule.ArgumentMatch.Match)
            {
                entry.State = State.Faulted;
            }
            else if (markUpdated)
            {
                entry.State = State.Updated;
            }
            return entry;
        }

        private void CheckNulls(ResXEntry entry)
        {
            if (entry.LocalizedValue == null)
                entry.LocalizedValue = String.Empty;
            if (entry.LocalizedComment == null)
                entry.LocalizedComment = String.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ResXEntries.Any(x => x.State == State.Faulted))
            {
                MessageBox.Show("Changes cannot be saved\nThere are some entries in a faulted state", "Save Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileMonger.FileMonger.SaveResXChanges(ResXEntries.ToList(), SelectedDirectory, SelectedCulture.Culture);
            //Check if it was a new culture, if so, reload cultures
            if (!SelectedCulture.Existing)
            {
                GetExitingCultures();
                SelectedCulture.Existing = true;
            }
            LoadResxFiles();
        }

        private void ToggleGoogleTranslateModal(int total)
        {
            if (total > 0)
            {
                GoogleTranslateModal = new GoogleTranslateModal(total);
                //GoogleTranslateModal has to be centered over parent manually as it is not displayed as a dialog. The location is calculated right before show since the user may have moved the main form
                GoogleTranslateModal.Location = new Point(Location.X + (Width - GoogleTranslateModal.Width) / 2, Location.Y + (Height - GoogleTranslateModal.Height) / 2);
                GoogleTranslateModal.Show(this);
                this.Enabled = false;
            }
            else
            {
                GoogleTranslateModal.Kill();
                this.Invoke((MethodInvoker)delegate
                {
                    this.Enabled = true;
                    BindDataGrid();
                });
            }
        }

        private void Chronos_OutputEvent(object sender, ChronosEvents e)
        {
            var entry = ResXEntries.FirstOrDefault(x => x.Key == e.JobItem.Key && x.SourceFile == e.JobItem.SourceFile);
            if (entry == null) return;

            entry.LocalizedValue = e.JobItem.LocalizedValue;
            entry.LocalizedComment = e.JobItem.LocalizedComment;
            entry.State = State.GoogleTranslated;

            CheckArguments(entry, false);

            GoogleTranslateModal.IncrementProgress();
        }

        private void Chronos_CompletedEvent(object sender, ChronosEvents e)
        {
            ToggleGoogleTranslateModal(0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Put shyte here that needs to happen before the app closes (pending changes, file handles, pending api calls, etc)
        /// </summary>
        private bool PerformPreExitCleanup()
        {
            bool cancelClose = false;
            if (ResXEntries.Any(x => x.State == State.Updated))
            {
                var result = MessageBox.Show("You have unsaved changes, are you sure you want to quit?", "Discard Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    cancelClose = true;
            }
            return cancelClose;
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = PerformPreExitCleanup();
        }

        private void ToggleLoadingModel(bool show)
        {
            if (show)
            {
                LoadingModal = new LoadingModal();
                //LoadingModal has to be centered over parent manually as it is not displayed as a dialog. The location is calculated right before show since the user may have moved the main form
                LoadingModal.Location = new Point(Location.X + (Width - LoadingModal.Width) / 2, Location.Y + (Height - LoadingModal.Height) / 2);
                LoadingModal.Show(this);
                this.Enabled = false;
            }
            else
            {
                LoadingModal.Close();
                this.Enabled = true;
            }
        }

        private void btnChangeLang_Click(object sender, EventArgs e)
        {
            ShowLanguageSelectorPopup(true);
            //Load Resources
            LoadResxFiles();
        }

        private void btnCompareOldMainResXFiles_Click(object sender, EventArgs e)
        {
            if (ResXEntries.Any(x => x.State == State.Updated || x.State == State.GoogleTranslated))
            {
                var result = MessageBox.Show("You have unsaved changes, continuing will discard you changes.\nAre you sure you want to continue?", "Discard Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No) return;
            }
            //Ask user for old resx file location
            ShowOldFileSelectPopup();
            if (!string.IsNullOrWhiteSpace(OldDirectory))
            {
                LoadResxFiles();
            }
        }

        private void ChangeMainResXLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartUpPolyglot(false);
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            var itemsToTranslate = ResXEntries.Where(x => x.State == State.New).ToList();
            GoogleTranslateResXEntries(itemsToTranslate);
        }

        private void btnTranslateFilterResults_Click(object sender, EventArgs e)
        {
            var itemsToTranslate = dgvResX.Rows.OfType<DataGridViewRow>().Select(x => x.DataBoundItem as ResXEntry).ToList();
            GoogleTranslateResXEntries(itemsToTranslate);
        }

        private void btnTranslateSelectedRows_Click(object sender, EventArgs e)
        {
            var itemsToTranslate = dgvResX.SelectedRows.OfType<DataGridViewRow>().Select(x => x.DataBoundItem as ResXEntry).ToList();
            GoogleTranslateResXEntries(itemsToTranslate);
        }

        private void GoogleTranslateResXEntries(List<ResXEntry> entries)
        {
            if (entries.Count == 0)
            {
                MessageBox.Show("There are no values to translate", "Translate Not Available", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show($"You are about to translate {entries.Count} items, do you want to continue?", "Translate Items", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            Chronos.Chronos.AddResXEntries(entries);
            Chronos.Chronos.StartProcessing(new ChronosParameters()
            {
                Culture = SelectedCulture.Culture,
                MaxNumberOfActiveThreads = 200,
                RequestsPerSecond = 200
            });

            ToggleGoogleTranslateModal(entries.Count);
        }

        private void btnClearOldDir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OldDirectory)) return;

            if (ResXEntries.Any(x => x.State == State.Updated || x.State == State.GoogleTranslated))
            {
                var result = MessageBox.Show("You have unsaved changes, continuing will discard you changes.\nAre you sure you want to continue?", "Discard Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No) return;
            }

            OldDirectory = String.Empty;
            lblPreviousDir.Text = "None Selected";

            LoadResxFiles();
        }
    }
}
