using System;

namespace HostsFileEditor
{
    class Program
    {
        
        static void Main(string[] args)
        {
            HostsFile hostsFile = new HostsFile();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Main(hostsFile);



            //switch (Console.ReadKey().Key)
            //{
            //    case ConsoleKey.D0:
            //        EntryDetails(hostsFile.hostsEntries[0], args);
            //        break;
            //    case ConsoleKey.D1:
            //        EntryDetails(hostsFile.hostsEntries[1], args);
            //        break;
            //    case ConsoleKey.D2:
            //        EntryDetails(hostsFile.hostsEntries[2], args);
            //        break;
            //    case ConsoleKey.D3:
            //        EntryDetails(hostsFile.hostsEntries[3], args);
            //        break;
            //    case ConsoleKey.D4:
            //        EntryDetails(hostsFile.hostsEntries[4], args);
            //        break;
            //    case ConsoleKey.D5:
            //        EntryDetails(hostsFile.hostsEntries[5], args);
            //        break;
            //    case ConsoleKey.N:
            //        Console.WriteLine('N');
            //        break;
            //    default:
            //        Console.WriteLine("invalid key");
            //        break;
            //}
        }

       
    
    }
}
