using Microsoft.Win32;
using PluginsAPI;
using Shell32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearCore.Cleaner
{
    public class CleanerManager
    {
        public CleanerForm form;
        public CleanerManager(CleanerForm form)
        {
            PluginClient.OnPluginPostObject += OnPluginPost;
            this.form = form;
            PluginClient.PluginLoad(CleanerDataBaseUpdater);
        }
        #region Переменные и класы
        public List<CstBut> bts = new List<CstBut>();
        public class CstBut
        {
            public string Type;
            public CheckBox Switch;
            public Label label;
        }
        List<string> buttons_texts = new List<string>();
        #endregion
        #region Работа с очисткой
        public async Task<long> Clear()
        {
            long res = ResycleBinSize();
            long cleared = 0;
            List<Task> tasks = new List<Task>();
            Task cl = Task.Factory.StartNew(() =>
            {
                foreach (CstBut bt in bts)
                {
                    if (bt.Switch.Checked == true)
                    {
                        foreach (CleanerSettings setting in GetByType(bt.Type))
                        {
                            Task t = Task.Factory.StartNew(() =>
                            {
                                if (bt.label.Text == "Корзина")
                                {
                                    cleared += res;
                                }
                                cleared += setting.Clear();
                                bt.Switch.Invoke(new Action(() => { bt.Switch.Checked = false; }));
                                return true;
                            });
                            tasks.Add(t);
                        }
                    }
                }
                foreach (Task t in tasks)
                {
                    t.Wait();
                }
            });
            await cl;
            return cleared;
        }
        public string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //
            if (byteCount == 0) { return "0" + suf[0]; }
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
        #endregion
        #region Работа с UI
        public void UpdateUI()
        {
            int offset = 0;
            form.Invoke(new Action(() => { form.button1.Visible = false; }));
            foreach (CstBut butrem in bts)
            {
                form.Invoke(new Action(() => { form.Controls.Remove(butrem.Switch); }));
                form.Invoke(new Action(() => { form.Controls.Remove(butrem.label); }));
            }
            bts.Clear();
            buttons_texts.Clear();
            //form.Invoke(new Action(() => { form.label1.Text = "БазаДанных: " + Program.mainMenu.cleaner.clearManager.DataBase.Count; }));
            foreach (CleanerSettings st in Program.mainMenu.cleaner.clearManager.DataBase)
            {
                if (!buttons_texts.Contains(st.Type))
                {
                    buttons_texts.Add(st.Type);
                }
            }
            foreach (string cst2 in buttons_texts)
            {
                CheckBox guna2 = new CheckBox();
                guna2.Location = new Point(15, 27 + offset);
                guna2.BackColor = form.BackColor;
                Label text = new Label();
                text.AutoSize = true;
                text.Text = cst2;
                text.ForeColor = Color.White;
                text.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                text.Location = new Point(35, 29 + offset);
                offset += 23;
                CstBut ct = new CstBut();
                ct.Type = cst2;
                ct.Switch = guna2;
                ct.label = text;
                bts.Add(ct);
                form.Invoke(new Action(() => { form.Controls.Add(text); }));
                form.Invoke(new Action(() => { form.Controls.Add(guna2); }));
            }
            form.Invoke(new Action(() => { form.button1.Location = new Point(form.button1.Location.X, 30 + offset); }));
            form.Invoke(new Action(() => { form.button1.Visible = true; }));
        }
        public async void UpdateCheckedUI()
        {
            form.button1.Enabled = false;
            Task cl = Task.Factory.StartNew(() =>
            {
                foreach (CstBut bt in bts)
                {
                    if (bt.Switch.Checked == true) { bt.Switch.Invoke(new Action(() => { bt.Switch.Checked = false; })); }
                    else { bt.Switch.Invoke(new Action(() => { bt.Switch.Checked = true; })); }
                }
            });
            await cl;
            form.button1.Enabled = true;
        }
        #endregion
        #region Работа с базой данных
        private WebScript CleanerDataBaseUpdater = new WebScript("https://nekiplay.000webhostapp.com/public_html/Cleaner/CleanerUpdater.cs");

        private static PluginUpdater PluginUpdater = new PluginUpdater();
        private PluginClient PluginClient = new PluginClient(PluginUpdater);

        public readonly List<CleanerSettings> DataBase = new List<CleanerSettings>();

        public Action OnLoadDone = null;

        private void OnPluginPost(object obj)
        {
            if (obj.GetType() == typeof(CleanerSettings))
            {
                CleanerSettings settings = (CleanerSettings)obj;
                DataBase.Add(settings);
            }
            else if (obj.GetType() == typeof(string))
            {
                string text = (string)obj;
                if (text == "LoadDone")
                {
                    if (OnLoadDone != null)
                    {
                        PluginClient.PluginUnLoad(this.CleanerDataBaseUpdater);
                        OnLoadDone();
                    }
                }
            }
        }

        public void ClearDataBase()
        {
            DataBase.Clear();
            PluginClient.PluginUnLoad(this.CleanerDataBaseUpdater);
        }
        public long ResycleBinSize()
        {
            long bytesdeleted = 0;
            var shell = (Shell32.IShellDispatch4)new Shell32.Shell();
            Folder recycleBin = shell.NameSpace(10);
            foreach (FolderItem2 f in recycleBin.Items())
            {
                switch (f.IsFolder)
                {
                    case false:
                        bytesdeleted += f.Size;
                        break;
                }
            }
            Marshal.FinalReleaseComObject(shell);
            return bytesdeleted;
        }
        public List<CleanerSettings> GetByType(string Type)
        {
            List<CleanerSettings> temp = new List<CleanerSettings>();
            foreach (CleanerSettings cl in DataBase)
            {
                if (cl.Type == Type)
                {
                    temp.Add(cl);
                }
            }
            return temp;
        }
        #endregion
    }
}
