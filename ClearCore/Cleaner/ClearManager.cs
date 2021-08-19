using PluginsAPI;
using Shell32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
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
            public XanderUI.XUICheckBox Switch;
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
                            Task<bool> t = new Task<bool>(() =>
                            {
                                if (bt.label.Text == "Корзина")
                                {
                                    cleared += res;
                                }
                                cleared += setting.Clear();
                                if (bt.Switch.Checked == true)
                                {
                                    bt.Switch.Invoke(new Action(() => { bt.Switch.Checked = false; }));
                                }
                                return true;
                            });
                            tasks.Add(t);
                        }
                    }
                }
                int temp = 0;
                foreach (Task t in tasks)
                {
                    t.Start();
                    t.Wait();
                    temp++;
                    form.xuiCircleProgressBar1.Invoke(new Action(() => { form.xuiCircleProgressBar1.Percentage = GetPercent(tasks.Count, temp); }));
                }
            });
            await cl;
            return cleared;
        }
        public int GetPercent(int b, int a)
        {
            if (b == 0) return 0;

            return (int)(a / (b / 100M));
        }
        public int GetPercent(long b, long a)
        {
            if (b == 0) return 0;

            return (int)(a / (b / 100M));
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
            foreach (CstBut butrem in bts)
            {
                form.Invoke(new Action(() => { form.Controls.Remove(butrem.Switch); }));
                form.Invoke(new Action(() => { form.Controls.Remove(butrem.label); }));
            }
            bts.Clear();
            buttons_texts.Clear();
            form.Invoke(new Action(() => { form.label1.Visible = true; form.label1.Text = "БазаДанных: " + Program.mainMenu.cleaner.clearManager.DataBase.Count; }));
            foreach (CleanerSettings st in Program.mainMenu.cleaner.clearManager.DataBase)
            {
                if (!buttons_texts.Contains(st.Type))
                {
                    buttons_texts.Add(st.Type);
                }
            }
            foreach (string cst2 in buttons_texts)
            {
                XanderUI.XUICheckBox guna2 = new XanderUI.XUICheckBox();
                guna2.Location = new Point(15, 27 + offset);
                guna2.Size = new Size(30, 20);
                guna2.CheckboxStyle = XanderUI.XUICheckBox.Style.iOS;
                Label text = new Label();
                text.AutoSize = true;
                text.Text = cst2;
                text.BackColor = Color.Transparent;
                text.ForeColor = Color.Black;
                text.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                text.Location = new Point(35, 29 + offset);
                offset += 22;
                CstBut ct = new CstBut();
                ct.Type = cst2;
                ct.Switch = guna2;
                ct.label = text;
                bts.Add(ct);
                form.Invoke(new Action(() => { form.Controls.Add(text); }));
                form.Invoke(new Action(() => { form.Controls.Add(guna2); }));
            }
            form.Invoke(new Action(() => { form.xuiButton1.Location = new Point(form.xuiButton1.Location.X, 30 + offset); }));
            form.Invoke(new Action(() => { form.xuiButton2.Location = new Point(form.xuiButton2.Location.X, 30 + offset); }));
            form.Invoke(new Action(() => { form.xuiButton3.Location = new Point(form.xuiButton3.Location.X, 30 + offset); }));
            form.Invoke(new Action(() => { form.xuiButton4.Location = new Point(form.xuiButton4.Location.X, 30 + offset); }));
            form.Invoke(new Action(() => { form.xuiCircleProgressBar1.Location = new Point(form.xuiButton1.Location.X, 70 + offset); }));
            form.Invoke(new Action(() => { form.xuiButton1.Visible = true; }));
            form.Invoke(new Action(() => { form.xuiButton2.Visible = true; }));
            form.Invoke(new Action(() => { form.xuiButton3.Visible = true; }));
            form.Invoke(new Action(() => { form.xuiButton4.Visible = true; }));
        }
        public async void UpdateCheckedUI()
        {
            form.xuiButton1.Enabled = false;
            Task cl = Task.Factory.StartNew(() =>
            {
                foreach (CstBut bt in bts)
                {
                    bt.Switch.Invoke(new Action(() => { bt.Switch.Checked = !bt.Switch.Checked; }));
                }
            });
            await cl;
            form.xuiButton1.Enabled = true;
        }
        #endregion
        #region Работа с базой данных
        public static readonly string DataBaseURL = "https://nekiplay.000webhostapp.com/public_html/Cleaner/CleanerUpdater.cs";
        public Plugin CleanerDataBaseUpdater = new WebScript(DataBaseURL);

        private static PluginUpdater PluginUpdater = new PluginUpdater();
        private PluginClient PluginClient = new PluginClient(PluginUpdater);

        public readonly List<CleanerSettings> DataBase = new List<CleanerSettings>();

        public Action OnLoadDone = null;
        public void LoadCustom(Plugin plugin)
        {
            CleanerDataBaseUpdater = plugin;
            ClearDataBase();
            PluginClient.PluginLoad(CleanerDataBaseUpdater);
            UpdateUI();
        }
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
                    PluginClient.PluginUnLoad(this.CleanerDataBaseUpdater);
                    if (OnLoadDone != null)
                    {
                        OnLoadDone();
                    }
                }
            }
        }

        public void ClearDataBase()
        {
            foreach (CstBut butrem in bts)
            {
                form.Invoke(new Action(() => { form.Controls.Remove(butrem.Switch); }));
                form.Invoke(new Action(() => { form.Controls.Remove(butrem.label); }));
            }
            DataBase.Clear();
            bts.Clear();
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
