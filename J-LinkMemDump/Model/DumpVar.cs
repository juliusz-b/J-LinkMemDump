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

            cmd.StandardInput.WriteLine("ping 8.8.8.8");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            return cmd.StandardOutput.ReadToEnd();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());
           // return cmd.StandardOutput;





           // return "";
        }
    }
}
