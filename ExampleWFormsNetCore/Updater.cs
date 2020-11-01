using System;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.IO.Compression;
using ExampleWFormsNetCore;

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
        public int[] progress { get; set; } = new int[3];


        public void Update(string urlVersion, string urlApp, string versionApp, string nameUpdate)
        {
            if(verifyConnection() == true)
            {
                WebClient getVersionApp = new WebClient();
                string s = getVersionApp.DownloadString(urlVersion);
                if (s != versionApp)
                {
                    hasNewUpdate = true;
                    DownloadFile(address: urlApp, location: location + "/" + nameUpdate);
                } else
                {
                    hasNewUpdate = false;
                }
                while(successUpdate)
                {
                    installUpdate(location + "\\" + nameUpdate);
                }
            } else
            {
                //pas de connexion
            }
        }

        public void DownloadFile(string address, string location)
        {
            WebClient c = new WebClient();
            Uri Uri = new Uri(address);
            successUpdate = false;
            c.DownloadFileCompleted += (sender, e) =>
            {
                if(e.Cancelled)
                {
                    MessageBox.Show("Le téléchargement de la vidéo a été annulé ...");
                }  else
                {
                    successUpdate = true;
                }
            };
            c.DownloadProgressChanged += (sender, e) =>
            {
                progress = new int [3] { e.ProgressPercentage, Convert.ToInt32(e.BytesReceived), Convert.ToInt32(e.TotalBytesToReceive) };
            };
            c.DownloadFileAsync(Uri, location);
        }

        public bool verifyConnection()
        {
            Ping p = new Ping();
            try
            {
                PingReply r = p.Send("216.58.193.78", 5000);
                if(r.Status == IPStatus.Success)
                {
                   return true;
                } else
                {
                   return false;
                }
            } catch { return false;  }

        }


        public void installUpdate(string l)
        {
            string[] f = l.Split(".");
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
                    Process.Start("ExampleWFormsNetCore.exe");
                    break;
            }
        }

    }
}

