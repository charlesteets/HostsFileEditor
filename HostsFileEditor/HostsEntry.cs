using System;
using System.Collections.Generic;
using System.Text;

namespace HostsFileEditor
{
    public class HostsEntry
    {
        internal string URL;
        internal string IP;
            #nullable enable
        internal string? comment;

        internal void Edit(ref string ComponentToEdit, string newValue)
        {
            ComponentToEdit = newValue;
        }

        internal HostsEntry (string _URL, string _IP, string _comment)
        {
            URL = _URL;
            IP = _IP;
            comment = _comment;
        }

        internal HostsEntry(string _URL, string _IP)
        {
            URL = _URL;
            IP = _IP;
        }


        internal HostsEntry (string hostsFileLineOfText)
        {
            string[] parameters = hostsFileLineOfText.Split(' ',3);
            if (parameters.Length >= 2) {
                try {
                    IP = parameters[0];
                    URL = parameters[1];
                    if (parameters.Length > 2)
                    {
                        comment = parameters[2];
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            
        }

        internal string FormatForHostsFile()
        {
            StringBuilder sb = new StringBuilder();
                sb.Append(this.IP);
                sb.Append(" ");
                sb.Append(this.URL);
                sb.Append(" ");
                sb.Append(this.comment == null ? "" : "#" + this.comment);
            return sb.ToString();
        }
    }
}
