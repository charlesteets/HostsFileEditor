using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HostsFileEditor
{
    internal class MainMenu
    {
        HostsFile hosts = new HostsFile();
        int page = 0;
        internal void Main(HostsFile hostsFile)
        {
            //int pages = hostsFile.hostsEntries.Count
            int pages = (int)(MathF.Ceiling((float)(((hostsFile.hostsEntries.Count-1) / 8))));
            Console.Clear();
            hosts = hostsFile;
            
            Console.WriteLine("Hosts file currently contains these entries:");
            Console.WriteLine("\n");
            Console.WriteLine($"Page {page + 1}");
            Console.WriteLine("\n");

            if (page > 0)
            {
                Console.WriteLine($"1. Previous Page");
            }

            for (int i = 0; i <= 7; i++)
                {
                    int actualEntry = i + (8 * page);
                    if (actualEntry < hostsFile.hostsEntries.Count) 
                    { 
                        Console.WriteLine($"{i + 2}. {hostsFile.hostsEntries[actualEntry].URL} mapped to {hostsFile.hostsEntries[actualEntry].IP}."); 
                    }
                }
            if (pages > page)
                {
                    Console.WriteLine($"0. Next Page");
                }
            //}
            Console.WriteLine("\n");
            Console.WriteLine("Press a number for more actions, \"B\" to backup, \"N\" for a new entry, or \"E\" to exit.");
            string input = Console.ReadKey().KeyChar.ToString();

            if (input == "E" || input == "e")
            {
                Console.Clear();
                Console.Write("Are you sure you wish to exit? (Y/N)");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.N:
                        Main(hostsFile);
                        break;
                }
            }
            if (input == "0")
            {
                if (pages > page)
                { page++; }
                Main(hostsFile);
            }

            if (input == "1")
            {
                if (page > 0)
                { page--; }
                Main(hostsFile);
            }

            if (int.TryParse(input, out int parsedInput)) {
                int pageCorrectedInput =  parsedInput - 2 + (page * 8);
                if (pageCorrectedInput < hostsFile.hostsEntries.Count && parsedInput != -1)
                {
                    EntryDetails(hostsFile.hostsEntries[pageCorrectedInput]);
                }
                else
                {
                    Main(hostsFile);

                }
            }
            else if (input == "N" || input == "n")
            {
                NewEntry(hostsFile);
            }
            else if (input == "B" || input == "b")
            {
                hosts.BackupHostsFile();
                Console.Clear();
                Console.WriteLine($"Backup made at {HostsFile.HostsFilePath}.bkup.\nPress any key to continue.");
                Console.ReadKey();
                Main(hostsFile);
            }
            else
            {
                Main(hostsFile);
            }
        }

        private void NewEntry(HostsFile hostsFile)
        {
            Console.Clear();
            Console.WriteLine("Please enter the URL you wish to map.");
            Regex regex = new Regex(@"(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()!@:%_\+.~#?&\/\/=]*)");

            string URLInput = Console.ReadLine();
            if (regex.IsMatch(URLInput))
            {
                Console.WriteLine("valid URL");
            }
            else
            {
                Console.WriteLine("Invalid URL. Press any key to return to main menu.");
                Console.ReadKey();
                Main(hostsFile);
            }

            Console.WriteLine($"Please enter the IP you wish to map {URLInput} to.");
            regex = new Regex(@"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}|localhost)");
                      
            string IPInput = Console.ReadLine();
            if (regex.IsMatch(IPInput))
            {
                Console.WriteLine("valid IP");
                HostsEntry newEntry = new HostsEntry(URLInput, IPInput);
                hostsFile.hostsEntries.Add(newEntry);
                hostsFile.SaveHostsFile();
                Main(hostsFile);
            }
            else
            {
                Console.WriteLine("Invalid IP. Press any key to return to main menu.");
                Console.ReadKey();
                Main(hostsFile);
            }
        }

        void EntryDetails(HostsEntry entry)
        {
            Console.Clear();
            Console.WriteLine($"{entry.URL} is mapped to {entry.IP}.");
            Console.WriteLine("Press \"U\" to change the URL, \"I\" to change the IP, \"D\" to delete, or \"B\" to go back");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.U:
                    Console.Clear();
                    Console.Write($"Please enter the URL you would like to replace this entry." + "\n");
                    Regex regex = new Regex(@"(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()!@:%_\+.~#?&\/\/=]*)");

                    string URLInput = Console.ReadLine();
                    if (regex.IsMatch(URLInput))
                    {
                        Console.Write($"Replace {entry.URL} with {URLInput}? (Y/N)");
                        ConsoleKey yesNoInput = ConsoleKey.Decimal;
                        while (yesNoInput != ConsoleKey.Y || yesNoInput != ConsoleKey.N)
                        {
                            yesNoInput = Console.ReadKey().Key;
                        }
                        switch (yesNoInput)
                        {
                            case ConsoleKey.Y:
                                entry.URL = URLInput;
                                hosts.SaveHostsFile();
                                Console.ReadKey();
                                Main(hosts);
                                break;
                            case ConsoleKey.N:
                                EntryDetails(entry);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid URL. Press any key to try again.");
                        Console.ReadKey();
                        EntryDetails(entry);
                    }
                    break;
                case ConsoleKey.I:
                    Console.Clear();
                    Console.Write($"Please enter the IP you would like to replace this entry." + "\n");
                    regex = new Regex(@"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}|localhost)");

                    string IPInput = Console.ReadLine();
                    if (regex.IsMatch(IPInput))
                    {
                        Console.Write($"Replace {entry.IP} with {IPInput}? (Y/N)");
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.Y:
                                entry.IP = IPInput;
                                hosts.SaveHostsFile();
                                Console.ReadKey();
                                Main(hosts);
                                break;
                            case ConsoleKey.N:
                                EntryDetails(entry);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid IP. Press any key to try again.");
                        Console.ReadKey();
                        EntryDetails(entry);
                    }
                    break;
                case ConsoleKey.D:
                    Console.Clear();
                    Console.Write($"Are you sure you wish to delete the entry" + "\n");
                    Console.Write($"mapping {entry.URL} to {entry.IP}? (Y/N)");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Y:
                            hosts.hostsEntries.Remove(entry);
                            hosts.SaveHostsFile();
                            Main(hosts);
                            break;
                        case ConsoleKey.N:
                            EntryDetails(entry);
                            break;
                    }
                    break;
                case ConsoleKey.B:
                    Main(hosts);
                    break;
                default:
                    Console.WriteLine("invalid key");
                    EntryDetails(entry);
                    break;
            }
        }
    }
}
