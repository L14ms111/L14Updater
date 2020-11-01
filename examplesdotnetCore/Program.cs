using System;
using System.Threading;
using L14Updater;

namespace examplesdotnetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
            Updater u = new Updater();
            u.location = AppDomain.CurrentDomain.BaseDirectory;
            string nameUpdater = "update.exe";
            u.Update(urlVersion: "https://localhost/version.txt", urlApp: "https://localhost/ui.exe", versionApp: "1.0", nameUpdate: nameUpdater, consoleApp: true);
            if (u.hasNewUpdate == true)
            {
                if (u.VerifyConnection() == true)
                {
                    while (!u.successUpdate)
                    {
                        Thread.Sleep(100);
                        if (u.errorUpdate == true || u.cancelUpdate == true)
                        {
                            Console.WriteLine("La mise à jour a été interrompu pour une raison inexact");

                        }
                        else
                        {
                            Console.WriteLine("downloaded {0} of {1} bytes. {2} % complete...", u.Progress[1], u.Progress[2], u.Progress[0]);
                        }

                    }
                    if (u.successUpdate)
                    {
                        u.InstallUpdate(u.location + "/" + nameUpdater);
                    }
                }
            }

        }
    }
}
