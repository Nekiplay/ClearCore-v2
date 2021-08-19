using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearCore.Optimize
{
    public partial class OptimizeForm : Form
    {
        public OptimizeManager optimizeManager;
        public OptimizeForm()
        {
            InitializeComponent();
        }

        private void OptimizeForm_Load(object sender, EventArgs e)
        {
            optimizeManager = new OptimizeManager(this);
            optimizeManager.OnLoadDone = () =>
            {
                optimizeManager.UpdateUI();
            };

            optimizeManager.OptimizeDone = (optimize) =>
            {
                Console.WriteLine(optimize.Type + " оптимизирована");
            };
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            foreach (OptimizeManager.CstBut bt in optimizeManager.bts)
            {
                if (bt.Switch.Checked == true)
                {
                    foreach (OptimizeSettings setting in optimizeManager.GetByType(bt.Type))
                    {
                        Task<bool> t = new Task<bool>(() =>
                        {
                            optimizeManager.Optimize(setting);
                            if (bt.Switch.Checked == true)
                            {
                                bt.Switch.Invoke(new Action(() => { bt.Switch.Checked = false; }));
                            }
                            return true;
                        });
                        t.Start();
                    }
                }
                else if (bt.Switch.Checked == false)
                {
                    foreach (OptimizeSettings setting in optimizeManager.GetByType(bt.Type))
                    {
                        Task<bool> t = new Task<bool>(() =>
                        {
                            optimizeManager.UnOptimize(setting);
                            if (bt.Switch.Checked == true)
                            {
                                bt.Switch.Invoke(new Action(() => { bt.Switch.Checked = false; }));
                            }
                            return true;
                        });
                        t.Start();
                    }
                }
            }
        }
    }
}
