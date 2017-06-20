using System;
using System.Windows.Forms;

namespace ResourceSyncTool
{
    public partial class DirectorySelectorPopup : Form
    {
        public string SelectedDirectory => txtResXFolder.Text;

        public DirectorySelectorPopup(bool isOldSelector)
        {
            InitializeComponent();

            lblText.Text = isOldSelector
                ? "Please select the directory where the OLD resource files are located"
                : "Please select the directory where the resource files are located";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowDialog();
                txtResXFolder.Text = dialog.SelectedPath;
                if (!string.IsNullOrWhiteSpace(txtResXFolder.Text))
                    DialogResult = DialogResult.OK;
            }
        }
    }
}
