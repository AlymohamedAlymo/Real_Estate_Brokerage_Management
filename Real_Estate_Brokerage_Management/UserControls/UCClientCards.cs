using DoctorERP.CustomElements;
using DoctorERP.Helpers;
using DoctorHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.SplashScreen;

namespace DoctorERP.User_Controls
{
    public partial class UCClientCards : UserControl
    {

        private Guid guid;
        public BindingSource Bs;
        public bool IsDirty = false, IsLoad = true;
        private bool IsNew = false, ShowConfirmMSG = true, IsProgrammatic = false, AddFromSelect;
        private readonly int CurrentPosition = 0;
        private int AgentType;
        public tbAgent AddedAgents = new tbAgent();

        public UCClientCards(Guid _guid, bool _IsNew, int _AgentType, bool _AddFromSelect)
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
            Txtnumber.TextAlignment = ContentAlignment.MiddleCenter;
            radstatus.TextAlignment = ContentAlignment.MiddleCenter;
            radDesktopAlert1.Popup.RootElement.RightToLeft = true;
            radPageView1.RootElement.Children[0].Children[0].Children[0].Children[3].Visibility = ElementVisibility.Hidden;

            List<CommandBarButton> radBarButton = new List<CommandBarButton> { BtnResfresh, BtnDelete, BtnNew, BtnEdit, BtnExit };
            foreach (CommandBarButton control in radBarButton)
            {
                RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();
                screenTip.CaptionLabel.Text = control.Text;
                screenTip.MainTextLabel.Text = control.Tag.ToString();
                control.ScreenTip = screenTip;
            }
            List<CommandBarDropDownButton> radDropDownButton = new List<CommandBarDropDownButton> { BtnImport, BtnExport, BtnPrint, BtnOperation };
            foreach (CommandBarDropDownButton control in radDropDownButton)
            {
                RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();
                screenTip.CaptionLabel.Text = control.Text;
                screenTip.MainTextLabel.Text = control.Tag.ToString();
                control.ScreenTip = screenTip;
            }
            List<RadMenuItem> radMenuItem = new List<RadMenuItem> { BtnImportExcel, BtnExportExcelData, BtnEcelExport, BtnEmailExport, BtnPdfExport, BtnWordExport, BtnSentToPrinter, BtnDesign, BtnPreview, BtnContract, BtnSaleOrder, BtnReservation, BtnPriceOffer };
            foreach (RadMenuItem control in radMenuItem)
            {
                RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();
                screenTip.CaptionLabel.Text = control.Text;
                screenTip.MainTextLabel.Text = control.Tag.ToString();
                control.ScreenTip = screenTip;
            }
            radPageView1.SelectedPage = PageHome;
            #endregion

            guid = _guid;
            IsNew = _IsNew;
            AddFromSelect = _AddFromSelect;
            AgentType = _AgentType;

            if (AgentType == 0)
            {
                Text = "تعريف المالك";
                BindingNavigatorClient.Enabled = false;
            }
            else
            {
                Text = "بطاقة عميل";
                ////grpOffice.Visible = false;
            }

            if (AddFromSelect)
            {
                BindingNavigatorClient.Enabled = false;

            }



            SetData();
        }

