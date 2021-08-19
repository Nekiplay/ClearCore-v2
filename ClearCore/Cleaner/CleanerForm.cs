using System;
using System.IO;
using System.Windows.Forms;

namespace ClearCore.Cleaner
{
    public partial class CleanerForm : Form
    {
        public CleanerForm()
        {
            InitializeComponent();
        }
        public CleanerManager clearManager = null;
        private void CleanerForm_Load(object sender, EventArgs e)
        {
            clearManager = new CleanerManager(this);
            clearManager.OnLoadDone = () =>
            {
                clearManager.UpdateUI();
            };
        }
        private async void xuiButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                timer1.Stop();
                xuiButton1.Enabled = false;
                xuiButton2.Enabled = false;
                xuiButton3.Enabled = false;
                xuiButton4.Enabled = false;
                xuiCircleProgressBar1.Visible = true;
                NotificationManager.Manager notificationManager = new NotificationManager.Manager();
                long cleared = await clearManager.Clear();
                if (cleared > 0)
                {
                    notificationManager.Alert("Очищено: " + clearManager.BytesToString(cleared), NotificationManager.NotificationType.Success);
                }
                else
                {
                    notificationManager.Alert("Нечего чистить", NotificationManager.NotificationType.Error);
                }

                xuiButton1.Enabled = true;
                xuiButton2.Enabled = true;
                xuiButton3.Enabled = true;
                xuiButton4.Enabled = true;
                timer1.Start();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                clearManager.UpdateCheckedUI();
            }
        }

        private void xuiButton2_Click(object sender, EventArgs e)
        {
            clearManager.ClearDataBase();
            clearManager = new CleanerManager(this);
            clearManager.OnLoadDone = () =>
            {
                clearManager.UpdateUI();
            };
        }

        private void xuiButton3_MouseDown(object sender, MouseEventArgs e)
        {

           
        }

        private void xuiButton3_Click(object sender, EventArgs e)
        {
            string path = "Export.cs";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                string responce = wc.DownloadString(CleanerManager.DataBaseURL);
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(responce);
                }
            }    
        }

        private void xuiButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "C# Script(*.cs)|*.cs";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog.FileName;
            clearManager.LoadCustom(new PluginsAPI.Script(filename));

        }

        private void xuiSegment1_Click(object sender, EventArgs e)
        {

        }

        private void xuiLineGraph1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            xuiCircleProgressBar1.Visible = false;
        }
    }
}
