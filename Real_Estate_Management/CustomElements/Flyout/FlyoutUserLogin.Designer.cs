namespace Contract_Management.Dialogs
{
    partial class FlyoutUserLogin
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.ExitButton = new Telerik.WinControls.UI.RadButton();
            this.OKButton = new Telerik.WinControls.UI.RadButton();
            this.editPanel = new Telerik.WinControls.UI.RadPanel();
            this.McPassWord = new Telerik.WinControls.UI.RadTextBox();
            this.idLabel = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.MCPlan = new Telerik.WinControls.UI.RadMultiColumnComboBox();
            this.carsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.RadIdLabel = new Telerik.WinControls.UI.RadLabel();
            this.MCUser = new Telerik.WinControls.UI.RadMultiColumnComboBox();
            this.driverBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.companiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanel)).BeginInit();
            this.editPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.McPassWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCPlan.EditorControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCPlan.EditorControl.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadIdLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUser.EditorControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUser.EditorControl.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.driverBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companiesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ExitButton.Location = new System.Drawing.Point(15, 316);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(77, 36);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.Text = "إغلاق";
            this.ExitButton.ThemeName = "Material";
            this.ExitButton.Click += new System.EventHandler(this.RadButtonCancel_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(182, 316);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(173, 36);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "دخول";
            this.OKButton.ThemeName = "Material";
            this.OKButton.Click += new System.EventHandler(this.RadButtonOK_Click);
            // 
            // editPanel
            // 
            this.editPanel.Controls.Add(this.McPassWord);
            this.editPanel.Controls.Add(this.idLabel);
            this.editPanel.Controls.Add(this.radLabel2);
            this.editPanel.Controls.Add(this.MCPlan);
            this.editPanel.Controls.Add(this.RadIdLabel);
            this.editPanel.Controls.Add(this.MCUser);
            this.editPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.editPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPanel.Location = new System.Drawing.Point(0, 0);
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size(370, 298);
            this.editPanel.TabIndex = 38;
            this.editPanel.ThemeName = "Material";
            // 
            // McPassWord
            // 
            this.McPassWord.AutoSize = false;
            this.McPassWord.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.McPassWord.Location = new System.Drawing.Point(31, 237);
            this.McPassWord.Name = "McPassWord";
            this.McPassWord.NullText = "ادخل كلمة المرور";
            this.McPassWord.ShowNullText = true;
            this.McPassWord.Size = new System.Drawing.Size(318, 40);
            this.McPassWord.TabIndex = 36;
            // 
            // idLabel
            // 
            this.idLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(265, 206);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(84, 25);
            this.idLabel.TabIndex = 37;
            this.idLabel.Text = "كلمة المرور";
            this.idLabel.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.idLabel.ThemeName = "Material";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(275, 115);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(74, 25);
            this.radLabel2.TabIndex = 35;
            this.radLabel2.Text = "المستخدم";
            this.radLabel2.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.radLabel2.ThemeName = "Material";
            // 
            // MCPlan
            // 
            this.MCPlan.AutoSize = false;
            this.MCPlan.AutoSizeDropDownHeight = true;
            this.MCPlan.AutoSizeDropDownToBestFit = true;
            this.MCPlan.DataSource = this.carsBindingSource;
            this.MCPlan.DisplayMember = "CarName";
            this.MCPlan.DropDownSizingMode = Telerik.WinControls.UI.SizingMode.RightBottom;
            // 
            // MCPlan.NestedRadGridView
            // 
            this.MCPlan.EditorControl.AutoScroll = true;
            this.MCPlan.EditorControl.BackColor = System.Drawing.SystemColors.Window;
            this.MCPlan.EditorControl.EnableCustomFiltering = true;
            this.MCPlan.EditorControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MCPlan.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MCPlan.EditorControl.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.MCPlan.EditorControl.MasterTemplate.AllowAddNewRow = false;
            this.MCPlan.EditorControl.MasterTemplate.AllowCellContextMenu = false;
            this.MCPlan.EditorControl.MasterTemplate.AllowColumnChooser = false;
            this.MCPlan.EditorControl.MasterTemplate.AllowDeleteRow = false;
            this.MCPlan.EditorControl.MasterTemplate.AllowDragToGroup = false;
            this.MCPlan.EditorControl.MasterTemplate.AllowEditRow = false;
            this.MCPlan.EditorControl.MasterTemplate.AllowSearchRow = true;
            this.MCPlan.EditorControl.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.MCPlan.EditorControl.MasterTemplate.DataSource = this.carsBindingSource;
            this.MCPlan.EditorControl.MasterTemplate.EnableCustomFiltering = true;
            this.MCPlan.EditorControl.MasterTemplate.EnableGrouping = false;
            this.MCPlan.EditorControl.MasterTemplate.ShowFilteringRow = false;
            this.MCPlan.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.MCPlan.EditorControl.Name = "NestedRadGridView";
            this.MCPlan.EditorControl.ReadOnly = true;
            this.MCPlan.EditorControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MCPlan.EditorControl.ShowGroupPanel = false;
            this.MCPlan.EditorControl.Size = new System.Drawing.Size(240, 150);
            this.MCPlan.EditorControl.TabIndex = 0;
            this.MCPlan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MCPlan.Location = new System.Drawing.Point(31, 50);
            this.MCPlan.Name = "MCPlan";
            this.MCPlan.NullText = "اختر المخطط";
            this.MCPlan.Size = new System.Drawing.Size(318, 40);
            this.MCPlan.TabIndex = 34;
            this.MCPlan.TabStop = false;
            this.MCPlan.ThemeName = "Material";
            this.MCPlan.ValueMember = "ID";
            // 
            // RadIdLabel
            // 
            this.RadIdLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadIdLabel.Location = new System.Drawing.Point(285, 19);
            this.RadIdLabel.Name = "RadIdLabel";
            this.RadIdLabel.Size = new System.Drawing.Size(64, 25);
            this.RadIdLabel.TabIndex = 2;
            this.RadIdLabel.Text = "المخطط";
            this.RadIdLabel.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.RadIdLabel.ThemeName = "Material";
            // 
            // MCUser
            // 
            this.MCUser.AutoSize = false;
            this.MCUser.AutoSizeDropDownHeight = true;
            this.MCUser.AutoSizeDropDownToBestFit = true;
            this.MCUser.DataSource = this.driverBindingSource;
            this.MCUser.DisplayMember = "DriverName";
            this.MCUser.DropDownSizingMode = Telerik.WinControls.UI.SizingMode.RightBottom;
            // 
            // MCUser.NestedRadGridView
            // 
            this.MCUser.EditorControl.AutoScroll = true;
            this.MCUser.EditorControl.BackColor = System.Drawing.SystemColors.Window;
            this.MCUser.EditorControl.EnableCustomFiltering = true;
            this.MCUser.EditorControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MCUser.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MCUser.EditorControl.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.MCUser.EditorControl.MasterTemplate.AllowAddNewRow = false;
            this.MCUser.EditorControl.MasterTemplate.AllowCellContextMenu = false;
            this.MCUser.EditorControl.MasterTemplate.AllowColumnChooser = false;
            this.MCUser.EditorControl.MasterTemplate.AllowDeleteRow = false;
            this.MCUser.EditorControl.MasterTemplate.AllowDragToGroup = false;
            this.MCUser.EditorControl.MasterTemplate.AllowEditRow = false;
            this.MCUser.EditorControl.MasterTemplate.AllowSearchRow = true;
            this.MCUser.EditorControl.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.MCUser.EditorControl.MasterTemplate.DataSource = this.driverBindingSource;
            this.MCUser.EditorControl.MasterTemplate.EnableCustomFiltering = true;
            this.MCUser.EditorControl.MasterTemplate.EnableGrouping = false;
            this.MCUser.EditorControl.MasterTemplate.ShowFilteringRow = false;
            this.MCUser.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.MCUser.EditorControl.Name = "NestedRadGridView";
            this.MCUser.EditorControl.ReadOnly = true;
            this.MCUser.EditorControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MCUser.EditorControl.ShowGroupPanel = false;
            this.MCUser.EditorControl.Size = new System.Drawing.Size(240, 150);
            this.MCUser.EditorControl.TabIndex = 0;
            this.MCUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MCUser.Location = new System.Drawing.Point(31, 145);
            this.MCUser.Name = "MCUser";
            this.MCUser.NullText = "اختر المستخدم";
            this.MCUser.Size = new System.Drawing.Size(318, 40);
            this.MCUser.TabIndex = 33;
            this.MCUser.TabStop = false;
            this.MCUser.ThemeName = "Material";
            this.MCUser.ValueMember = "ID";
            this.MCUser.SelectedIndexChanged += new System.EventHandler(this.MCUser_SelectedIndexChanged);
            // 
            // FlyoutUserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.editPanel);
            this.Name = "FlyoutUserLogin";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(370, 369);
            this.Load += new System.EventHandler(this.FlyoutAddByanat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanel)).EndInit();
            this.editPanel.ResumeLayout(false);
            this.editPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.McPassWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCPlan.EditorControl.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCPlan.EditorControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadIdLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUser.EditorControl.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUser.EditorControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.driverBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companiesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton ExitButton;
        private Telerik.WinControls.UI.RadLabel RadIdLabel;
        public Telerik.WinControls.UI.RadButton OKButton;
        public Telerik.WinControls.UI.RadMultiColumnComboBox MCUser;
        public Telerik.WinControls.UI.RadPanel editPanel;
        public System.Windows.Forms.BindingSource driverBindingSource;
        public System.Windows.Forms.BindingSource carsBindingSource;
        public System.Windows.Forms.BindingSource companiesBindingSource;
        public Telerik.WinControls.UI.RadMultiColumnComboBox MCPlan;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox McPassWord;
        private Telerik.WinControls.UI.RadLabel idLabel;
    }
}