        #region Main Events
        #region Default
        private bool MessageWarning(string Heading, string Body, string FootNote)
        {
            RadTaskDialogPage page = new RadTaskDialogPage()
            {

                Caption = " ",
                Heading = Heading,
                Text = Body,
                RightToLeft = true,
                CustomFont = "Robot",
                Icon = RadTaskDialogIcon.ShieldWarningYellowBar,
                AllowCancel = true,
                Footnote = new RadTaskDialogFootnote("ملحوظة: " + FootNote),
                CommandAreaButtons = {
                    RadTaskDialogButton.Yes,
                    RadTaskDialogButton.No
                }

            };
            page.CommandAreaButtons[0].Text = "نعم";
            page.CommandAreaButtons[1].Text = "لا";
            RadTaskDialogButton result = RadTaskDialog.ShowDialog(page, RadTaskDialogStartupLocation.CenterScreen);
            if (result == null || result == RadTaskDialogButton.No)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool MessageException(string Heading, string Body, string FootNote)
        {
            RadTaskDialogPage page = new RadTaskDialogPage()
            {

                Caption = " ",
                Heading = Heading,
                Text = Body,
                RightToLeft = true,
                CustomFont = "Robot",
                Icon = RadTaskDialogIcon.ShieldErrorRedBar,
                Footnote = new RadTaskDialogFootnote("ملحوظة: " + FootNote),
                CommandAreaButtons = {
                    RadTaskDialogButton.OK,
                }

            };
            page.CommandAreaButtons[0].Text = "موافق";
            RadTaskDialogButton result = RadTaskDialog.ShowDialog(page, RadTaskDialogStartupLocation.CenterScreen);
            return true;
        }
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
            radDesktopAlert1.ContentImage = Properties.Resources.information50;
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
        private bool Check(string Operation, string Command, OperationType.OperationIs OperType)
        {
            if (IsDirty) { TackAction(); }
            if (!FrmMain.IsPermissionGranted(Operation))
            {
                MessageException(Operation, "لا تملك صلاحية", "لا تملك صلاحية للقيام ب " + Command + " \n تواصل مع المدير.");
                return false;
            }
            if (Bs.Current == null)
            {
                MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة العميل أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            tbAgent Agent = (tbAgent)Bs.Current;
            var Check = CheckifOprAllow(Agent, OperType);
            if (!Check.Keys.First() && !Operation.Contains("جديد"))
            {
                MessageException(Operation, "لا يمكن " + Command, Check.Values.First());
                return false;
            }
            if (Agent is null || Agent.guid.Equals(Guid.Empty))
            {
                MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة العميل أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            return true;

        }

        private Dictionary<bool, string> CheckifOprAllow(tbAgent Agent, OperationType.OperationIs operation)
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
            if (GoHead)
            {
                Pair.Clear();
                IsSoldorInOrder = !tbBillheader.IsExist("ownerguid", Agent.guid);
                Pair.Add(IsSoldorInOrder, "لا يمكن حذف الحساب لأنه مستخدم في العقود");
                if (IsSoldorInOrder)
                {
                    Pair.Clear();
                    IsSoldorInOrder = !tbBillheader.IsExist("buyerguid", Agent.guid);
                    Pair.Add(IsSoldorInOrder, "لا يمكن حذف الحساب لأنه مستخدم في العقود");
                }
            }
            return Pair;

        }

        private void SetReadOnly(bool IsReadOnly)
        {
            List<RadControl> NotUsedControls = new List<RadControl>()
            { Txtnumber, Txtlastaction };
            List<RadControl> ContainerControls = new List<RadControl>()
            { radCollapsiblePanelParenter1, radCollapsiblePanelParenter2, radCollapsiblePanelParenter3, radCollapsiblePanelParenter4, radCollapsiblePanel2 };
            List<RadPageViewPage> ContainerPage = new List<RadPageViewPage>()
            { PageHome, PageAgent };

            foreach (RadControl collection in ContainerControls)
            {
                foreach (RadControl control in collection.Controls[0].Controls[0].Controls)
                {
                    if (NotUsedControls.Contains(control)) { continue; }
                    if (control is RadTextBox radTextControl)
                    {
                        radTextControl.ReadOnly = IsReadOnly;
                        if (IsReadOnly) { radTextControl.AutoCompleteMode = AutoCompleteMode.None; }
                        else if (!IsReadOnly) { radTextControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend; }
                    }
                    else if (control is RadSpinEditor radSpinControl)
                    {
                        radSpinControl.ReadOnly = IsReadOnly;
                    }
                    else if (control is RadMultiColumnComboBox radCmbControl)
                    {
                        radCmbControl.ReadOnly = IsReadOnly;
                    }
                    else if (control is RadCheckBox radChkControl)
                    {
                        radChkControl.ReadOnly = IsReadOnly;
                    }
                }

            }
            foreach (RadPageViewPage collection in ContainerPage)
            {
                foreach (RadControl control in collection.Controls)
                {
                    if (NotUsedControls.Contains(control)) { continue; }
                    if (control is RadTextBox radTextControl)
                    {
                        radTextControl.ReadOnly = IsReadOnly;
                        if (IsReadOnly) { radTextControl.AutoCompleteMode = AutoCompleteMode.None; }
                        else if (!IsReadOnly) { radTextControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend; }
                    }
                    else if (control is RadSpinEditor radSpinControl)
                    {
                        radSpinControl.ReadOnly = IsReadOnly;
                    }
                    else if (control is RadMultiColumnComboBox radCmbControl)
                    {
                        radCmbControl.ReadOnly = IsReadOnly;
                    }
                    else if (control is RadCheckBox radChkControl)
                    {
                        radChkControl.ReadOnly = IsReadOnly;
                    }
                }

            }


        }

        public void TackAction()
        {
            bool Confirm = MessageWarning("بطاقة العميل", "هل تريد حفظ التغيرات ؟", "إذا ضغت علي زر نعم سوف يتم حفظ البيان المفتوح");
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
                    BtnNew.ScreenTip.Text = "إضافة بطاقة عميل جديدة";
                    BtnNew.Image = Properties.Resources.BtnAddNew;
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
                    BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة عميل";
                    BtnEdit.Image = Properties.Resources.BtnEdite;
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




        #region Binding
        private void SetData()
        {
            IsLoad = true;

            if (FrmMain.PlanGuid != Guid.Empty)
            {
                //tbAgent.Fill("PlanGuid", FrmMain.PlanGuid, "agenttype", AgentType);

            }
            else if (FrmMain.PlanGuid == Guid.Empty)
            {
                tbAgent.Fill("agenttype", AgentType);
            }


            Bs = new BindingSource(tbAgent.lstData, string.Empty);
            Bs.PositionChanged += new EventHandler(Bs_PositionChanged);
            BindingNavigatorClient.BindingSource = Bs;

            tbPlanInfo.Fill();
            radCmbPlanGuid.DataSource = tbPlanInfo.lstData;
            radCmbPlanGuid.ValueMember = "guid";
            radCmbPlanGuid.DisplayMember = "name";
            radCmbPlanGuid.EditorControl.Columns[0].IsVisible = false;
            radCmbPlanGuid.EditorControl.Columns[2].IsVisible = false;
            radCmbPlanGuid.EditorControl.Columns[1].HeaderText = "اسم المخطط";
            radCmbPlanGuid.EditorControl.Columns[3].HeaderText = "المدينة";
            radCmbPlanGuid.EditorControl.Columns[3].HeaderText = "الموقع";
            radCmbPlanGuid.EditorControl.Columns[4].HeaderText = "رقم المخطط";
            radCmbPlanGuid.AutoSizeDropDownColumnMode = BestFitColumnMode.AllCells;

            SetControlsDataBindings();

            if (IsNew) { BtnNew.PerformClick(); return; }

            if (!guid.Equals(Guid.Empty))
            {
                Bs.Position = tbAgent.lstData.FindIndex(delegate (tbAgent Agent) { return Agent.guid == guid; });
                return;
            }
            Bs.MoveLast();

            IsLoad = false;

        }
        private void SetControlsDataBindings()
        {
            List<RadControl> ContainerControls = new List<RadControl>()
            { radCollapsiblePanel2 };
            List<RadPageViewPage> ContainerPage = new List<RadPageViewPage>()
            { PageHome, PageAgent };

            foreach (RadControl collection in ContainerControls)
            {
                foreach (RadControl control in collection.Controls[0].Controls[0].Controls)
                {
                    if (control.Name.StartsWith("rad")) { continue; }
                    control.DataBindings.Clear();
                    string dataMember = control.Name.Remove(0, 3);
                    string propertyName = control is RadSpinEditor ? "Value" : control is RadCheckBox ? "Checked"
                        : control is RadMultiColumnComboBox ? "SelectedValue" : "Text";
                    Binding ControlBinding = new System.Windows.Forms.Binding(propertyName, Bs, dataMember, false);
                    control.DataBindings.Add(ControlBinding);
                }

            }
            foreach (RadPageViewPage collection in ContainerPage)
            {
                foreach (RadControl control in collection.Controls)
                {
                    if (control.Name.StartsWith("rad")) { continue; }
                    control.DataBindings.Clear();
                    string dataMember = control.Name.Remove(0, 3);
                    string propertyName = control is RadSpinEditor ? "Value" : control is RadCheckBox ? "Checked"
                        : control is RadMultiColumnComboBox ? "SelectedValue" : "Text";
                    Binding ControlBinding = new System.Windows.Forms.Binding(propertyName, Bs, dataMember, false);
                    control.DataBindings.Add(ControlBinding);
                }

            }


        }

        private void Bs_PositionChanged(object sender, EventArgs e)
        {
            tbAgent obj = (tbAgent)Bs.Current;

            if (Bs.Current == null || obj.guid.Equals(Guid.Empty)) { return; }
            if (IsDirty && !IsProgrammatic)
            {
                if (IsNew)
                {
                    Bs.MoveLast();
                    if (IsDirty) { TackAction(); }
                }

                IsProgrammatic = true;
                Bs.Position = CurrentPosition - 1;
                TackAction();
                IsProgrammatic = false;
            }
            if (!obj.guid.Equals(Guid.Empty)) { FillBindingCurrentData(obj); }

        }
        private void FillBindingCurrentData(tbAgent Agent)
        {
            if (Agent.note == "غير نشط")
            {
                BtnReservation.Text = "تنشيط";
                radstatus.Text = "غير نشط";

            }
            else
            {
                BtnReservation.Text = "إلغاء التنشيط";
                radstatus.Text = "نشط";

            }

            FillDataGridAttachments(Agent.guid);
            BtnDelete.Enabled = true;
            BtnEdit.Enabled = true;
            SetReadOnly(true);
        }

        private void NewFill()
        {
            IsNew = true;
            BtnDelete.Enabled = false;
            BtnEdit.Enabled = false;
            BtnNew.Text = "حفظ";
            BtnNew.ScreenTip.Text = "حفظ بطاقة عميل جديدة";
            BtnNew.Image = Properties.Resources.BtnConform;

            BtnReservation.Text = "تنشيط";

            FillDataGridAttachments(Guid.Empty);
            SetReadOnly(false);

            foreach (RadControl control in PageHome.Controls)
            {
                if (control is RadSpinEditor spinEditor)
                {
                    spinEditor.Value = 0;
                }
                else if (control is RadTextBox textControl)
                {
                    textControl.Text = string.Empty;
                }

                if (control.Name.StartsWith("rad")) { continue; }
                control.DataBindings.Clear();
            }
            Txtnumber.Text = (tbAgent.GetMaxNumber("Number") + 1).ToString();
            radstatus.Text = "نشط";
            radCmbPlanGuid.SelectedValue = FrmMain.PlanGuid;
            Txtname.Focus();
        }

        private bool Add()
        {

            if (ShowConfirmMSG)
            {
                if (!MessageWarning("هل أنت متاكد من الإضافة ؟", "إضافة بطاقة عميل جديدة", "إذا ضغت علي زر نعم سوف يتم إضافة بطاقة العميل الجديدة"))
                    return false;
            }
            if (Txtname.Text.Trim().Equals(string.Empty))
            {
                RadCallout callout = new RadCallout
                {
                    ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle,
                    ArrowDirection = Telerik.WinControls.ArrowDirection.Right,
                    AutoClose = true,
                    CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle,
                    DropShadow = true
                };
                RadCallout.Show(callout, Txtname, "اسم العميل غير مدخل", "حفظ البيانات", "ثم الضغط علي زر حفظ لحفظ التغييرات");
                return false;
            }

           
            tbAgent agent = new tbAgent 
            { 
                agencynumber = Txtagencynumber.Text,
                agentcivilid = Txtagentcivilid.Text,
                agentemail = Txtagentemail.Text, 
                agentmobile = Txtagentmobile.Text,
                agentname = Txtagentname.Text,
                agentpublicnumber = Txtagentpublicnumber.Text,
                agenttype = AgentType,
                agentvatid = Txtagentvatid.Text,
                civilid = Txtcivilid.Text,
                email = Txtemail.Text,
                guid = Guid.NewGuid(),
                lastaction = "عملية إضافة" + " - بتاريخ  " + DateTime.Now.ToString("dd/MM/yyyy")
                + " - الساعة  " + DateTime.Now.ToString("hh:mm tt") + " - عن طريق المستخدم  " + FrmMain.CurrentUser.name,
                mobile = Txtmobile.Text,
                name = Txtname.Text,
                note = Txtnote.Text,
                number = tbAgent.GetMaxNumber("Number") + 1,
                officecr = Txtofficecr.Text,
                officeemail = Txtofficeemail.Text,
                officename = Txtofficename.Text,
                officephone = Txtofficephone.Text,
                officepublicnumber = Txtofficepublicnumber.Text,
                officevatid = Txtofficevatid.Text,
                publicnumber = Txtpublicnumber.Text,
                vatid = Txtvatid.Text,
                
            };
            //agent.planguid = tbPlanInfo.lstData.Where(u => u.name == CmbPlanGuid.Text).FirstOrDefault().guid;

            DBConnect.StartTransAction();
            tbLog.AddLog("إضافة", "بطاقة عميل", agent.name.ToString());
           AddAttachments(agent.guid);
            agent.Insert();

            if (DBConnect.CommitTransAction())
            {
                ShowNotification("إضافة بطاقة عميل جديدة", "تمت عملية إضافة بطاقة العميل بنجاح", "عملية إضافة بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("إضافة بطاقة عميل جديدة", "تمت العملية", "تمت عملية إضافة بطاقة العميل بنجاح", "بطاقة العميل الجديدة تمت إضافتها يمكن القيام بالعمليات عليها الأن.");
                FrmMain.DataHasChanged = true;
            }

            return true;
        }

        private bool Edit()
        {


            if (ShowConfirmMSG)
            {
                if (!MessageWarning("هل أنت متاكد من التعديل ؟", "تعديل بطاقة عميل", "إذا ضغت علي زر نعم سوف يتم تعديل بيانات بطاقة العميل"))
                    return false;
            }
            if (Txtname.Text.Trim().Equals(string.Empty))
            {
                RadCallout callout = new RadCallout
                {
                    ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle,
                    ArrowDirection = Telerik.WinControls.ArrowDirection.Right,
                    AutoClose = true,
                    CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle,
                    DropShadow = true
                };
                RadCallout.Show(callout, Txtname, "اسم العميل غير مدخل", "حفظ البيانات", "ثم الضغط علي زر حفظ لحفظ التغييرات");
                return false;
            }

            Guid CurrentGuid = ((tbAgent)Bs.Current).guid;
            tbAgent agent = tbAgent.lstData.Where(u => u.guid == CurrentGuid).FirstOrDefault();

            agent.agencynumber = Txtagencynumber.Text;
            agent.agentcivilid = Txtagentcivilid.Text;
            agent.agentemail = Txtagentemail.Text;
            agent.agentmobile = Txtagentmobile.Text;
            agent.agentname = Txtagentname.Text;
            agent.agentpublicnumber = Txtagentpublicnumber.Text;
            agent.agenttype = AgentType;
            agent.agentvatid = Txtagentvatid.Text;
            agent.civilid = Txtcivilid.Text;
            agent.email = Txtemail.Text;
            agent.lastaction = "عملية إضافة" + " - بتاريخ  " + DateTime.Now.ToString("dd/MM/yyyy")
            + " - الساعة  " + DateTime.Now.ToString("hh:mm tt") + " - عن طريق المستخدم  " + FrmMain.CurrentUser.name;
            agent.mobile = Txtmobile.Text;
            agent.name = Txtname.Text;
            agent.note = Txtnote.Text;
            agent.officecr = Txtofficecr.Text;
            agent.officeemail = Txtofficeemail.Text;
                agent.officename = Txtofficename.Text;
            agent.officephone = Txtofficephone.Text;
            agent.officepublicnumber = Txtofficepublicnumber.Text;
            agent.officevatid = Txtofficevatid.Text;
            agent.publicnumber = Txtpublicnumber.Text;
            agent.vatid = Txtvatid.Text;


            //Agent.planguid = tbPlanInfo.lstData.Where(u => u.name == CmbPlanGuid.Text).FirstOrDefault().guid;

            DBConnect.StartTransAction();
            AddAttachments(agent.guid);
            agent.Update();
            tbLog.AddLog("تعديل", "بطاقة عميل", agent.name.ToString());
            if (DBConnect.CommitTransAction())
            {
                ShowNotification("تعديل بطاقة عميل جديدة", "تمت عملية تعديل بطاقة العميل بنجاح", "عملية إضافة بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("تعديل بطاقة عميل جديدة", "تمت العملية", "تمت عملية تعديل بطاقة العميل بنجاح", "تم تعديل بيانات بطاقة العميل يمكن القيام بالعمليات عليها الأن.");
                FrmMain.DataHasChanged = true;

            }

            return true;
        }

        #endregion


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
            if (radstatus.Text == "نشط") { radstatus.BackColor = Color.FromArgb(0, 255, 1); }
            else if (radstatus.Text == "غير نشط") { radstatus.BackColor = Color.FromArgb(254, 0, 0); }
            else if (radstatus.Text == "محجوز") { radstatus.BackColor = Color.FromArgb(255, 255, 0); }
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
                            radstatus.Text = Agent.note = "غير نشط";
                            BtnReservation.Text = "تنشيط";
                            Agent.Update();
                            if (DBConnect.CommitTransAction())
                            {
                                ShowDesktopAlert("تنشيط بطاقة عميل", "تنشيط بطاقة العميل", "تمت عملية تنشيط البطاقة بنجاح", "تم تنشيط بطاقة العميل يمكن القيام بالعمليات عليها الأن.");
                                FrmMain.DataHasChanged = true;
                            }

                            string ReserveReason = $"{content.ReserveReason}";
                            RadCallout.Show(callout, this.BtnReservation, $"عملية تنشيط بطاقة العميل بسبب{ReserveReason} تمت!", "تمت العملية بنجاح");
                        }
                        else
                        {
                            RadCallout.Show(callout, this.BtnReservation, "فشلت عملية تنشيط بطاقة العميل!", "فشلت العملية");
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
                            RadCallout.Show(callout, this.BtnReservation, "فشلت عملية تنشيط بطاقة العميل!", "فشلت العملية");
                        }
                    }
                }
            });

