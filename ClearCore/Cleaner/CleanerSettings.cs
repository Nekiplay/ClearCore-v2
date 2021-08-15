using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClearCore.Cleaner
{
    public class CleanerSettings
    {
        public string Type;
        /* File Settings */
        public string Expansion;

        public string DirectoryPath;
        public Modes Mode;
        public enum Modes
        {
            Files,
            FullAll,
            All,
            RecycleBin,
        }
        public static string SteamPath
        {
            get
            {
                string steamdir = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", "Nothing");
                if (string.IsNullOrEmpty(steamdir) || steamdir == "Nothing")
                {
                    steamdir = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", "Nothing");
                }
                return steamdir;
            }
        }
        public CleanerSettings(string DirectoryPath, string Expansion, string Mode, string Type)
        {
            this.DirectoryPath = DirectoryPath;
            this.DirectoryPath = this.DirectoryPath.Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString());
            this.DirectoryPath = this.DirectoryPath.Replace("%user%", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString());
            this.DirectoryPath = this.DirectoryPath.Replace("%mydocs%", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString());
            this.DirectoryPath = this.DirectoryPath.Replace("%windows%", Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToString());
            this.DirectoryPath = this.DirectoryPath.Replace("%internetcache%", Environment.GetFolderPath(Environment.SpecialFolder.InternetCache).ToString());
            this.DirectoryPath = this.DirectoryPath.Replace("%steam%", SteamPath);
            this.Expansion = Expansion;
            if (Mode == "Files")
            {
                this.Mode = Modes.Files;
            }
            else if (Mode == "All")
            {
                this.Mode = Modes.All;
            }
            else if (Mode == "FullAll")
            {
                this.Mode = Modes.FullAll;
            }
            else if (Mode == "RecycleBin")
            {
                this.Mode = Modes.RecycleBin;
            }
            this.Type = Type;
        }
        public bool IsExists
        {
            get
            {
                if (!string.IsNullOrEmpty(DirectoryPath))
                {
                    if (Directory.Exists(DirectoryPath)) { return true; }
                    else { return false; }
                }
                else { return false; }
            }
        }
        public long deleteFolder(string folder)
        {
            long bytesdeleted = 0;
            if (Directory.Exists(folder))
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(folder);
                    DirectoryInfo[] diA = di.GetDirectories();
                    FileInfo[] fi = di.GetFiles();
                    foreach (FileInfo f in fi)
                    {
                        try { bytesdeleted += f.Length; f.Delete(); }
                        catch { bytesdeleted -= f.Length; }
                    }
                    foreach (DirectoryInfo df in diA)
                    {
                        bytesdeleted += deleteFolder(df.FullName);
                        try { if (df.GetDirectories().Length == 0 && df.GetFiles().Length == 0) df.Delete(); } catch { }
                    }
                }
                catch { }
            }
            return bytesdeleted;
        }
        public long Clear()
        {
            long bytesdeleted = 0;
            if (IsExists)
            {
                switch (Mode)
                {
                    case Modes.Files:
                        var result = System.IO.Directory.EnumerateFiles(DirectoryPath, Expansion);
                        foreach (var m in result)
                        {
                            try
                            {
                                System.IO.FileInfo file = new System.IO.FileInfo(m);
                                try
                                {
                                    bytesdeleted += file.Length;
                                    System.IO.File.Delete(m);
                                }
                                catch
                                {
                                    bytesdeleted -= file.Length;
                                }
                            }
                            catch { }
                        }
                        break;
                    case Modes.All:
                        var result2 = System.IO.Directory.EnumerateFiles(DirectoryPath, Expansion);
                        foreach (var m in result2)
                        {
                            try
                            {
                                System.IO.FileInfo file = new System.IO.FileInfo(m);
                                try
                                {
                                    bytesdeleted += file.Length;
                                    System.IO.File.Delete(m);
                                }
                                catch { bytesdeleted -= file.Length; }
                            }
                            catch { }
                        }
                        break;
                    case Modes.FullAll:
                        bytesdeleted += deleteFolder(DirectoryPath);
                        try { Directory.Delete(DirectoryPath, true); } catch { }
                        break;
                    case Modes.RecycleBin:
                        SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI | RecycleFlags.SHERB_NOSOUND);
                        break;
                }
            }
            return bytesdeleted;
        }
        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);
    }
}
