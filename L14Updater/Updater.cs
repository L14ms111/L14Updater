using System;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;

namespace L14Updater
{
    /*
     * 
     * 
     * Fait par Ismaïl M. (L14ms1) <l14ms1@outlook.fr> <github.com/L14ms111/L14Updater>
     * 
     * 
     */
    public class Updater
    {
        public bool hasNewUpdate;
        public bool successUpdate;
        public bool cancelUpdate = false;
        public bool errorUpdate = false;
        public string location;
        public int[] Progress { get; set; } = new int[3];


        public void Update(string urlVersion, string urlApp, string versionApp, string nameUpdate, bool consoleApp)
        {
            if (VerifyConnection() == true)
            {
                WebClient getVersionApp = new WebClient();
                string s = getVersionApp.DownloadString(urlVersion);
                if (s != versionApp)
                {
                    hasNewUpdate = true;
                    DownloadFile(address: urlApp, location: location + "/" + nameUpdate, consoleApp: consoleApp);
                }
                else
                {
                    hasNewUpdate = false;
                }
                while (successUpdate)
                {
                    InstallUpdate(location + "\\" + nameUpdate);
                }
            }
            else
            {
                errorUpdate = true;
            }
        }

        public void DownloadFile(string address, string location, bool consoleApp)
        {
            WebClient c = new WebClient();
            Uri Uri = new Uri(address);
            successUpdate = false;

            if (consoleApp == false)
            {
                c.DownloadFileCompleted += (sender, e) =>
                {
                    if (e.Cancelled)
                    {
                        successUpdate = false;
                    }
                    else
                    {
                        successUpdate = true;
                    }
                };

                c.DownloadProgressChanged += (sender, e) =>
                {
                    Progress = new int[3] { e.ProgressPercentage, Convert.ToInt32(e.BytesReceived), Convert.ToInt32(e.TotalBytesToReceive) };
                };
            }
            else
            {
                c.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
                c.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
            }
            c.DownloadFileAsync(Uri, location);
        }

        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress = new int[3] { e.ProgressPercentage, Convert.ToInt32(e.BytesReceived), Convert.ToInt32(e.TotalBytesToReceive) };
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                successUpdate = false;
            }
            else
            {
                successUpdate = true;
            }
            successUpdate = true;
        }

        public bool VerifyConnection()
        {
            Ping p = new Ping();
            try
            {
                PingReply r = p.Send("216.58.193.78", 5000); // server google
                if (r.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }

        }


        public void InstallUpdate(string l)
        {
            string[] f = l.Split(Convert.ToChar("."));
            switch (f[1])
            {

                case "exe":
                    Process.Start(l);
                    break;

                case "msi":
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @l,
                        UseShellExecute = true
                    });
                    break;

                case "zip":
                    ZipFile.ExtractToDirectory(l, AppDomain.CurrentDomain.BaseDirectory);
                    Process.Start(AppDomain.CurrentDomain.FriendlyName);
                    break;
            }
        }

    }
}