            this.Invoke(action);
        }


        #endregion



        #region Buttons

        private void MenuExit_Click(object sender, EventArgs e)
        {

            RadPageViewPage parent = this.Parent as RadPageViewPage;
            this.Dispose();
            parent.Dispose();
            GC.Collect();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

            if (BtnNew.Text == "جديد")
            {
                if (!Check("إضافة جديد", "إضافة بطاقة عميل جديدة", OperationType.OperationIs.Add)) { return; }

                BtnNew.Text = "حفظ";
                BtnNew.ScreenTip.Text = "حفظ بطاقة العميل الجديدة";
                BtnNew.Image = Properties.Resources.BtnConform;
                BtnAttachment.Enabled = true;
                BtnScanner.Enabled = true;
                radPageView1.SelectedPage = PageHome;
                IsDirty = true;
                guid = Guid.Empty;
                Bs.AddNew();
                Bs.MoveLast();
                NewFill();
            }
            else if (BtnNew.Text == "حفظ")
            {
                Add();
                BtnNew.Text = "جديد";
                BtnNew.ScreenTip.Text = "إضافة بطاقة عميل جديدة";
                BtnNew.Image = Properties.Resources.BtnAddNew;
                BtnAttachment.Enabled = false;
                BtnScanner.Enabled = false;
                SetReadOnly(true);
                IsDirty = false;
                IsNew = false;
                guid = Guid.Empty;
                SetData();

            }

        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {

            tbAgent Agent = (tbAgent)Bs.Current;

            if (BtnEdit.Text == "تعديل")
            {
                if (!Check("تعديل بطاقة", "تعديل بطاقة العميل", OperationType.OperationIs.Edit)) { return; }

                BtnEdit.Text = "حفظ";
                BtnEdit.ScreenTip.Text = "حفظ التعديلات";
                BtnEdit.Image = Properties.Resources.BtnConform;
                BtnAttachment.Enabled = true;
                BtnScanner.Enabled = true;
                IsDirty = true;
                guid = Agent.guid;
                SetReadOnly(false);
            }
            else if (BtnEdit.Text == "حفظ")
            {
                Edit();
                BtnEdit.Text = "تعديل";
                BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة العميل";
                BtnEdit.Image = Properties.Resources.BtnEdite;
                BtnAttachment.Enabled = false;
                BtnScanner.Enabled = false;
                SetReadOnly(true);
                IsDirty = false;
                IsNew = false;
                guid = Agent.guid;
                SetData();

            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!Check("حذف بطاقة", "حذف بطاقة العميل", OperationType.OperationIs.Delete)) { return; }

            if (!MessageWarning("حذف بطاقة العميل", "هل أنت متأكد من حذف هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم حذف بطاقة العميل \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
            {
                return;
            }

            DBConnect.StartTransAction();
            tbAttachment attach = new tbAttachment();
            tbAgent agent = (tbAgent)Bs.Current;
            tbLog.AddLog("حذف", "بطاقة عميل", agent.name.ToString());
            attach.DeleteBy("ParentGuid", agent.guid);
            agent.Delete();
            if (DBConnect.CommitTransAction())
            {
                ShowNotification("حذف بطاقة عميل", "تمت عملية حذف بطاقة العميل بنجاح", "عملية حذف بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("حذف بطاقة عميل", "تمت العملية", "تمت عملية حذف بطاقة العميل بنجاح", "تم حذف بطاقة العميل و لا يوجد لها بيانات في قاعدة البيانات الأن.");
                FrmMain.DataHasChanged = true;
                IsNew = false;
                guid = Guid.Empty;
                SetData();
            }
        }


        private void MenuSaleOrder_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إنشاء أمر بيع", OperationType.OperationIs.Add)) { return; }
            tbAgent client = (tbAgent)Bs.Current;
            FrmSaleOrder frm = new FrmSaleOrder(Guid.Empty, true, client);
            frm.Show(this);
        }

        private void BtnReservation_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تنشيط بطاقة العميل", OperationType.OperationIs.Edit)) { return; }
            tbAgent Agent = (tbAgent)Bs.Current;
            if (Agent.note.Equals("غير نشط"))
            {
                if (!MessageWarning("تنشيط بطاقة العميل", "هل أنت متأكد من تنشيط هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم تنشيط بطاقة العميل \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
                {
                    return;
                }

                DBConnect.StartTransAction();
                radstatus.Text = Agent.note = "نشط";
                BtnReservation.Text = "إلغاء تنشيط";
                Agent.note = string.Empty;
                Agent.Update();
                if (DBConnect.CommitTransAction())
                {
                    ShowDesktopAlert("تنشيط بطاقة العميل", "تمت العملية", "تمت عملية تنشيط بطاقة العميل بنجاح", "تم تنشيط بطاقة العميل لا يمكن القيام بالعمليات عليها الأن.");
                    FrmMain.DataHasChanged = true;
                }
            }
            else
            {
                if (!MessageWarning("تنشيط بطاقة العميل", "هل أنت متأكد من إلغاء تنشيط هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم إلغاء تنشيط بطاقة العميل \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
                {
                    return;
                }
                RadFlyoutManager.Show(this, typeof(FlyoutReserveContent));
            }


        }

        private void BtnContract_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إنشاء عقد بيع", OperationType.OperationIs.Add)) { return; }
            tbAgent client = (tbAgent)Bs.Current;
            FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 0, new List<tbLand>(), client);
            frm.Show();

        }


        private void BtnImport_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إستيراد البيانات", OperationType.OperationIs.Add)) { return; }
            //FrmAgentImport frm = new FrmAgentImport();
            //frm.ShowDialog();

        }
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Edit)) { return; }

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
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Edit)) { return; }

            tbAgent Agent = (tbAgent)Bs.Current;
            tbAgent.Fill("guid", Agent.guid);
            ExcelXLSX.ExportToExcelFromDataTable(tbAgent.dtData);

        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إرسال بريد إلكتروني", OperationType.OperationIs.Edit)) { return; }
            RadFlyoutManager.Show(this, typeof(FlyoutEmailContent));

        }

        private void BtnExportWord_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Edit)) { return; }

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
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Edit)) { return; }

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

            guid = Guid.Empty;
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
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print)) { return; }
            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                Reports.DesignReport(report);
        }

        private void MenuPreview_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                report.Show();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print)) { return; }

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

            if (AgentType == 0)
                Reports.InitReport(rpt, "ownercard.frx", false);
            else
                Reports.InitReport(rpt, "agentcard.frx", false);

            tbAgent.Fill(Agent.guid);

            tbAgent.Fill("agenttype", 0);

            tbPlanInfo.Fill();
            rpt.RegisterData(tbPlanInfo.dtData, "planinfodata");
            rpt.RegisterData(tbAgent.dtData, "ownerdata");



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
                tbattach.Name = Path.GetFileName(opf.FileName);
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

        private void BtnAddParenter_Click(object sender, EventArgs e)
        {
            if (!radCollapsiblePanelParenter1.Visible) { radCollapsiblePanelParenter1.Visible = true; return; }
            if (radCollapsiblePanelParenter1.Visible) { radCollapsiblePanelParenter2.Visible = true; return; }
            if (radCollapsiblePanelParenter2.Visible) { radCollapsiblePanelParenter3.Visible = true; return; }
            if (radCollapsiblePanelParenter3.Visible) { radCollapsiblePanelParenter4.Visible = true; return; }

        }

        private void PageHome_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuPreviewAttach_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print)) { return; }

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
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print)) { return; }
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
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Delete)) { return; }

            DataGridAttachments.Rows.RemoveAt(DataGridAttachments.CurrentRow.Index);
        }


        private void BtnScanner_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print)) { return; }

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
                tbattach.Name = Path.GetFileName(frmscan.imgSc.filename);
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
