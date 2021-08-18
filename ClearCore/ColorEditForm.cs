using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearCore
{
    public partial class ColorEditForm : Form
    {
        public ColorEditForm()
        {
            InitializeComponent();
        }

        private void xuiColorPicker1_SelectedColorChanged(object sender, EventArgs e)
        {
            ThemeManager theme = Program.mainMenu.themeManager;
            theme.BackColor = xuiColorPicker1.SelectedColor;
            theme.ApplyColors();
        }
    }
}
