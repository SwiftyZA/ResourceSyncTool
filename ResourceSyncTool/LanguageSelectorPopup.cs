using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Common.POCOS;

namespace ResourceSyncTool
{
    public partial class LanguageSelectorPopup : Form
    {
        internal CultureContainer SelectedLanguage { get; set; }

        public LanguageSelectorPopup(List<CultureContainer> cultures, bool allowExit)
        {
            InitializeComponent();

            cboLanguages.DrawMode = DrawMode.OwnerDrawVariable;
            cboLanguages.DropDownStyle = ComboBoxStyle.DropDown;
            cboLanguages.DataSource = cultures.OrderByDescending(x => x.Existing).ThenBy(x => x.Name).ToList();
            cboLanguages.DisplayMember = "Name";
            cboLanguages.ValueMember = "Value";

            btnCancel.Visible = allowExit;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cboLanguages_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font font = cboLanguages.Font;
            Brush brush = Brushes.Black;
            CultureContainer culture = (CultureContainer)cboLanguages.Items[e.Index];

            if (culture.Existing)
            {
                font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            }

            e.Graphics.DrawString(culture.Name, font, brush, e.Bounds);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            var item = cboLanguages.SelectedItem as CultureContainer;
            if (item == null)
            {
                MessageBox.Show("Selected value is invalid, please select a valid language", "Invalid language",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SelectedLanguage = item;
            DialogResult = DialogResult.OK;
        }
    }
}
