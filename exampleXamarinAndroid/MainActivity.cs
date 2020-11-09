using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using L14Updater;
using System.Threading;
using System.Threading.Tasks;
using System;
using Android;
using Android.Support.V4.App;
using System.Diagnostics;
using System.IO;
using System.Timers;
using Android.Content;
using Android.Net;

namespace exampleXamarinAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private static System.Timers.Timer a;
        static readonly int REQUEST_STORAGES = 1;
        static string[] PERMISSIONS_STORAGES = {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ActivityCompat.RequestPermissions(this, PERMISSIONS_STORAGES, REQUEST_STORAGES);
            Updater u = new Updater();
            u.location = (string)GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads);
            string NameUpdate = "update.apk";
            u.Update(urlVersion: "https://dcc39fa75019.ngrok.io/version.txt", urlApp: "https://dcc39fa75019.ngrok.io/ui.apk", versionApp: "1.4", nameUpdate: NameUpdate, consoleApp: false);
            var info = this.FindViewById<TextView>(Resource.Id.textView1);
            var _progress = this.FindViewById<TextView>(Resource.Id.textView3);
            var mo = this.FindViewById<TextView>(Resource.Id.textView4);
            if (u.VerifyConnection() == true)
            {
                if (u.hasNewUpdate == false)
                {
                    info.Text = "Aucune mise à jour";
                    this.FindViewById<TextView>(Resource.Id.textView2).Text = null;
                    _progress.Text = null;
                    mo.Text = null;
                }
                else
                {
                    a = new System.Timers.Timer();
                    a.Interval = 100;

                    a.Elapsed += (source, e) =>
                    {
                        if (u.successUpdate)
                        {
                            info.Text = "Mise à jour terminé";
                            this.FindViewById<TextView>(Resource.Id.textView2).Text = null;
                            _progress.Text = null;
                            mo.Text = null;
                            Toast.MakeText(this, "finish", ToastLength.Long).Show();
                            // now open apk for install ^^
                            a.Stop();
                           
                        }
                        info.Text = "Installation de la mise à jour ...";
                        this.FindViewById<TextView>(Resource.Id.textView2).Text = null;
                        _progress.Text = u.Progress[0] + " %";
                        mo.Text = bytesToMB(u.Progress[1]) + " of " + bytesToMB(u.Progress[2]) + " mo";
                    };


                    a.AutoReset = true;
                    a.Enabled = true;            
                }
            }
            else
            {
                info.Text = "Aucune connexion detecté.";
                this.FindViewById<TextView>(Resource.Id.textView2).Text = null;
                _progress.Text = null;
                mo.Text = null;
            }

        }
        private string bytesToMB(int bytes)
        {
            float number;
            number = bytes / 1024f / 1024f;
            return number.ToString();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}