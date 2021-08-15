using System;
using System.Windows.Forms;

namespace ClearCore
{
    public partial class MainMenu : Form
    {
        public static PluginsAPI.PluginUpdater PluginUpdater = new PluginsAPI.PluginUpdater();
        public MainMenu()
        {
            InitializeComponent();
        }
        private Form currentChildForm;
        private string currentChildFormname;
        public void OpenChildForm(Form childForm, bool newform = false)
        {
            if (currentChildForm != childForm && currentChildFormname != childForm.Name)
            {
                if (currentChildForm != null)
                {
                    if (newform)
                    {
                        currentChildForm.Close();
                    }
                    panelDesktop.Controls.Clear();
                }
                currentChildForm = childForm;
                currentChildFormname = childForm.Name;
                childForm.BackColor = panelDesktop.BackColor;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panelDesktop.Controls.Add(childForm);
                panelDesktop.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            BrokenCoreAPI.UserInfo userInfo = new BrokenCoreAPI.UserInfo();
            userInfo.Parse("https://brokencore.club/members/1656/");
            Console.WriteLine(userInfo.Nickname);
            Console.WriteLine(userInfo.Messages);
            Console.WriteLine(userInfo.Resources);
            Console.WriteLine(userInfo.Reactions);
        }
    }
}
