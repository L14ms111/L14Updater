using System;
using System.Threading;
using L14Updater;

namespace examplesdotnetCore
{
    class Program
    {
        static void Main(string[] args)
        { 
            Updater u = new Updater();
            u.location = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            u.Update(urlVersion: "https://localhost/version.txt", urlApp: "https://localhost/ui.exe", versionApp: "1.1", nameUpdate: "update.exe");
            if(u.verifyConnection() == true)
            {
                while (!u.successUpdate)
                {
                    Thread.Sleep(100);
                    Console.WriteLine("downloaded {0} of {1} bytes. {2} % complete...", u.progress[1], u.progress[2], u.progress[0]);
                }
                if(u.hasNewUpdate == false)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
