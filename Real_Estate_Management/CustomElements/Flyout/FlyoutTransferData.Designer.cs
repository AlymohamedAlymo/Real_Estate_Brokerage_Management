﻿namespace Real_Estate_Management.CustomElements
{
    partial class FlyoutTransferData
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
            this.components = new System.ComponentModel.Container();
            this.radWaitingBar1 = new Telerik.WinControls.UI.RadWaitingBar();
            this.dotsLineWaitingBarIndicatorElement1 = new Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement();
            this.radPictureBox2 = new Telerik.WinControls.UI.RadPictureBox();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            this.SuspendLayout();
            // 
            // radWaitingBar1
            // 
            this.radWaitingBar1.Location = new System.Drawing.Point(11, 83);
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
            // radPictureBox2
            // 
            this.radPictureBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPictureBox2.Image = global::Real_Estate_Management.Properties.Resources.data_migration;
            this.radPictureBox2.Location = new System.Drawing.Point(8, 169);
            this.radPictureBox2.Name = "radPictureBox2";
            // 
            // 
            // 
            this.radPictureBox2.RootElement.EnableElementShadow = false;
            this.radPictureBox2.Size = new System.Drawing.Size(414, 90);
            this.radPictureBox2.TabIndex = 4;
            ((Telerik.WinControls.UI.RadPictureBoxElement)(this.radPictureBox2.GetChildAt(0))).Image = global::Real_Estate_Management.Properties.Resources.data_migration;
            ((Telerik.WinControls.UI.RadPictureBoxElement)(this.radPictureBox2.GetChildAt(0))).ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            ((Telerik.WinControls.UI.RadPictureBoxElement)(this.radPictureBox2.GetChildAt(0))).Margin = new System.Windows.Forms.Padding(25, 0, 25, 0);
            // 
            // radLabel3
            // 
            this.radLabel3.AutoSize = false;
            this.radLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.ForeColor = System.Drawing.Color.White;
            this.radLabel3.Location = new System.Drawing.Point(8, 8);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(414, 56);
            this.radLabel3.TabIndex = 9;
            this.radLabel3.Text = "جاري تحديث قاعدة البيانات";
            this.radLabel3.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FlyoutTransferData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(131)))), ((int)(((byte)(161)))));
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radPictureBox2);
            this.Controls.Add(this.radWaitingBar1);
            this.Name = "FlyoutTransferData";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(430, 267);
            this.Load += new System.EventHandler(this.FlyoutTransferData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadWaitingBar radWaitingBar1;
        private Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement dotsLineWaitingBarIndicatorElement1;
        private Telerik.WinControls.UI.RadPictureBox radPictureBox2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private System.Windows.Forms.Timer timer1;
    }
}
