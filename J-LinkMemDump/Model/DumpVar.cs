using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace J_LinkMemDump.Model
{
    internal static class DumpVar
    {
        public static async Task<string> GetDataFromJlinkAsync(string size, string address)
        {
            //wywolanie komendy jlinka
            var cmd = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            cmd.Start();
            cmd.StandardInput.WriteLine($"nrfjprog --memrd {address} --n {size}");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            //cmd.WaitForExit();
            var str = await ParseInput(cmd);
            str = str.Replace(" ", string.Empty);
            str = SwapBits(str);
            str = ConvertHex(str);
            return str;
        }

        private static async Task<string> ParseInput(Process p)
        {
            var ret = "";
            while (true)
            {
                var line = await p.StandardOutput.ReadLineAsync();
                if (line == null)
                    return ret;
                ret += BetweenStrings(line, ":", "|");
            }
        }

        private static string BetweenStrings(string text, string start, string end)
        {
            var p1 = text.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var p2 = text.IndexOf(end, p1, StringComparison.Ordinal);
            return p2 == -1 ? "" : text.Substring(p1, p2 - p1);
        }

        private static string SwapBits(string hexString)
        {
            try
            {
                var swapped = string.Empty;
                for (var i = 0; i < hexString.Length; i += 8)
                {
                    i--;
                    swapped += hexString[i + 7];
                    swapped += hexString[i + 8];
                    swapped += hexString[i + 5];
                    swapped += hexString[i + 6];
                    swapped += hexString[i + 3];
                    swapped += hexString[i + 4];
                    swapped += hexString[i + 1];
                    swapped += hexString[i + 2];
                    i++;
                }
                return swapped;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }

        private static string ConvertHex(string hexString)
        {
            try
            {
                var ascii = string.Empty;
                for (var i = 0; i < hexString.Length; i += 2)
                {
                    var hs = hexString.Substring(i, 2);
                    var decval = Convert.ToUInt32(hs, 16);
                    var character = Convert.ToChar(decval);
                    ascii += character;
                }
                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }
    }
}