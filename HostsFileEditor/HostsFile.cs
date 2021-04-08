using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace HostsFileEditor
{
    class HostsFile
    {
        public List<HostsEntry> hostsEntries = new List<HostsEntry>();
        internal int currentLineIndex = 0;
        string[] HostFileLines = File.ReadAllLines(HostsFilePath);

        internal void BuildHostsEntries()
        {
            foreach (string line in HostFileLines)
            {
                if (!(line == ""))
                {
                    if (!(line[0] == '#'))
                    { hostsEntries.Add(new HostsEntry(line)); }
                }
            }
        }

        internal void BackupHostsFile()
        {
            if (File.Exists(HostsFilePath + ".bkup"))
            {
                File.Delete(HostsFilePath + ".bkup");
            }
            File.Copy(HostsFilePath, HostsFilePath + ".bkup");
        }

        internal void SaveHostsFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (HostsEntry entry in hostsEntries)
            {
                sb.Append(entry.IP);
                sb.Append(" ");
                sb.Append(entry.URL);
                sb.Append(" #");
                sb.Append(entry.comment);
                sb.Append("\n");
            }
            File.WriteAllText(HostsFilePath, sb.ToString());
        }

        internal HostsFile()
        {
            BuildHostsEntries();
        }

        internal HostsEntry FirstSubstantialLine()
        {
            currentLineIndex = 0;
            return NextEntry();
        }

        internal HostsEntry NextEntry()
        {
            do
            {
                if (currentLineIndex < HostFileLines.Length)
                { currentLineIndex++; }
                Console.WriteLine($"{currentLineIndex}");
            }
            while (HostFileLines[currentLineIndex] == "" || HostFileLines[currentLineIndex][0] == '#');
            return new HostsEntry(HostFileLines[currentLineIndex]);
        }

        internal static string HostsFilePath
        {
            get
            {
                return Environment.SystemDirectory + @"\drivers\etc\hosts";
            }
        }
    }
}
