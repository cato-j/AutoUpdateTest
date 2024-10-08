using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdateTest
{
    internal class GitHubUpdater
    {
        private static readonly string baseLatestUrl = "https://api.github.com/repos/your_username/your_repo/releases/latest";
        private static string LatestUrl
        {
            get
            {
                string username = "cato-j";
                string repo = "AutoUpdateTest";

                string s = baseLatestUrl.Replace("your_username", username);
                s = s.Replace("your_repo", repo);
                return s;
            }
        }

        public static async Task<GitHubRelease?> GetLatestRelease(HttpClient client)
        {
            var response = await client.GetStringAsync(LatestUrl);
            GitHubRelease? release = JsonConvert.DeserializeObject<GitHubRelease>(response);

            return release;
        }

        public class GitHubRelease
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }

            [JsonProperty("assets")]
            public List<Asset> Assets { get; set; }

            public GitHubRelease()
            {
                TagName = string.Empty;
                Name = string.Empty;
                Body = string.Empty;
                Assets = [];
            }
        }

        public class Asset
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }

            public Asset()
            {
                Name = string.Empty;
                BrowserDownloadUrl = string.Empty;
            }
        }
    }
}
