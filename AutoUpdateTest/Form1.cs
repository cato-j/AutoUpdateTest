using AutoUpdaterDotNET;
using System.Net;

namespace AutoUpdateTest
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private static VersionNumber version = new(8,0,0);

        public Form1()
        {
            InitializeComponent();

            label1.Text = version.ToString();

            CheckForUpdates();
        }

        public async Task CheckForUpdates()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", "request");

            var updater = new GitHubUpdater();
            var latest = await updater.GetLatestRelease(client);
            var latestVersion = new VersionNumber(latest.TagName);

            label1.Text = $"{version.ToString()} | {latestVersion}";

            if (latestVersion > version)
            {
                try
                {
                    var assets = latest.Assets;
                    var first = assets.FirstOrDefault();
                    var url = first.BrowserDownloadUrl;

                    using SaveFileDialog saveFileDialog = new SaveFileDialog()
                    {
                        AddExtension = true,
                        AddToRecent = true,
                        DefaultExt = ".msi",
                        FileName = first.Name,
                        OverwritePrompt = true,
                    };
                    if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                    // Download file
                    byte[] fileBytes = await client.GetByteArrayAsync(url);
                    await File.WriteAllBytesAsync(saveFileDialog.FileName, fileBytes);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
