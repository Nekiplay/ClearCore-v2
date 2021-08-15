using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private async void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NotificationManager.Manager manager = new NotificationManager.Manager();
                button1.Enabled = false;
                long cleared = await clearManager.Clear();
                if (cleared > 0)
                {
                    manager.Alert("Очищено: " + clearManager.BytesToString(cleared), NotificationManager.NotificationType.Success);
                }
                else
                {
                    manager.Alert("Нечего чистить", NotificationManager.NotificationType.Error);
                }
                button1.Enabled = true;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                clearManager.UpdateCheckedUI();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
