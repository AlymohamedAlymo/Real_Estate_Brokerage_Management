using DoctorERP.CustomElements;
using DoctorERP.Helpers;
using DoctorHelper.Helpers;
using Helper.Helpers;
using Real_Estate_Management.Data;
using Real_Estate_Management.Data.DataBase;
using SmartArabXLSX.Vml.Office;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.SplashScreen;
using Telerik.Windows.Diagrams.Core;

namespace DoctorERP.User_Controls
{
    public partial class UCLawyer : UserControl
    {

        private Guid CurrentGuid;
        private bool IsLoad = true, IsNew = false, IsProgrammatic = false, IsDirty = false, ShowConfirmMSG = true;
        private BindingSource Bs;
        //private bool IsDirty = false, IsLoad = true;
        //private bool IsNew = false, ShowConfirmMSG = true, IsProgrammatic = false;
        private int CurrentPosition = 0;
        //private readonly tbAgent AddedAgents = new tbAgent();
        private readonly DoctorHelper.Messages.Messages MessagesHelper = new DoctorHelper.Messages.Messages();
        private static readonly RadCallout callout = new RadCallout();

        public UCLawyer(Guid _guid)
        {

            InitializeComponent();

            #region Initialize

            BtnSentToPrinter.Click -= BtnPrint_Click;
            BtnSentToPrinter.Click += BtnPrint_Click;
            BtnPreview.Click -= MenuPreview_Click;
            BtnPreview.Click += MenuPreview_Click;
            BtnDesign.Click -= MenuDesign_Click;
            BtnDesign.Click += MenuDesign_Click;
            BtnImportExcel.Click -= BtnImport_Click;
            BtnImportExcel.Click += BtnImport_Click;
            BtnEcelExport.Click -= BtnExportExcel_Click;
            BtnEcelExport.Click += BtnExportExcel_Click;
            BtnPdfExport.Click -= BtnExportPdf_Click;
            BtnPdfExport.Click += BtnExportPdf_Click;
            BtnEmailExport.Click -= BtnSendEmail_Click;
            BtnEmailExport.Click += BtnSendEmail_Click;
            BtnWordExport.Click -= BtnExportWord_Click;
            BtnWordExport.Click += BtnExportWord_Click;
            MenuPreviewAttachment.Click -= MenuPreviewAttach_Click;
            MenuPreviewAttachment.Click += MenuPreviewAttach_Click;
            MenuExtractAttachement.Click -= MenuExtractAttachment_Click;
            MenuExtractAttachement.Click += MenuExtractAttachment_Click;
            MenuDeleteAttachment.Click -= MenuDeleteAttachment_Click;
            MenuDeleteAttachment.Click += MenuDeleteAttachment_Click;
            BtnExportExcelData.Click -= BtnExportExcelData_Click;
            BtnExportExcelData.Click += BtnExportExcelData_Click;
            BtnSaleOrder.Click -= MenuSaleOrder_Click;
            BtnSaleOrder.Click += MenuSaleOrder_Click;
            BtnContract.Click -= BtnContract_Click;
            BtnContract.Click += BtnContract_Click;
            BtnReservation.Click -= BtnReservation_Click;
            BtnReservation.Click += BtnReservation_Click;

            RadFlyoutManager.FlyoutClosed -= this.RadFlyoutManager_FlyoutClosed;
            RadFlyoutManager.FlyoutClosed += this.RadFlyoutManager_FlyoutClosed;

            radLabel5.TextAlignment = ContentAlignment.MiddleCenter;
            TxtNumber.TextAlignment = ContentAlignment.MiddleCenter;
            TxtStatues.TextAlignment = ContentAlignment.MiddleCenter;
            radlabelBookings.TextAlignment = ContentAlignment.MiddleCenter;
            radLabel8.TextAlignment = ContentAlignment.MiddleCenter;

            radDesktopAlert1.Popup.RootElement.RightToLeft = true;
            radPageView1.RootElement.Children[0].Children[0].Children[0].Children[2].Visibility = ElementVisibility.Hidden;
            this.MainContainer.PanelElement.PanelFill.Visibility = ElementVisibility.Hidden;
            this.MainContainer.BackgroundImage = Real_Estate_Management.Properties.Resources.Background;
            this.MainContainer.BackgroundImageLayout = ImageLayout.Stretch;
            this.radPanel2.PanelElement.PanelFill.Visibility = ElementVisibility.Hidden;
            this.radPanel2.BackgroundImage = Real_Estate_Management.Properties.Resources.Background;
            this.radPanel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.DataGridAttachments.AutoGenerateHierarchy = true;
            this.DataGridAttachments.TableElement.CellSpacing = 10;
            this.DataGridAttachments.RootElement.EnableElementShadow = false;
            this.DataGridAttachments.GridViewElement.DrawFill = false;
            this.DataGridAttachments.TableElement.Margin = new Padding(15, 0, 15, 0);
            this.DataGridAttachments.BackColor = Color.Transparent;
            this.DataGridAttachments.GridViewElement.DrawFill = true;
            this.DataGridAttachments.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            RadGridLocalizationProvider.CurrentProvider = new MyArabicRadGridLocalizationProvider();
            DataGridAttachments.TableElement.UpdateView();

            var radBarButton = new[] { BtnResfresh, BtnDelete, BtnNew, BtnEdit, BtnExit };
            radBarButton.ForEach(control =>
            {
                control.ScreenTip = new RadOffice2007ScreenTipElement
                {
                    CaptionLabel = { Text = control.Text },
                    MainTextLabel = { Text = control.Tag.ToString() }
                };
            });

            var radDropDownButton = new[] { BtnImport, BtnExport, BtnPrint, BtnOperation };
            radDropDownButton.ForEach(control =>
            {
                control.ScreenTip = new RadOffice2007ScreenTipElement
                {
                    CaptionLabel = { Text = control.Text },
                    MainTextLabel = { Text = control.Tag.ToString() }
                };
            });

            var radMenuItem = new[] { BtnImportExcel, BtnExportExcelData, BtnEcelExport, BtnEmailExport, BtnPdfExport, BtnWordExport, BtnSentToPrinter, BtnDesign, BtnPreview, BtnContract, BtnSaleOrder, BtnReservation, BtnPriceOffer };
            radMenuItem.ForEach(control =>
            {
                control.ScreenTip = new RadOffice2007ScreenTipElement
                {
                    CaptionLabel = { Text = control.Text },
                    MainTextLabel = { Text = control.Tag.ToString() }
                };
            });
            radPageView1.SelectedPage = PageHome;


            callout.ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle;
            callout.ArrowDirection = Telerik.WinControls.ArrowDirection.Right;
            callout.AutoClose = true;
            callout.CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle;
            callout.DropShadow = true;

            #endregion

            CurrentGuid = _guid;

            SetData();

        }

