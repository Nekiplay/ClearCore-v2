
namespace ClearCore
{
    partial class ColorEditForm
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
            this.xuiButton1 = new XanderUI.XUIButton();
            this.xuiRadio1 = new XanderUI.XUIRadio();
            this.SuspendLayout();
            // 
            // xuiButton1
            // 
            this.xuiButton1.BackgroundColor = System.Drawing.Color.Transparent;
            this.xuiButton1.ButtonImage = null;
            this.xuiButton1.ButtonStyle = XanderUI.XUIButton.Style.MaterialRounded;
            this.xuiButton1.ButtonText = "Задний фон";
            this.xuiButton1.ClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.xuiButton1.ClickTextColor = System.Drawing.Color.DodgerBlue;
            this.xuiButton1.CornerRadius = 5;
            this.xuiButton1.Horizontal_Alignment = System.Drawing.StringAlignment.Center;
            this.xuiButton1.HoverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.xuiButton1.HoverTextColor = System.Drawing.Color.DodgerBlue;
            this.xuiButton1.ImagePosition = XanderUI.XUIButton.imgPosition.Left;
            this.xuiButton1.Location = new System.Drawing.Point(12, 12);
            this.xuiButton1.Name = "xuiButton1";
            this.xuiButton1.Size = new System.Drawing.Size(79, 25);
            this.xuiButton1.TabIndex = 6;
            this.xuiButton1.TextColor = System.Drawing.Color.DodgerBlue;
            this.xuiButton1.Vertical_Alignment = System.Drawing.StringAlignment.Center;
            this.xuiButton1.Visible = false;
            // 
            // xuiRadio1
            // 
            this.xuiRadio1.Checked = false;
            this.xuiRadio1.ForeColor = System.Drawing.Color.White;
            this.xuiRadio1.Location = new System.Drawing.Point(83, 251);
            this.xuiRadio1.Name = "xuiRadio1";
            this.xuiRadio1.RadioColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(250)))));
            this.xuiRadio1.RadioHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(55)))), ((int)(((byte)(98)))));
            this.xuiRadio1.RadioStyle = XanderUI.XUIRadio.Style.Material;
            this.xuiRadio1.Size = new System.Drawing.Size(100, 16);
            this.xuiRadio1.TabIndex = 8;
            this.xuiRadio1.Text = "xuiRadio1";
            // 
            // ColorEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.xuiRadio1);
            this.Controls.Add(this.xuiButton1);
            this.Name = "ColorEditForm";
            this.Text = "ColorEditForm";
            this.ResumeLayout(false);

        }

        #endregion

        public XanderUI.XUIButton xuiButton1;
        private XanderUI.XUIRadio xuiRadio1;
    }
}