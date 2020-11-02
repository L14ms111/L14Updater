using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using L14Updater;

namespace ExampleWFormsNetCore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Updater u = new Updater();
            u.location = AppDomain.CurrentDomain.BaseDirectory;
            string nameUpdate = "update.exe";
            u.Update(urlVersion: "https://localhost/version.txt", urlApp: "https://localhost/ui.exe", versionApp: "1.1", nameUpdate: nameUpdate, consoleApp : false);
            if (u.hasNewUpdate == false)
            {
                label1.Text = "Aucune mise à jour";
                labelZ.Text = null;
            } else
            {           
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = (200); //a adapter
                timer.Start();

                timer.Tick += (sender, e) =>
                {
                    label1.Text = u.Progress[0].ToString() + "%";
                    labelZ.Text = bytesToMB(u.Progress[1]) + " of " + bytesToMB(u.Progress[2]) + " mo";
                    if(u.successUpdate)
                    {
                        u.InstallUpdate(u.location + "/" + nameUpdate);
                        Environment.Exit(0);
                        timer.Stop();
                    }   
                };
            }

        }

        private string bytesToMB(int bytes)
        {
            decimal number;
            number = bytes / 1024 / 1024;
            return number.ToString();
        }

        //optionnal
        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelZ_Click(object sender, EventArgs e)
        {

        }
    }
}