        #region Binding

        private void SetData()
        {
            IsLoad = true;

            if (FrmMain.PlanGuid != Guid.Empty)
            {
                TbLawyer_Rep.Fill("PlanGuid", FrmMain.PlanGuid);
            }
            else
            {
                TbLawyer_Rep.Fill();
            }

            Bs = new BindingSource(TbLawyer_Rep.dtData, string.Empty);
            BindingNavigatorClient.BindingSource = Bs;

            TbPlans_Rep.Fill();
            CmbPlanGuid.DataSource = TbPlans_Rep.dtData;
            CmbPlanGuid.ValueMember = "Guid";
            CmbPlanGuid.DisplayMember = "Name";

            if (!FrmMain.PlanGuid.Equals(Guid.Empty))
            {
                CmbPlanGuid.SelectedValue = FrmMain.PlanGuid;
            }
            else
            {
                CmbPlanGuid.SelectedIndex = 0;
            }
            var controls = MainContainer.Controls.OfType<RadControl>().Where(control => !control.Name.StartsWith("rad"));
            foreach (var control in controls)
            {
                control.DataBindings.Clear();
                string dataMember = control.Name.Remove(0, 3);
                string propertyName = control is RadSpinEditor ? "Value" : control is RadCheckBox ? "Checked"
                    : control is RadMultiColumnComboBox ? "SelectedValue" : "Text";
                Binding ControlBinding = new System.Windows.Forms.Binding(propertyName, Bs, dataMember);
                control.DataBindings.Add(ControlBinding);
            }

            if (IsNew)
            {
                BtnNew.PerformClick();
                return;
            }

            if (!CurrentGuid.Equals(Guid.Empty))
            {
                var lawyers = TbLawyer_Rep.lstData.Where(u => u.Guid == CurrentGuid).ToList();
                if (lawyers.Any())
                {
                    var index = TbLawyer_Rep.lstData.IndexOf(lawyers.First());
                    Bs.Position = index;
                    return;
                }
            }

            Bs.MoveLast();

            IsLoad = false;
        }

        private void Bs_PositionChanged(object sender, EventArgs e)
        {
            if (IsLoad) { return; }
            DataRowView dataRow = (DataRowView)Bs.Current;
            if (Bs.Current == null) { return; }
            tbLawyer obj = (tbLawyer)Bs.Current;
            if (obj.Guid.Equals(Guid.Empty)) { return; }
            CurrentPosition = Bs.Position;

            if (IsDirty && !IsProgrammatic)
            {
                if (IsNew)
                {
                    Bs.MoveLast();
                }

                IsProgrammatic = true;
                Bs.Position = CurrentPosition - 1;
                TackAction();
                IsProgrammatic = false;

            }
            BtnReservation.Text = obj.Statues == "غير نشط" ? "تنشيط" : "إلغاء التنشيط";
            FillDataGridAttachments(obj.Guid);
            BtnDelete.Enabled = true;
            BtnEdit.Enabled = true;
            SetReadOnly(true);

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

            switch (BtnNew.Text)
            {
                case "جديد":
                    if (!Check("إضافة جديد", "إضافة بطاقة محامي جديدة", OperationType.OperationIs.Add, true)) { return; }
                    NewFill();
                    break;
                case "حفظ":
                    Add();
                    break;
            }

        }
        private void NewFill()
        {
            IsNew = true;
            IsDirty = true;
            CurrentGuid = Guid.Empty;
            BtnAttachment.Enabled = true;
            BtnScanner.Enabled = true;
            BtnDelete.Enabled = false;
            BtnEdit.Enabled = false;
            radPageView1.SelectedPage = PageHome;
            BtnNew.Image = Real_Estate_Management.Properties.Resources.BtnConform;
            BtnNew.Text = "حفظ";
            BtnNew.ScreenTip.Text = "حفظ بطاقة المحامي الجديدة";
            BtnReservation.Text = "إلغاء التنشيط";

            Bs.AddNew();
            Bs.MoveLast();

            FillDataGridAttachments(Guid.Empty);
            SetReadOnly(false);

            foreach (RadControl control in MainContainer.Controls)
            {
                if (control is RadSpinEditor spinEditor)
                {
                    spinEditor.Value = 0;
                }
                else if (control is RadTextBox textControl)
                {
                    textControl.Text = string.Empty;
                }
                if (!control.Name.StartsWith("rad"))
                {
                    control.DataBindings.Clear();
                }
            }

            TxtStatues.Text = "نشط";
            TxtNumber.Text = (TbLawyer_Rep.GetMaxNumber("Number") + 1).ToString();
            CmbPlanGuid.SelectedValue = FrmMain.PlanGuid;

            TxtName.Focus();
        }

