
namespace ClearCore
{
    partial class ColorPicker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPicker));
            this.xuiColorPicker1 = new XanderUI.XUIColorPicker();
            this.xuiButton1 = new XanderUI.XUIButton();
            this.SuspendLayout();
            // 
            // xuiColorPicker1
            // 
            this.xuiColorPicker1.Location = new System.Drawing.Point(0, 0);
            this.xuiColorPicker1.Name = "xuiColorPicker1";
            this.xuiColorPicker1.PickerImage = ((System.Drawing.Image)(resources.GetObject("xuiColorPicker1.PickerImage")));
            this.xuiColorPicker1.SelectedColor = System.Drawing.Color.Empty;
            this.xuiColorPicker1.ShowColorPreview = true;
            this.xuiColorPicker1.Size = new System.Drawing.Size(133, 133);
            this.xuiColorPicker1.TabIndex = 8;
            this.xuiColorPicker1.Text = "xuiColorPicker1";
            this.xuiColorPicker1.SelectedColorChanged += new System.EventHandler(this.xuiColorPicker1_SelectedColorChanged);
            // 
            // xuiButton1
            // 
            this.xuiButton1.BackgroundColor = System.Drawing.Color.Transparent;
            this.xuiButton1.ButtonImage = null;
            this.xuiButton1.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.xuiButton1.ButtonText = "Применить";
            this.xuiButton1.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.xuiButton1.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.xuiButton1.CornerRadius = 5;
            this.xuiButton1.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.xuiButton1.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.xuiButton1.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.xuiButton1.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.xuiButton1.Location = new System.Drawing.Point(0, 139);
            this.xuiButton1.Name = "xuiButton1";
            this.xuiButton1.Size = new System.Drawing.Size(133, 25);
            this.xuiButton1.TabIndex = 9;
            this.xuiButton1.TextColor = System.Drawing.Color.DodgerBlue;
            this.xuiButton1.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.xuiButton1.Visible = false;
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(137, 167);
            this.Controls.Add(this.xuiButton1);
            this.Controls.Add(this.xuiColorPicker1);
            this.Name = "ColorPicker";
            this.Text = "ColorPicker";
            this.Load += new System.EventHandler(this.ColorPicker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private XanderUI.XUIColorPicker xuiColorPicker1;
        public XanderUI.XUIButton xuiButton1;
    }
}