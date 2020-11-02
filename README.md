# Amethyste
L14Updater is a library which allows anyone to add an updater in their project.
# How to use ?
## Install library
To install it, you can either copy paste the Updater.cs file or use NuGet.

## Code

First, we integrate it into the code :

```
using L14Updater;
```
After saying you want to use this namespace, you can start using it :
```
Updater u = new Updater();
```
The code only tells us, that we will use the functions available in the class.

You must start to highlight 1 very important variable for the rest of the program, this is where we want to put the executable of the program in our case we want to put it at the root of the program:
```
u.location = AppDomain.CurrentDomain.BaseDirectory;
```

Then you must put in a function, which link contains the current version of the software, the link of the executable or the .zip archive, the version of the application ,what you want to name the executable or the archive zip and the last parameter is a bool, to say if it's a console app or not.
```
u.Update(urlVersion: "", urlApp: "", versionApp: "", nameUpdate: "", consoleApp : false);
```

And from there, you can tell if there is a new update, has the program installer been installed or even if there is a connection or not.

This is to say if there is or not a new update:

```
if(u.hasNewUpdate)
{
    // your code
}
```
This is to say is there a connection or not:
```
if(u.verifyConnection()) 
{
    // your code
}
```

And that to say if the installer or the zip archive has been installed:
```
if(u.successUpdate) 
{
    // your code
}
```

To know the percentage of the installation of the executable or the zip archive, just put:
```
u.progress[0];
```
Consequently to know how many bytes have been downloaded, or the size of the executable or the zip archive is:
```
// bytes downloaded
u.progress[1];
// bytes total
u.progress[2]:
```

[BONUS] to convert from bytes to megabytes
```
private string bytesToMB(int bytes)
{
    int number;
    number = bytes / 1024 / 1024;
    return number.ToString();
}
```

# About the library
This small library, which allows the developer to save 30 minutes, or even 1 hour. It is easy to use, examples are provided with the project: as 4 examples, one in Windows Form with .NET Framework technology, another in Windows Form also but in .NET Core and finally a console version in .NET Core and finally a version on Xamarin Android.
## Comptability
Windows : .NET Core, .NET Framework (i tested)

Android : Xamarin Android
