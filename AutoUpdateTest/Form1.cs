using AutoUpdaterDotNET;

namespace AutoUpdateTest
{
    public partial class Form1 : Form
    {
        string pat = "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";

        public Form1()
        {
            InitializeComponent();

            label1.Text = "1.0.0";

            CheckForUpdates();
        }

        public void CheckForUpdates()
        {
            AutoUpdater.Start("https://your_server_path/update.xml");
            AutoUpdater.HttpUserAgent = pat;
        }
    }
}
