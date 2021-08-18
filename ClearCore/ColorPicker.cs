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
    public partial class ColorPicker : Form
    {
        public ColorPicker()
        {
            InitializeComponent();
            this.BackColor = Program.mainMenu.themeManager.BackColor;
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {

        }

        private void xuiColorPicker1_SelectedColorChanged(object sender, EventArgs e)
        {

        }
    }
}
