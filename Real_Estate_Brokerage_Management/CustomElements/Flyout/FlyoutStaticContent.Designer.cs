namespace DoctorERP
{
    partial class FlyoutStaticContent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radWaitingBar1 = new Telerik.WinControls.UI.RadWaitingBar();
            this.dotsLineWaitingBarIndicatorElement1 = new Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement();
            this.radPictureBox1 = new Telerik.WinControls.UI.RadPictureBox();
            this.radPictureBox2 = new Telerik.WinControls.UI.RadPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // radWaitingBar1
            // 
            this.radWaitingBar1.Location = new System.Drawing.Point(11, 152);
            this.radWaitingBar1.Name = "radWaitingBar1";
            this.radWaitingBar1.Padding = new System.Windows.Forms.Padding(100, 0, 100, 0);
            this.radWaitingBar1.Size = new System.Drawing.Size(408, 58);
            this.radWaitingBar1.TabIndex = 0;
            this.radWaitingBar1.Text = "radWaitingBar1";
            this.radWaitingBar1.WaitingIndicators.Add(this.dotsLineWaitingBarIndicatorElement1);
            this.radWaitingBar1.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsLine;
            // 
            // dotsLineWaitingBarIndicatorElement1
            // 
            this.dotsLineWaitingBarIndicatorElement1.ElementColor = System.Drawing.Color.White;
            this.dotsLineWaitingBarIndicatorElement1.Name = "dotsLineWaitingBarIndicatorElement1";
            // 
            // radPictureBox1
            // 
            this.radPictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPictureBox1.Image = global::DoctorERP.Properties.Resources.add;
            this.radPictureBox1.ImageLayout = Telerik.WinControls.UI.RadImageLayout.Center;
            this.radPictureBox1.Location = new System.Drawing.Point(8, 8);
            this.radPictureBox1.Name = "radPictureBox1";
            // 
            // 
            // 
            this.radPictureBox1.RootElement.EnableElementShadow = false;
            this.radPictureBox1.Size = new System.Drawing.Size(414, 138);
            this.radPictureBox1.TabIndex = 3;
            // 
            // radPictureBox2
            // 
            this.radPictureBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPictureBox2.Image = global::DoctorERP.Properties.Resources.agreement16;
            this.radPictureBox2.Location = new System.Drawing.Point(8, 216);
            this.radPictureBox2.Name = "radPictureBox2";
            // 
            // 
            // 
            this.radPictureBox2.RootElement.EnableElementShadow = false;
            this.radPictureBox2.Size = new System.Drawing.Size(414, 46);
            this.radPictureBox2.TabIndex = 4;
            ((Telerik.WinControls.UI.RadPictureBoxElement)(this.radPictureBox2.GetChildAt(0))).Image = global::DoctorERP.Properties.Resources.broken_link;
            ((Telerik.WinControls.UI.RadPictureBoxElement)(this.radPictureBox2.GetChildAt(0))).ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            ((Telerik.WinControls.UI.RadPictureBoxElement)(this.radPictureBox2.GetChildAt(0))).Margin = new System.Windows.Forms.Padding(25, 0, 25, 0);
            // 
            // FlyoutStaticContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(131)))), ((int)(((byte)(161)))));
            this.Controls.Add(this.radPictureBox2);
            this.Controls.Add(this.radPictureBox1);
            this.Controls.Add(this.radWaitingBar1);
            this.Name = "FlyoutStaticContent";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(430, 270);
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadWaitingBar radWaitingBar1;
        private Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement dotsLineWaitingBarIndicatorElement1;
        private Telerik.WinControls.UI.RadPictureBox radPictureBox1;
        private Telerik.WinControls.UI.RadPictureBox radPictureBox2;
    }
}
