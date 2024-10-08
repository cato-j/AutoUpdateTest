using AutoUpdaterDotNET;

namespace AutoUpdateTest
{
    public partial class Form1 : Form
    {
        string pat = "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";

        public Form1()
        {
            InitializeComponent();

            label1.Text = "2.0.0";

            CheckForUpdates();
        }

        public void CheckForUpdates()
        {
            AutoUpdater.Start("https://github.com/cato-j/AutoUpdateTest/blob/master/latestVersion.xml");
            AutoUpdater.HttpUserAgent = pat;
        }
    }
}
