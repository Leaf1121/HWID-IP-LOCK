using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HWID_IP_LOCK
{
    static class Program
    {
        private static string GetHWID()
        {
            RegistryKey registryKey;

            if (Environment.Is64BitOperatingSystem)
            {
                registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            }

            return registryKey.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion").GetValue("ProductId").ToString();
        }

        [STAThread]
        static void Main()
        {

            string userid = GetHWID();
            string[] users = new string[100];
            users[0] = ""; // your hardware id  0 ~ 100
            users[1] = ""; // your hardware id 0 ~ 100
            users[2] = ""; // your hardware id 0 ~ 100

            string[] ip = new string[100];
            ip[0] = "117.110.115.128"; // your are ip address 0 ~ 100
            ip[1] = ""; // your are ip address 0 ~ 100
            ip[2] = ""; // your are ip address 0 ~ 100
            string serverIp;
            try
            {
                string searchIpFromUrl = new System.Net.WebClient().DownloadString(("http://checkip.dyndns.org"));
                string EtcIpInfo = searchIpFromUrl.Substring(searchIpFromUrl.IndexOf("</body>"), searchIpFromUrl.Length - searchIpFromUrl.IndexOf("</body>"));
                serverIp = searchIpFromUrl.Substring(searchIpFromUrl.IndexOf(":") + 1, searchIpFromUrl.Length - searchIpFromUrl.IndexOf(":") - EtcIpInfo.Length - 1).Trim();
            }
            catch
            {
                throw;
            }

            bool hardcheck = false;
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] == GetHWID())
                {
                    hardcheck = true;
                    MessageBox.Show("HWID Correspond");
                    break;
                }
                else
                {
                    MessageBox.Show("HWID is wrong. ");
                    hardcheck = false;
                    return;
                }
            }
            if (hardcheck == true)
                for (int i = 0; i < ip.Length; i++)
                {
                    if (serverIp == ip[i])
                    {
                        MessageBox.Show("License for " + ip[i], "Claude-Agnes17");
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                        break;
                    }
                    else
                    {
                        MessageBox.Show("IP is wrong.");
                        return;
                    }
                }
        }
    }
}
