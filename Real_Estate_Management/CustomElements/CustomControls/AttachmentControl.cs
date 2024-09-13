using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using DoctorHelper.Helpers;

namespace Real_Estate_Management
{
    public partial class AttachmentControl : UserControl
    {

        [Browsable(true)]
        [Category("Action")]
        [Description("Preview Attachments Button Click")]
        public event EventHandler MenuPreviewAttach_Click;

        [Browsable(true)]
        [Category("Action")]
        [Description("Extract Attachments Button Click")]
        public event EventHandler MenuExtractAttachment_Click;

        [Browsable(true)]
        [Category("Action")]
        [Description("Delete Attachments Button Click")]
        public event EventHandler MenuDeleteAttachment_Click;

        [Browsable(true)]
        [Category("Action")]
        [Description("Add Attachments Button Click")]
        public event EventHandler BtnAddAttachment_Click;

        [Browsable(true)]
        [Category("Action")]
        [Description("Scanner Attachments Button Click")]
        public event EventHandler BtnScanner_Click;



        public RadContextMenu AttachmentsContextMenu = new RadContextMenu();

        public AttachmentControl()
        {
            InitializeComponent();

            this.radPanel2.PanelElement.PanelFill.Visibility = ElementVisibility.Hidden;
            this.radPanel2.BackgroundImage = Real_Estate_Management.Properties.Resources.Background;
            this.radPanel2.BackgroundImageLayout = ImageLayout.Stretch;

            //this.DataGridAttachments.AutoGenerateHierarchy = true;
            this.DataGridAttachments.TableElement.CellSpacing = 10;
            this.DataGridAttachments.RootElement.EnableElementShadow = false;
            this.DataGridAttachments.GridViewElement.DrawFill = false;
            this.DataGridAttachments.TableElement.Margin = new Padding(15, 0, 15, 0);
            this.DataGridAttachments.BackColor = Color.Transparent;
            this.DataGridAttachments.GridViewElement.DrawFill = true;
            this.DataGridAttachments.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            RadGridLocalizationProvider.CurrentProvider = new MyArabicRadGridLocalizationProvider();
            DataGridAttachments.TableElement.UpdateView();

            RadMenuItem customMenuItem = new RadMenuItem
            {
                Text = "معاينة - تشغيل"
            };
            AttachmentsContextMenu.Items.Add(customMenuItem);
            RadMenuItem customMenuItem2 = new RadMenuItem
            {
                Text = "إستخراج"
            };
            AttachmentsContextMenu.Items.Add(customMenuItem2);

            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            AttachmentsContextMenu.Items.Add(separator);
            RadMenuItem customMenuItem3 = new RadMenuItem
            {
                Text = "حذف",
                Enabled = false
            };
            AttachmentsContextMenu.Items.Add(customMenuItem3);

            customMenuItem.Click -= PreviewAttach_Click;
            customMenuItem.Click += PreviewAttach_Click;
            customMenuItem2.Click -= ExtractAttachment_Click;
            customMenuItem2.Click += ExtractAttachment_Click;
            customMenuItem3.Click -= DeleteAttachment_Click;
            customMenuItem3.Click += DeleteAttachment_Click;


        }
        #region Attach

        public void FillDataGridAttachments(Guid parentguid)
        {


            tbAttachment.Fill("ParentGuid", parentguid);
            DataGridAttachments.DataSource = tbAttachment.lstData;

            DataGridAttachments.Columns[0].IsVisible = false;
            DataGridAttachments.Columns[1].IsVisible = false;
            DataGridAttachments.Columns[2].IsVisible = false;
            DataGridAttachments.Columns[5].IsVisible = false;

            DataGridAttachments.Columns[3].HeaderText = "اسم الملف";
            DataGridAttachments.Columns[4].HeaderText = "الحجم";

            this.DataGridAttachments.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


            foreach (GridViewDataColumn col in this.DataGridAttachments.Columns)
            {
                col.TextAlignment = ContentAlignment.MiddleCenter;
                col.HeaderTextAlignment = ContentAlignment.MiddleCenter;
            }

        }
        public void AddAttachments(Guid parentguid)
        {

            foreach (Telerik.WinControls.UI.GridViewRowInfo dr in DataGridAttachments.Rows)
            {
                tbAttachment tbAttach = new tbAttachment
                {
                    Guid = new Guid(dr.Cells[0].Value.ToString())
                };
                if (!tbAttachment.IsExistTrans(tbAttach.Guid, parentguid))
                {
                    dr.Cells[0].Value = tbAttach.Guid = Guid.NewGuid();
                    tbAttach.ParentGuid = parentguid;
                    tbAttach.Name = dr.Cells[2].Value.ToString();
                    tbAttach.FileName = dr.Cells[3].Value.ToString();
                    tbAttach.FileSize = dr.Cells[4].Value.ToString();
                    tbAttach.FileData = (byte[])dr.Cells[5].Value;

                    tbAttach.Insert();
                }
            }

            UpdateAttachments(parentguid);
        }

        private void UpdateAttachments(Guid parentguid)
        {

            tbAttachment.FillTrans(parentguid);
            foreach (tbAttachment attach in tbAttachment.lstData)
            {
                if (!IsAttachExist(attach.Guid))
                {
                    tbAttachment tbAttach = new tbAttachment();
                    tbAttach.DeleteBy("Guid", attach.Guid);
                }

            }
        }

        bool IsAttachExist(Guid guid)
        {
            foreach (Telerik.WinControls.UI.GridViewRowInfo dr in DataGridAttachments.Rows)
            {
                Guid AttachGuid = new Guid(dr.Cells[0].Value.ToString());
                if (AttachGuid.Equals(guid))
                    return true;
            }
            return false;
        }

        private void DataGridAttachments_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (!(e.ContextMenu is RadDropDownMenu)) { return; }
            e.ContextMenu = AttachmentsContextMenu.DropDown;
        }


        private void PreviewAttach_Click(object sender, EventArgs e)
        {

            MenuPreviewAttach_Click?.Invoke(this, e);

        }
        private void ExtractAttachment_Click(object sender, EventArgs e)
        {

            MenuExtractAttachment_Click?.Invoke(this, e);

        }
        private void DeleteAttachment_Click(object sender, EventArgs e)
        {

            MenuDeleteAttachment_Click?.Invoke(this, e);

        }

        private void AddAttachment_Click(object sender, EventArgs e)
        {
            BtnAddAttachment_Click?.Invoke(this, e);

        }

        private void Scanner_Click(object sender, EventArgs e)
        {
            BtnScanner_Click?.Invoke(this, e);

        }



        #endregion

    }
}
