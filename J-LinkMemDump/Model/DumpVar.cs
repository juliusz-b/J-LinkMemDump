using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J_LinkMemDump.Model
{
    internal class DumpVar
    {
        public DumpVar()
        {
        }
        public string GetDataFromJlink(string size, string legth)
        {
            //wywolanie komendy jlinka
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            //nrfjprog --memrd 0x20004534 --n 0x4000
            cmd.StandardInput.WriteLine("ping 8.8.8.8");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
           // return betweenStrings(cmd.StandardOutput.ReadToEnd(), ".",".");

            return cmd.StandardOutput.ReadToEnd();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());
           // return cmd.StandardOutput;





           // return "";
        }
        public static String betweenStrings(String text, String start, String end)
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

    }
}
