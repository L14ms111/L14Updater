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
            string nameUpdate = "update.zip";
            u.Update(urlVersion: "https://localhost/version.txt", urlApp: "http://localhost/a.zip", versionApp: "1.0", nameUpdate: nameUpdate);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (200); //a adapter
            timer.Start();

            timer.Tick += (sender, e) =>
            {
                label1.Text = u.progress[0].ToString() + "%";
                labelZ.Text = bytesToMB(u.progress[1]) + " of " + bytesToMB(u.progress[2]) + " mo";
                if(u.successUpdate)
                {
                    u.installUpdate(u.location + "/" + nameUpdate);
                }
            };
            if (u.hasNewUpdate == false)
            {
                label1.Text = "Aucune mise à jour";
                labelZ.Text = null;
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