        private void Add()
        {
            if (ShowConfirmMSG && !MessagesHelper.MessageWarning("هل أنت متاكد من الإضافة ؟", "إضافة بطاقة محامي جديدة", "إذا ضغت علي زر نعم سوف يتم إضافة بطاقة المحامي الجديدة"))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                RadCallout.Show(callout, TxtName, "ادخل اسم المحامي أولاً", "اسم المحامي غير مدخل", "ثم إضغط علي زر حفظ لحفظ الملف الجديد");
                return;
            }

            Guid _PlanGuid = (Guid)CmbPlanGuid.SelectedValue;
            Guid _Guid = Guid.NewGuid();
            DateTime CurrentDate = DBConnect.GetServerDate();
            string _LastAction = string.Format("عملية إضافة بواسطة المستخدم : {0} -  بتاريخ : {1} - الساعة : {2}",
                FrmMain.CurrentUser.name,
                CurrentDate.ToString("yyyy/MM/dd"),
                CurrentDate.ToString("hh:mm tt"));
            string internedLastAction = string.Intern(_LastAction);


            DBConnect.StartTransAction();
            TbLog_Rep.AddLog(_PlanGuid, "إضافة", "إضافة بطاقة محامي", $"إضافة بطاقة محامي بأسم {TxtName.Text}");
            TbLawyer_Rep.AddLawyer(_PlanGuid, _Guid, int.Parse(TxtNumber.Text), TxtName.Text, TxtMobile.Text, TxtIDNumber.Text, TxtVatNumber.Text, TxtPublicNumber.Text,
                TxtEmail.Text, TxtNote.Text, internedLastAction);
            AddAttachments(_Guid);
            if (DBConnect.CommitTransAction())
            {
                ShowNotification("إضافة بطاقة محامي جديدة", "تمت عملية إضافة بطاقة المحامي بنجاح", internedLastAction);
                ShowDesktopAlert("إضافة بطاقة محامي جديدة", "تمت العملية", "تمت عملية إضافة بطاقة المحامي بنجاح", "بطاقة المحامي الجديدة تمت إضافتها يمكن القيام بالعمليات عليها الأن.");
                //FrmMain.DataHasChanged = true;

                IsDirty = false;
                IsNew = false;
                CurrentGuid = _Guid;
                BtnAttachment.Enabled = false;
                BtnScanner.Enabled = false;
                BtnDelete.Enabled = true;
                BtnEdit.Enabled = true;
                BtnNew.Image = Real_Estate_Management.Properties.Resources.BtnAddNew;
                BtnNew.Text = "جديد";
                BtnNew.ScreenTip.Text = "إضافة بطاقة محامي جديدة";
                BtnReservation.Text = "إلغاء التنشيط";
                SetReadOnly(true);
                SetData();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            switch (BtnNew.Text)
            {
                case "تعديل":
                    if (!Check("تعديل بطاقة", "تعديل بطاقة المحامي", OperationType.OperationIs.Edit, true)) { return; }
                    BtnEdit.Text = "حفظ";
                    BtnEdit.ScreenTip.Text = "حفظ التعديلات";
                    BtnEdit.Image = Real_Estate_Management.Properties.Resources.BtnConform;
                    BtnAttachment.Enabled = true;
                    BtnScanner.Enabled = true;
                    IsDirty = true;
                    CurrentGuid = ((tbLawyer)Bs.Current).Guid;
                    SetReadOnly(false);
                    break;
                case "حفظ":
                    Edit();
                    break;
            }

        }

        private void Edit()
        {

            tbLawyer lawer = (tbLawyer)Bs.Current;
            if (!MessagesHelper.MessageWarning("هل أنت متاكد من التعديل ؟", "تعديل بطاقة محامي", "إذا ضغت علي زر نعم سوف يتم تعديل بيانات بطاقة المحامي"))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                RadCallout.Show(callout, TxtName, "اسم المحامي غير مدخل", "حفظ البيانات", "ثم الضغط علي زر حفظ لحفظ التغييرات");
                return;
            }

            //DataRowView dataRow = (DataRowView)Bs.Current;
            //DataRow lawer = (DataRow)dataRow.Row;

            Guid CurrentGuid = ((tbAgent)Bs.Current).guid;
            tbAgent agent = tbAgent.lstData.Where(u => u.guid == CurrentGuid).FirstOrDefault();

            //agent.agencynumber = Txtagencynumber.Text;
            //agent.agentcivilid = Txtagentcivilid.Text;
            //agent.agentemail = Txtagentemail.Text;
            //agent.agentmobile = Txtagentmobile.Text;
            //agent.agentname = Txtagentname.Text;
            //agent.agentpublicnumber = Txtagentpublicnumber.Text;
            //agent.agenttype = AgentType;
            //agent.agentvatid = Txtagentvatid.Text;
            //agent.civilid = Txtcivilid.Text;
            //agent.email = Txtemail.Text;
            //agent.lastaction = "عملية إضافة" + " - بتاريخ  " + DateTime.Now.ToString("dd/MM/yyyy")
            //+ " - الساعة  " + DateTime.Now.ToString("hh:mm tt") + " - عن طريق المستخدم  " + FrmMain.CurrentUser.name;
            //agent.mobile = Txtmobile.Text;
            //agent.name = Txtname.Text;
            //agent.note = Txtnote.Text;
            //agent.officecr = Txtofficecr.Text;
            //agent.officeemail = Txtofficeemail.Text;
            //agent.officename = Txtofficename.Text;
            //agent.officephone = Txtofficephone.Text;
            //agent.officepublicnumber = Txtofficepublicnumber.Text;
            //agent.officevatid = Txtofficevatid.Text;
            //agent.publicnumber = Txtpublicnumber.Text;
            //agent.vatid = Txtvatid.Text;


