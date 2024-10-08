using AutoUpdaterDotNET;
using System;
using System.Diagnostics;
using System.Net;

namespace AutoUpdateTest
{
    public partial class Form1 : Form
    {
        private static readonly VersionNumber version = new(13, 0, 0);

        public Form1()
        {
            InitializeComponent();

            label1.Text = version.ToString();

            this.Load += Form1_Load;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await CheckForUpdates();
        }

        public async Task CheckForUpdates()
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", "request");

                var updater = new GitHubUpdater();
                var latest = await GitHubUpdater.GetLatestRelease(client);
                if (latest == null) return;
                var latestVersion = new VersionNumber(latest.TagName);

                label1.Text = $"{version} | {latestVersion}";

                if (latestVersion > version)
                {
                    var assets = latest.Assets;
                    var first = assets.FirstOrDefault();
                    if (first == null) return;
                    var url = first.BrowserDownloadUrl;

                    using SaveFileDialog saveFileDialog = new()
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

                    Process.Start("msiexec.exe", $"/i \"{saveFileDialog.FileName}\" /passive");
                    Environment.Exit(0);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
