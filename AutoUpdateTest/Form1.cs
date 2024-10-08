using AutoUpdaterDotNET;

namespace AutoUpdateTest
{
    public partial class Form1 : Form
    {
        internal static string pat = "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";
        private static string token => $"Bearer {pat}";
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();

            label1.Text = "4.0.0";

            CheckForUpdates();
        }
        public async Task CheckForUpdates()
        {
            var updater = new GitHubUpdater();
            var latest = await updater.GetLatestRelease();

            label1.Text = $"{label1.Text} | {latest.TagName}";

            //AutoUpdater.Start(latest);
            //AutoUpdater.HttpUserAgent = pat;
            //AutoUpdater.CurrentVersion = latestVersion;
        }
    }
}