            //Agent.planguid = tbPlanInfo.lstData.Where(u => u.name == CmbPlanGuid.Text).FirstOrDefault().guid;

            DBConnect.StartTransAction();
            AddAttachments(agent.guid);
            agent.Update();
            tbLog.AddLog("تعديل", "بطاقة محامي", agent.name.ToString());
            if (DBConnect.CommitTransAction())
            {
                ShowNotification("تعديل بطاقة محامي جديدة", "تمت عملية تعديل بطاقة المحامي بنجاح", "عملية إضافة بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("تعديل بطاقة محامي جديدة", "تمت العملية", "تمت عملية تعديل بطاقة المحامي بنجاح", "تم تعديل بيانات بطاقة المحامي يمكن القيام بالعمليات عليها الأن.");
                //FrmMain.DataHasChanged = true;

                BtnEdit.Text = "تعديل";
                BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة المحامي";
                BtnEdit.Image = Real_Estate_Management.Properties.Resources.BtnEdite;
                BtnAttachment.Enabled = false;
                BtnScanner.Enabled = false;
                SetReadOnly(true);
                IsDirty = false;
                IsNew = false;
                CurrentGuid = lawer.Guid;
                SetData();

            }

            //return true;
        }

        #endregion

        #region Main Events
        #region Default
        private void ShowDesktopAlert(string Header, string Content, string ContentHighlight, string Footer)
        {
            Content = TextHelper.ReverseText(Content);
            ContentHighlight = TextHelper.ReverseText(ContentHighlight);
            Footer = TextHelper.ReverseText(Footer);

            radDesktopAlert1.CaptionText = "<html><b>\n" + Header;
            radDesktopAlert1.ContentText = "<html><i>" +
                Content +
                "</i><b><span><color=Blue>" +
                "\n" + ContentHighlight + "\n" +
                "</span></b>" +
                Footer;
            radDesktopAlert1.ContentImage = Real_Estate_Management.Properties.Resources.information50;
            radDesktopAlert1.Opacity = 0.9f;
            radDesktopAlert1.Show();

        }
        private void ShowNotification(string Header, string Content, string Note)
        {
            radToastNotificationManager1.ToastNotifications[0].Xml = "<toast launch=\"readMoreArg\">\r\n  <visual>\r\n    <binding template=\"ToastGeneric\">\r\n   " +
                "   <text>" + Header + "</text>\r\n   " +
                "   <text>" + Content + "</text>\r\n  " +
                "    <text placement=\"attribution\">" + Note + "</text>\r\n    </binding>\r\n  </visual>\r\n</toast>";
            radToastNotificationManager1.ShowNotification(0);

        }

