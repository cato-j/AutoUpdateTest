using AutoUpdaterDotNET;

namespace AutoUpdateTest
{
    public partial class Form1 : Form
    {
        internal static string pat = "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";
        private static string token => $"Bearer {pat}";
        private static readonly HttpClient client = new HttpClient();
        private static VersionNumber version = new(6,0,0);

        public Form1()
        {
            InitializeComponent();

            label1.Text = version.ToString();

            CheckForUpdates();
        }
        public async Task CheckForUpdates()
        {
            var updater = new GitHubUpdater();
            var latest = await updater.GetLatestRelease();
            var latestVersion = new VersionNumber(latest.TagName);

            label1.Text = $"{version.ToString()} | {latestVersion}";

            if (latestVersion > version)
            {
                AutoUpdater.Start(latest.Assets.Find(x=>x.BrowserDownloadUrl.ToLower().Equals(".msi")).BrowserDownloadUrl);
                AutoUpdater.HttpUserAgent = pat;
                //AutoUpdater.CurrentVersion = latestVersion;
            }
        }
    }
}
