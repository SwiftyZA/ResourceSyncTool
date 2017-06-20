using System.Windows.Forms;

namespace ResourceSyncTool
{
    public partial class GoogleTranslateModal : Form
    {
        public GoogleTranslateModal(int total)
        {
            InitializeComponent();
            pgsbrGoogle.Maximum = total;
            pgsbrGoogle.Minimum = 0;
            pgsbrGoogle.Step = 1;
            lblProgress.Text = $"Translated  {pgsbrGoogle.Value}  of  {pgsbrGoogle.Maximum}  keys";
        }

        public void IncrementProgress()
        {
            lblProgress.Invoke((MethodInvoker)delegate
            {
                lblProgress.Text = $"Translated {pgsbrGoogle.Value} of {pgsbrGoogle.Maximum} keys";
            });

            pgsbrGoogle.Invoke((MethodInvoker)delegate
            {
                pgsbrGoogle.PerformStep();
            });
        }

        public void Kill()
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }
    }
}