        #endregion
        private bool Check(string Operation, string Command, OperationType.OperationIs OperType, bool IsAction)
        {
            if (IsDirty) { TackAction(); }
            if (!FrmMain.IsPermissionGranted(Operation))
            {
                MessagesHelper.MessageException(Operation, "لا تملك صلاحية", "لا تملك صلاحية للقيام ب " + Command + " \n تواصل مع المدير.");
                return false;
            }
            if (Bs.Current == null)
            {
                MessagesHelper.MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة المحامي أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            DataRowView dataRow = (DataRowView)Bs.Current;
            DataRow lawyerRow = (DataRow)dataRow.Row;
            Dictionary<bool, string> Check = CheckifOprAllow(lawyerRow, OperType, IsAction);
            if (!Check.Keys.First() && !Operation.Contains("جديد"))
            {
                MessagesHelper.MessageException(Operation, "لا يمكن " + Command, Check.Values.First());
                return false;
            }
            if (lawyerRow is null || lawyerRow.ItemArray[1].Equals(Guid.Empty))
            {
                MessagesHelper.MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة المحامي أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            return true;

        }

        private Dictionary<bool, string> CheckifOprAllow(DataRow DataRow, OperationType.OperationIs operation, bool IsAction)
        {
            bool GoHead = false, IsSoldorInOrder = false;
            Dictionary<bool, string> Pair = new Dictionary<bool, string>();
            switch (operation)
            {
                case OperationType.OperationIs.Add:
                    GoHead = FrmMain.CurrentUser.CanAdd;
                    Pair.Clear();
                    Pair.Add(GoHead, "ليس لديك صلاحية الإضافة");
                    break;
                case OperationType.OperationIs.Edit:
                    GoHead = FrmMain.CurrentUser.CanEdit;
                    Pair.Add(GoHead, "ليس لديك صلاحية التعديل");
                    break;
                case OperationType.OperationIs.Delete:
                    GoHead = FrmMain.CurrentUser.CanDelete;
                    Pair.Add(GoHead, "ليس لديك صلاحية الحذف");
                    break;
                case OperationType.OperationIs.Print:
                    GoHead = FrmMain.CurrentUser.CanPrint;
                    Pair.Add(GoHead, "ليس لديك صلاحية الطباعة");
                    break;
                default:
                    break;
            }
            //if (IsAction)
            //{
            //    if (GoHead)
            //    {
            //        Pair.Clear();
            //        IsSoldorInOrder = !tbBillheader.IsExist("ownerguid", DataRow.Guid);
            //        Pair.Add(IsSoldorInOrder, "لا يمكن حذف الحساب لأنه مستخدم في العقود");
            //        if (IsSoldorInOrder)
            //        {
            //            Pair.Clear();
            //            IsSoldorInOrder = !tbBillheader.IsExist("buyerguid", DataRow.Guid);
            //            Pair.Add(IsSoldorInOrder, "لا يمكن حذف الحساب لأنه مستخدم في العقود");
            //        }
            //    }
            //}
            return Pair;

        }
        private void SetReadOnly(bool isReadOnly)
        {
            HashSet<RadControl> notUsedControls = new HashSet<RadControl> { TxtNumber, TxtLastAction };

            foreach (var control in MainContainer.Controls)
            {
                if (notUsedControls.Contains(control))
                {
                    continue;
                }

                switch (control)
                {
                    case RadTextBox radTextControl:
                        radTextControl.ReadOnly = isReadOnly;
                        radTextControl.AutoCompleteMode = isReadOnly ? AutoCompleteMode.None : AutoCompleteMode.SuggestAppend;
                        break;

                    case RadSpinEditor radSpinControl:
                        radSpinControl.ReadOnly = isReadOnly;
                        break;

                    case RadMultiColumnComboBox radCmbControl:
                        radCmbControl.ReadOnly = isReadOnly;
                        break;

                    case RadCheckBox radChkControl:
                        radChkControl.ReadOnly = isReadOnly;
                        break;
                }
            }
        }

        public void TackAction()
        {
            bool Confirm = MessagesHelper.MessageWarning("بطاقة المحامي", "هل تريد حفظ التغيرات ؟", "إذا ضغت علي زر نعم سوف يتم حفظ البيان المفتوح");
            if (Confirm)
            {
                ShowConfirmMSG = false;
                if (BtnNew.Text == "حفظ") { BtnNew.PerformClick(); }
                else if (BtnEdit.Text == "حفظ") { BtnEdit.PerformClick(); }
            }
            else if (!Confirm)
            {
                if (BtnNew.Text == "حفظ")
                {
                    IsProgrammatic = true;
                    Bs.MoveLast();
                    BtnNew.Text = "جديد";
                    BtnNew.ScreenTip.Text = "إضافة بطاقة محامي جديدة";
                    BtnNew.Image = Real_Estate_Management.Properties.Resources.BtnAddNew;
                    SetReadOnly(true);
                    IsProgrammatic = false;
                    IsDirty = false;
                    IsNew = false;
                    SetData();

                }
                else if (BtnEdit.Text == "حفظ")
                {
                    IsProgrammatic = true;
                    Bs.CancelEdit();
                    BtnEdit.Text = "تعديل";
                    BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة محامي";
                    BtnEdit.Image = Real_Estate_Management.Properties.Resources.BtnEdite;
                    BtnAttachment.Enabled = false;
                    BtnScanner.Enabled = false;
                    SetReadOnly(true);
                    IsProgrammatic = false;
                    IsDirty = false;
                    IsNew = false;
                    SetData();
                }
            }
        }

        private void CmbPlanGuid_DropDownOpening(object sender, System.ComponentModel.CancelEventArgs args)
        {
            if (!IsDirty)
            {
                RadCallout callout = new RadCallout
                {
                    ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle,
                    ArrowDirection = Telerik.WinControls.ArrowDirection.Right,
                    AutoClose = true,
                    CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle,
                    DropShadow = true
                };
                RadControl cn = sender as RadControl;
                RadCallout.Show(callout, cn, "لا يمكن تعديل بيانات البطاقة قبل الضغط علي زر تعديل أولاً", "تعديل البيانات", "ثم الضغط علي زر حفظ لحفظ التغييرات");
                args.Cancel = true;
            }

        }
        private void AlertTimer_Tick(object sender, EventArgs e)
        {
            if (RadOverlayManager.IsActive) { RadOverlayManager.Close(); }
            this.ParentForm.TopMost = false;
            AlertTimer.Stop();

        }

        private void CommandBarLabel1_TextChanged(object sender, EventArgs e)
        {
            string NewTxt = commandBarLabel1.Text.Replace("of", "من");
            commandBarLabel1.Text = NewTxt;

        }
        private void TxtViewOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            RadCallout callout = new RadCallout
            {
                ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle,
                ArrowDirection = Telerik.WinControls.ArrowDirection.Right,
                AutoClose = true,
                CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle,
                DropShadow = true
            };
            RadControl cn = sender as RadControl;
            RadCallout.Show(callout, cn, "لا يمك تعديل هذا البيان", "تعديل البيانات", "بيان للعرض فقط و لا يمكن تغييره");

        }
        private void TxtReadOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsDirty)
            {
                RadCallout callout = new RadCallout
                {
                    ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle,
                    ArrowDirection = Telerik.WinControls.ArrowDirection.Right,
                    AutoClose = true,
                    CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle,
                    DropShadow = true
                };
                RadControl cn = sender as RadControl;
                RadCallout.Show(callout, cn, "لا يمكن تعديل بيانات البطاقة قبل الضغط علي زر تعديل أولاً", "تعديل البيانات", "ثم الضغط علي زر حفظ لحفظ التغييرات");
            }
        }

        private void TxtClearButtonVisible_TextChanged(object sender, EventArgs e)
        {
            if (sender is RadTextBox control)
            {
                if (control.ReadOnly) { control.ShowClearButton = false; }
                else if (!control.ReadOnly) { control.ShowClearButton = true; }

            }
        }

