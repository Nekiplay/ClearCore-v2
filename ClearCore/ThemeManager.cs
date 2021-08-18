using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearCore
{
    public class ThemeManager
    {
        public Color BackColor = Color.White;

        public void ApplyColors()
        {
            Program.mainMenu.cleaner.BackColor = BackColor;
            Program.mainMenu.panelDesktop.BackColor = BackColor;
        }
    }
}
