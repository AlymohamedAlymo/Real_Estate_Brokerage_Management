namespace Real_Estate_Management
{
    partial class AttachmentControl
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.DataGridAttachments = new Telerik.WinControls.UI.RadGridView();
            this.BtnScanner = new Telerik.WinControls.UI.RadButton();
            this.BtnAttachment = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridAttachments.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnScanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.DataGridAttachments);
            this.radPanel2.Controls.Add(this.BtnScanner);
            this.radPanel2.Controls.Add(this.BtnAttachment);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1163, 774);
            this.radPanel2.TabIndex = 11512125;
            // 
            // DataGridAttachments
            // 
            this.DataGridAttachments.AllowShowFocusCues = true;
            this.DataGridAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridAttachments.EnableCodedUITests = true;
            this.DataGridAttachments.EnableCustomDrawing = true;
            this.DataGridAttachments.EnableKeyMap = true;
            this.DataGridAttachments.EnableKineticScrolling = true;
            this.DataGridAttachments.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridAttachments.Location = new System.Drawing.Point(20, 63);
            this.DataGridAttachments.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            // 
            // 
            // 
            this.DataGridAttachments.MasterTemplate.AllowAddNewRow = false;
            this.DataGridAttachments.MasterTemplate.AllowColumnChooser = false;
            this.DataGridAttachments.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.DataGridAttachments.MasterTemplate.AllowColumnReorder = false;
            this.DataGridAttachments.MasterTemplate.AllowColumnResize = false;
            this.DataGridAttachments.MasterTemplate.AllowDeleteRow = false;
            this.DataGridAttachments.MasterTemplate.AllowDragToGroup = false;
            this.DataGridAttachments.MasterTemplate.AllowEditRow = false;
            this.DataGridAttachments.MasterTemplate.AllowRowReorder = true;
            this.DataGridAttachments.MasterTemplate.AllowRowResize = false;
            this.DataGridAttachments.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.DataGridAttachments.MasterTemplate.EnableAlternatingRowColor = true;
            this.DataGridAttachments.MasterTemplate.ShowChildViewCaptions = true;
            this.DataGridAttachments.MasterTemplate.ShowFilteringRow = false;
            this.DataGridAttachments.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.DataGridAttachments.Name = "DataGridAttachments";
            this.DataGridAttachments.ShowChildViewCaptions = true;
            this.DataGridAttachments.ShowGroupPanel = false;
            this.DataGridAttachments.ShowGroupPanelScrollbars = false;
            this.DataGridAttachments.Size = new System.Drawing.Size(1123, 680);
            this.DataGridAttachments.TabIndex = 6;
            this.DataGridAttachments.TitleText = "المرفقات";
            this.DataGridAttachments.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.DataGridAttachments_ContextMenuOpening);
            // 
            // BtnScanner
            // 
            this.BtnScanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnScanner.Enabled = false;
            this.BtnScanner.Location = new System.Drawing.Point(828, 16);
            this.BtnScanner.Name = "BtnScanner";
            this.BtnScanner.Size = new System.Drawing.Size(172, 36);
            this.BtnScanner.TabIndex = 2;
            this.BtnScanner.Text = "جلب من الماسح الضوئي";
            this.BtnScanner.ThemeName = "Material";
            this.BtnScanner.Click += new System.EventHandler(this.Scanner_Click);
            // 
            // BtnAttachment
            // 
            this.BtnAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAttachment.Enabled = false;
            this.BtnAttachment.Location = new System.Drawing.Point(1006, 16);
            this.BtnAttachment.Name = "BtnAttachment";
            this.BtnAttachment.Size = new System.Drawing.Size(120, 36);
            this.BtnAttachment.TabIndex = 1;
            this.BtnAttachment.Text = "إضافة مرفق";
            this.BtnAttachment.ThemeName = "Material";
            this.BtnAttachment.Click += new System.EventHandler(this.AddAttachment_Click);
            // 
            // AttachmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radPanel2);
            this.Name = "AttachmentControl";
            this.Size = new System.Drawing.Size(1163, 774);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridAttachments.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnScanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnAttachment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel2;
        public Telerik.WinControls.UI.RadGridView DataGridAttachments;
        public Telerik.WinControls.UI.RadButton BtnScanner;
        public Telerik.WinControls.UI.RadButton BtnAttachment;
    }
}