        private void RadMenueTxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsDirty) { TackAction(); }

            }

        }
        private void Txtstatus_TextChanged(object sender, EventArgs e)
        {
            if (TxtStatues.Text == "نشط") { TxtStatues.BackColor = Color.FromArgb(0, 255, 1); }
            else if (TxtStatues.Text == "غير نشط") { TxtStatues.BackColor = Color.FromArgb(254, 0, 0); }
            else if (TxtStatues.Text == "محجوز") { TxtStatues.BackColor = Color.FromArgb(255, 255, 0); }
        }

        private void RadFlyoutManager_FlyoutClosed(FlyoutClosedEventArgs e)
        {
            Action action = new Action(() =>
            {
                if (e.Content is FlyoutReserveContent)
                {
                    RadCallout callout = new RadCallout();
                    callout.ArrowDirection = Telerik.WinControls.ArrowDirection.Up;

                    FlyoutReserveContent content = e.Content as FlyoutReserveContent;
                    if (content != null)
                    {
                        tbAgent Agent = (tbAgent)Bs.Current;
                        if (content.Result == DialogResult.OK)
                        {
                            DBConnect.StartTransAction();
                            TxtStatues.Text = Agent.note = "غير نشط";
                            BtnReservation.Text = "تنشيط";
                            Agent.Update();
                            if (DBConnect.CommitTransAction())
                            {
                                ShowDesktopAlert("تنشيط بطاقة محامي", "تنشيط بطاقة المحامي", "تمت عملية تنشيط البطاقة بنجاح", "تم تنشيط بطاقة المحامي يمكن القيام بالعمليات عليها الأن.");
                                FrmMain.DataHasChanged = true;
                            }

                            string ReserveReason = $"{content.ReserveReason}";
                            RadCallout.Show(callout, this.BtnReservation, $"عملية تنشيط بطاقة المحامي بسبب{ReserveReason} تمت!", "تمت العملية بنجاح");
                        }
                        else
                        {
                            RadCallout.Show(callout, this.BtnReservation, "فشلت عملية تنشيط بطاقة المحامي!", "فشلت العملية");
                        }
                    }

                }
                else if (e.Content is FlyoutEmailContent)
                {

                    FlyoutEmailContent content = e.Content as FlyoutEmailContent;
                    RadCallout callout = new RadCallout();

                    if (content != null)
                    {
                        tbAgent Agent = (tbAgent)Bs.Current;
                        if (content.Result == DialogResult.OK)
                        {
                            FastReport.Export.Email.EmailExport email = new FastReport.Export.Email.EmailExport();
                            FastReport.Report report = new FastReport.Report();
                            if (Readyreport(report))
                            {
                                report.Prepare();
                                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                                report.Export(export, Application.ExecutablePath + "Client.pdf");


                                MemoryStream ms = new MemoryStream();
                                using (FileStream file = new FileStream(Application.ExecutablePath + "Client.pdf", FileMode.Open, FileAccess.Read))
                                    file.CopyTo(ms);
                                DynamicAttachement dynamicAttachement = new DynamicAttachement
                                {
                                    attachData = ms,
                                    attachFileName = Application.ExecutablePath + "Client.pdf"
                                };

                                tbAttachment tbAttachment = new tbAttachment();

                                SendEmail.SendMail(content.ToMail, "subject", "messagebody", dynamicAttachement, new tbAttachment(), "ali", content.FromMail, content.PassWord
                                    , "smtp-mail.outlook.com", 587, true);

                            }

                            RadCallout.Show(callout, this.BtnEmailExport, $"لقد تم إرسال الملف Clients.pdf \n عبر الإيميل بنجاح", "تمت العملية");
                        }
                        else
                        {
                            RadCallout.Show(callout, this.BtnReservation, "فشلت عملية تنشيط بطاقة المحامي!", "فشلت العملية");
                        }
                    }
                }
            });

            this.Invoke(action);
        }

        #endregion

        #region Buttons

        private void BtnAddParenter_Click(object sender, EventArgs e)
        {
            if (!IsDirty)
            {
                RadCallout callout = new RadCallout
                {
                    ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle,
                    ArrowDirection = Telerik.WinControls.ArrowDirection.Right,
                    AutoClose = true,
                    CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle,
                    DropShadow = true
                };
                RadControl cn = sender as RadControl;
                RadCallout.Show(callout, cn, "لا يمكن تعديل بيانات البطاقة قبل الضغط علي زر تعديل أولاً", "تعديل البيانات", "ثم الضغط علي زر حفظ لحفظ التغييرات");
                return;
            }

        }

        private void MenuExit_Click(object sender, EventArgs e)
        {

            RadPageViewPage parent = this.Parent as RadPageViewPage;
            this.Dispose();
            parent.Dispose();
            GC.Collect();

        }



        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!Check("حذف بطاقة", "حذف بطاقة المحامي", OperationType.OperationIs.Delete, true)) { return; }

            if (!MessagesHelper.MessageWarning("حذف بطاقة المحامي", "هل أنت متأكد من حذف هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم حذف بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
            {
                return;
            }

            DBConnect.StartTransAction();
            tbAttachment attach = new tbAttachment();
            tbAgent agent = (tbAgent)Bs.Current;
            tbLog.AddLog("حذف", "بطاقة محامي", agent.name.ToString());
            attach.DeleteBy("ParentGuid", agent.guid);
            agent.Delete();
            if (DBConnect.CommitTransAction())
            {
                ShowNotification("حذف بطاقة محامي", "تمت عملية حذف بطاقة المحامي بنجاح", "عملية حذف بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("حذف بطاقة محامي", "تمت العملية", "تمت عملية حذف بطاقة المحامي بنجاح", "تم حذف بطاقة المحامي و لا يوجد لها بيانات في قاعدة البيانات الأن.");
                FrmMain.DataHasChanged = true;
                IsNew = false;
                CurrentGuid = Guid.Empty;
                SetData();
            }
        }


        private void MenuSaleOrder_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إنشاء أمر بيع", OperationType.OperationIs.Add, true)) { return; }
            tbAgent client = (tbAgent)Bs.Current;
            FrmSaleOrder frm = new FrmSaleOrder(Guid.Empty, true, client);
            frm.Show(this);
        }

        private void BtnReservation_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تنشيط بطاقة المحامي", OperationType.OperationIs.Edit, false)) { return; }
            tbAgent Agent = (tbAgent)Bs.Current;
            if (Agent.note.Equals("غير نشط"))
            {
                if (!MessagesHelper.MessageWarning("تنشيط بطاقة المحامي", "هل أنت متأكد من تنشيط هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم تنشيط بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
                {
                    return;
                }

                DBConnect.StartTransAction();
                TxtStatues.Text = Agent.note = "نشط";
                BtnReservation.Text = "إلغاء تنشيط";
                Agent.note = string.Empty;
                Agent.Update();
                if (DBConnect.CommitTransAction())
                {
                    ShowDesktopAlert("تنشيط بطاقة المحامي", "تمت العملية", "تمت عملية تنشيط بطاقة المحامي بنجاح", "تم تنشيط بطاقة المحامي لا يمكن القيام بالعمليات عليها الأن.");
                    FrmMain.DataHasChanged = true;
                }
            }
            else
            {
                if (!MessagesHelper.MessageWarning("تنشيط بطاقة المحامي", "هل أنت متأكد من إلغاء تنشيط هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم إلغاء تنشيط بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
                {
                    return;
                }
                RadFlyoutManager.Show(this, typeof(FlyoutReserveContent));
            }


        }

        private void BtnContract_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إنشاء عقد بيع", OperationType.OperationIs.Add, true)) { return; }
            tbAgent client = (tbAgent)Bs.Current;
            //FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 0, new List<tbLand>(), client);
            //frm.Show();

        }


        private void BtnImport_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إستيراد البيانات", OperationType.OperationIs.Add, false)) { return; }
            FrmBuyerImport frm = new FrmBuyerImport();
            frm.ShowDialog();

        }
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Title = "تصدير البيانات إلى ملف pdf",
                    Filter = "Spreadsheet(.xlsx,.xls) | *.xlsx; *.xls",
                    RestoreDirectory = true,
                    OverwritePrompt = true
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    report.Prepare();
                    FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                    if (export.ShowDialog())
                    {
                        report.Export(export, sfd.FileName);
                    }
                }
            }
        }

        private void BtnExportExcelData_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            tbAgent Agent = (tbAgent)Bs.Current;
            tbAgent.Fill("guid", Agent.guid);
            ExcelXLSX.ExportToExcelFromDataTable(tbAgent.dtData);

        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إرسال بريد إلكتروني", OperationType.OperationIs.Print, false)) { return; }
            RadFlyoutManager.Show(this, typeof(FlyoutEmailContent));

        }

        private void BtnExportWord_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Title = "تصدير البيانات إلى ملف pdf",
                    Filter = "Word File (.docx ,.doc)|*.docx;*.doc",
                    RestoreDirectory = true,
                    OverwritePrompt = true
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    report.Prepare();
                    FastReport.Export.OoXML.Word2007Export export = new FastReport.Export.OoXML.Word2007Export();
                    if (export.ShowDialog())
                        report.Export(export, sfd.FileName);
                }

            }

        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "تصدير البيانات إلى ملف pdf";
                sfd.Filter = "Pdf Files|*.pdf";
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    report.Prepare();
                    FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                    if (export.ShowDialog())
                        report.Export(export, sfd.FileName);
                }

            }

        }

        private void BtnResfresh_Click(object sender, EventArgs e)
        {
            this.ParentForm.TopMost = true;
            RadOverlayManager.Show(this);

            if (IsDirty) { TackAction(); }

            CurrentGuid = Guid.Empty;
            SetData();
            ShowDesktopAlert("تحديث البيانات", "تمت العملية", "تمت عملية تحديث البيانات بنجاح", "تم تحديث البيانات و يمكن القيام بالعمليات عليها الأن.");

            AlertTimer.Start();
            if (RadOverlayManager.IsActive) { RadOverlayManager.Close(); }
            this.ParentForm.TopMost = false;

        }

        private void NavigatorBtn_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }
        }
        private void MenuDesign_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }
            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                Reports.DesignReport(report);
        }

        private void MenuPreview_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                report.Show();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                report.Print();
        }
        #endregion

        #region report and print

        private bool Readyreport(FastReport.Report rpt)
        {
            tbAgent Agent = (tbAgent)Bs.Current;

            if (Agent is null || Agent.guid.Equals(Guid.Empty))
            {
                ShowDesktopAlert("الطباعة", "فشلت العملية", "يجب حفظ البطاقة أولاً", "يجب حفظ البطاقة أولاً ثم يمكنك طباعتها.");
                return false;
            }

            //if (AgentType == 0)
            //    Reports.InitReport(rpt, "ownercard.frx", false);
            //else
            //    Reports.InitReport(rpt, "agentcard.frx", false);

            Reports.InitReport(rpt, "agentcard.frx", false);

            tbAgent.Fill(Agent.guid);

            tbPlanInfo.Fill();
            rpt.RegisterData(tbPlanInfo.dtData, "planinfodata");
            rpt.RegisterData(tbAgent.dtData, "ownerdata");
            rpt.RegisterData(tbAgent.dtData, "agentdata");



            rpt.SetParameterValue("UserName", FrmMain.CurrentUser.name);

            rpt.RegisterData(tbAgent.dtData, "data");

            return true;
        }


        #endregion

        #region Attach

        private void FillDataGridAttachments(Guid parentguid)
        {
            tbAttachment.Fill("ParentGuid", parentguid);
            DataGridAttachments.DataSource = tbAttachment.dtData;

            DataGridAttachments.Columns[0].IsVisible = false;
            DataGridAttachments.Columns[1].IsVisible = false;

            DataGridAttachments.Columns[2].HeaderText = "الاسم";
            DataGridAttachments.Columns[3].HeaderText = "اسم الملف";
            DataGridAttachments.Columns[4].HeaderText = "الحجم";
            DataGridAttachments.Columns[5].HeaderText = "الملف";

            foreach (GridViewDataColumn col in this.DataGridAttachments.Columns)
            {
                col.TextAlignment = ContentAlignment.MiddleCenter;
                col.HeaderTextAlignment = ContentAlignment.MiddleCenter;
            }

        }
        private void AddAttachments(Guid parentguid)
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

        private void BtnAddAttachment_Click(object sender, EventArgs e)
        {

            OpenFileDialog opf = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = "All Files (*.*)|*.*"
            };

            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                tbAttachment tbattach = new tbAttachment();
                tbattach.Guid = Guid.NewGuid();
                tbattach.ParentGuid = Guid.Empty;
                tbattach.Name = System.IO.Path.GetFileName(opf.FileName);
                tbattach.FileName = tbattach.Name;
                tbattach.FileData = FileHelper.FileToByteArray(opf.FileName);
                if (tbattach.FileData.Length > 10000000)
                {
                    ShowDesktopAlert("إضافة المرفقات", "فشلت العملية", "تمت إضافة المرفقات", "لا يمكن إضافة مرفق بحجم أكبر من 10 ميغا بايت.");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;


                tbattach.FileSize = string.Format(new FileSizeFormatProvider(), "{0:fs}", tbattach.FileData.Length);
                tbAttachment.dtData.Rows.Add(tbattach.Guid, tbattach.ParentGuid, tbattach.Name, tbattach.FileName, tbattach.FileSize, tbattach.FileData);
                DataGridAttachments.DataSource = tbAttachment.dtData;
                this.Cursor = Cursors.Arrow;


            }
        }

        private void DataGridAttachment_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (BtnEdit.Text == "حفظ") { MenuDeleteAttachment.Enabled = true; }
                else if (BtnEdit.Text != "حفظ") { MenuDeleteAttachment.Enabled = false; }

                if (e.Button == MouseButtons.Right)
                {
                    if (DataGridAttachments.CurrentCell != null)
                    {
                        this.radContextMenuManager1.SetRadContextMenu(this.DataGridAttachments, this.AttachmentsContextMenu);
                    }
                    else if (DataGridAttachments.CurrentCell == null)
                    {
                        this.radContextMenuManager1.SetRadContextMenu(this.DataGridAttachments, null);
                    }
                }
            }
            catch
            {
                this.radContextMenuManager1.SetRadContextMenu(this.DataGridAttachments, null);
            }
        }


        private void MenuPreviewAttach_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            byte[] bfiles = (byte[])DataGridAttachments.CurrentRow.Cells[5].Value;
            Guid guid = new Guid(DataGridAttachments.CurrentRow.Cells[0].Value.ToString());
            string filename = DataGridAttachments.CurrentRow.Cells[3].Value.ToString();

            tbAttachment attach = tbAttachment.FindByFull("guid", guid);

            if (bfiles.Length <= 1)
                FileHelper.RunFile(attach.FileName, attach.FileData);
            else
                FileHelper.RunFile(filename, bfiles);

        }

        private void MenuExtractAttachment_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }
            SaveFileDialog sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Filter = "All Files (*.*)|*.*"
            };

            byte[] bfiles = (byte[])DataGridAttachments.CurrentRow.Cells[5].Value;
            Guid guid = new Guid(DataGridAttachments.CurrentRow.Cells[0].Value.ToString());
            tbAttachment attach = tbAttachment.FindByFull("guid", guid);
            sfd.FileName = attach.Name;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (bfiles.Length <= 1)
                        FileHelper.ByteArraytoFile(attach.FileData, sfd.FileName);
                    else
                        FileHelper.ByteArraytoFile(bfiles, sfd.FileName);

                    ShowDesktopAlert("إستخراج المرفقات", "تمت العملية", "تمت إستخراج المرفقات بنجاح", "تم عملية إستخراج المرفقات بنجاح و يمكن القيام بالعمليات عليها الأن.");

                }
                catch (Exception)
                {
                    ShowDesktopAlert("إستخراج المرفقات", "حدث خطأ", "لم يتم إستخراج المرفقات بنجاح", "حدث خطأ عند إستخراج المرفقات.");
                }
            }
        }

        private void MenuDeleteAttachment_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Delete, true)) { return; }

            DataGridAttachments.Rows.RemoveAt(DataGridAttachments.CurrentRow.Index);
        }

        private void BtnScanner_Click(object sender, EventArgs e)
        {
            RadButton toolmenu = (RadButton)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Add, false)) { return; }

            FrmScanImage2 frmscan = new FrmScanImage2();
            if (frmscan.ShowDialog() == DialogResult.OK)
            {
                if (frmscan.imgSc.fbytes.Length <= 0)
                {
                    ShowDesktopAlert("إضافة مرفقات", "لم يتم إضافة المرفق", "الملف غير صالح", "حدد ملف صالح ثم اعد المحاولة مرة آخرى.");
                    return;
                }
                tbAttachment tbattach = new tbAttachment();
                tbattach.Guid = Guid.NewGuid();
                tbattach.ParentGuid = Guid.Empty;
                tbattach.Name = System.IO.Path.GetFileName(frmscan.imgSc.filename);
                tbattach.FileName = tbattach.Name;

                tbattach.FileData = frmscan.imgSc.fbytes;
                tbattach.FileSize = string.Format(new FileSizeFormatProvider(), "{0:fs}", tbattach.FileData.Length);
                tbAttachment.dtData.Rows.Add(tbattach.Guid, tbattach.ParentGuid, tbattach.Name, tbattach.FileName, tbattach.FileSize, tbattach.FileData);
                DataGridAttachments.DataSource = tbAttachment.dtData;
            }
            else
            {
                ShowDesktopAlert("إضافة مرفقات", "لم يتم إضافة المرفق", "تم إلغاء العملية", "قام المستخدم بإلغاء العملية.");
            }
        }


        #endregion


    }
}
