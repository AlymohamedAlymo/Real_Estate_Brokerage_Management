using Real_Estate_Management.CustomElements;
using Real_Estate_Management.Helpers;
using DoctorHelper.Helpers;
using DoctorHelper.Messages;
using Real_Estate_Management.Data.DataBase;
using Real_Estate_Management.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.SplashScreen;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace Real_Estate_Management.User_Controls
{
    public partial class UCLawyer : System.Windows.Forms.UserControl
    {

        private Guid CurrentGuid;
        private bool IsLoad = true, IsNew = false, IsProgrammatic = false, IsDirty = false, ShowConfirmMSG = true;
        public BindingSource Bs = new BindingSource();
        private int CurrentPosition = 0;
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
            BtnExportExcelData.Click -= BtnExportExcelData_Click;
            BtnExportExcelData.Click += BtnExportExcelData_Click;
            BtnSaleOrder.Click -= MenuSaleOrder_Click;
            BtnSaleOrder.Click += MenuSaleOrder_Click;
            BtnContract.Click -= BtnContract_Click;
            BtnContract.Click += BtnContract_Click;
            BtnReservation.Click -= BtnReservation_Click;
            BtnReservation.Click += BtnReservation_Click;
            Bs.PositionChanged += Bs_PositionChanged;
            RadFlyoutManager.FlyoutClosed -= this.RadFlyoutManager_FlyoutClosed;
            RadFlyoutManager.FlyoutClosed += this.RadFlyoutManager_FlyoutClosed;
            RadFlyoutManager.ContentCreated -= this.RadFlyoutManager_ContentCreated;
            RadFlyoutManager.ContentCreated += this.RadFlyoutManager_ContentCreated;

            radLabel5.TextAlignment = ContentAlignment.MiddleCenter;
            TxtNumber.TextAlignment = ContentAlignment.MiddleCenter;
            TxtStatues.TextAlignment = ContentAlignment.MiddleCenter;
            radlabelBookings.TextAlignment = ContentAlignment.MiddleCenter;
            radLabel8.TextAlignment = ContentAlignment.MiddleCenter;

            radPageView1.RootElement.Children[0].Children[0].Children[0].Children[2].Visibility = ElementVisibility.Hidden;
            this.MainContainer.PanelElement.PanelFill.Visibility = ElementVisibility.Hidden;
            this.MainContainer.BackgroundImage = Real_Estate_Management.Properties.Resources.Background;
            this.MainContainer.BackgroundImageLayout = ImageLayout.Stretch;


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

            Bs.DataSource = TbLawyer_Rep.lstData;
            BindingNavigatorClient.BindingSource = Bs;

            TbPlans_Rep.Fill();
            CmbPlanGuid.DataSource = TbPlans_Rep.lstData;
            CmbPlanGuid.ValueMember = "Guid";
            CmbPlanGuid.DisplayMember = "Name";
            CmbPlanGuid.SelectedValue = FrmMain.PlanGuid;
            CmbPlanGuid.Columns[0].IsVisible = false;
            CmbPlanGuid.Columns[1].IsVisible = false;
            CmbPlanGuid.Columns[2].IsVisible = false;
            CmbPlanGuid.Columns[7].IsVisible = false;
            CmbPlanGuid.Columns[8].IsVisible = false;

            CmbPlanGuid.Columns[3].HeaderText = "رقم الصك";
            CmbPlanGuid.Columns[4].HeaderText = "اسم المخطط";
            CmbPlanGuid.Columns[5].HeaderText = "المدينة";
            CmbPlanGuid.Columns[6].HeaderText = "الموقع";

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

            BtnReservation.Text = "تنشيط";
            IsLoad = false;

            if (IsNew)
            {
                BtnNew.PerformClick();
                return;
            }

            if (!CurrentGuid.Equals(Guid.Empty))
            {
                var ObjLawer = TbLawyer_Rep.lstData.Where(u => u.Guid == CurrentGuid).ToList();
                if (ObjLawer.Any())
                {
                    var index = TbLawyer_Rep.lstData.IndexOf(ObjLawer.First());
                    Bs.Position = index;
                    BtnReservation.Text = ObjLawer.First().Statues.ToString() == "غير نشط" ? "تنشيط" : "إلغاء تنشيط";
                    return;
                }
            }

            Bs.MoveLast();

            tbLawyer Obj = (tbLawyer)Bs.Current;
            BtnReservation.Text = Obj.Statues.ToString() == "غير نشط" ? "تنشيط" : "إلغاء تنشيط";

        }

        private void Bs_PositionChanged(object sender, EventArgs e)
        {
            if (IsLoad) { return; }
            tbLawyer obj = (tbLawyer)Bs.Current;

            if (Bs.Current == null) { return; }
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
            BtnReservation.Text = obj.Statues.ToString() == "غير نشط" ? "تنشيط" : "إلغاء التنشيط";
            attachmentControl1.FillDataGridAttachments(obj.Guid);
            BtnDelete.Enabled = true;
            BtnEdit.Enabled = true;
            SetReadOnly(true);

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

            switch (BtnNew.Text)
            {
                case "جديد":
                    Bs.AddNew();
                    Bs.MoveLast();
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
            attachmentControl1.BtnAttachment.Enabled = true;
            attachmentControl1.BtnScanner.Enabled = true;
            attachmentControl1.AttachmentsContextMenu.Items[3].Enabled = true;

            BtnDelete.Enabled = false;
            BtnEdit.Enabled = false;
            radPageView1.SelectedPage = PageHome;
            BtnNew.Image = Real_Estate_Management.Properties.Resources.BtnConform;
            BtnNew.Text = "حفظ";
            BtnNew.ScreenTip.Text = "حفظ بطاقة المحامي الجديدة";
            BtnReservation.Text = "إلغاء التنشيط";
            attachmentControl1.FillDataGridAttachments(Guid.Empty);
            SetReadOnly(false);

            foreach (RadControl control in MainContainer.Controls)
            {
                control.DataBindings.Clear();
            }



            TxtStatues.Text = "نشط";
            TxtNumber.Text = (TbLawyer_Rep.GetMaxNumber("Number") + 1).ToString();
            CmbPlanGuid.SelectedValue = FrmMain.PlanGuid;

            TxtName.Focus();
        }

        private void Add()
        {
            if (ShowConfirmMSG && !Messages.MessageWarning("هل أنت متاكد من الإضافة ؟", "إضافة بطاقة محامي", "إذا ضغط علي زر نعم سوف يتم اضافة بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
            {
                IsDirty = false;
                IsNew = false;
                CurrentGuid = Guid.Empty;
                attachmentControl1.BtnAttachment.Enabled = false;
                attachmentControl1.BtnScanner.Enabled = false;
                attachmentControl1.AttachmentsContextMenu.Items[3].Enabled = false;

                BtnDelete.Enabled = true;
                BtnEdit.Enabled = true;
                BtnNew.Image = Real_Estate_Management.Properties.Resources.BtnAddNew;
                BtnNew.Text = "جديد";
                BtnNew.ScreenTip.Text = "إضافة بطاقة محامي جديدة";
                BtnReservation.Text = "إلغاء التنشيط";
                SetReadOnly(true);
                SetData();

                return;
            }

            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                RadCallout.Show(callout, TxtName, "ادخل اسم المحامي أولاً", "اسم المحامي غير مدخل", "ثم إضغط علي زر حفظ لحفظ الملف");
                return;
            }

            Guid _PlanGuid = Guid.Empty;
            if (CmbPlanGuid.SelectedValue != null)
            {
                _PlanGuid = (Guid)CmbPlanGuid.SelectedValue;

            }
            else
            {
                _PlanGuid = TbPlans_Rep.lstData.Where(u => u.Name == CmbPlanGuid.Text).FirstOrDefault().Guid;
            }

            Guid _Guid = Guid.NewGuid();
            DateTime CurrentDate = DBConnect.GetServerDate();
            string _LastAction = string.Format("عملية إضافة بواسطة المستخدم : {0} -  بتاريخ : {1} - الساعة : {2}",
                FrmMain.CurrentUser.name,
                CurrentDate.ToString("yyyy/MM/dd"),
                CurrentDate.ToString("hh:mm tt"));
            string internedLastAction = string.Intern(_LastAction);


            DBConnect.StartTransAction();
            TbLog_Rep.AddNew(_PlanGuid, "إضافة", "بطاقة محامي", $"إضافة بطاقة محامي بأسم {TxtName.Text}");
            TbLawyer_Rep.AddNew(_PlanGuid, _Guid, int.Parse(TxtNumber.Text), TxtName.Text, TxtMobile.Text, TxtMobileAdd.Text,
                TxtIDNumber.Text, TxtVatNumber.Text, TxtOfficeName.Text, TxtEmail.Text, TxtNote.Text, internedLastAction);
            attachmentControl1.AddAttachments(_Guid);
            if (DBConnect.CommitTransAction())
            {
                Notifications.ShowNotification(this.components, "إضافة بطاقة محامي جديدة", "تمت عملية إضافة بطاقة المحامي بنجاح", internedLastAction);
                Notifications.ShowDesktopAlert(this.components,"إضافة بطاقة محامي جديدة", "تمت العملية", "تمت عملية إضافة بطاقة المحامي بنجاح", "بطاقة المحامي الجديدة تمت إضافتها يمكن القيام بالعمليات عليها الأن.");
                IsDirty = false;
                IsNew = false;
                CurrentGuid = _Guid;
                attachmentControl1.BtnAttachment.Enabled = false;
                attachmentControl1.BtnScanner.Enabled = false;
                attachmentControl1.AttachmentsContextMenu.Items[3].Enabled = false;

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
            switch (BtnEdit.Text)
            {
                case "تعديل":
                    if (!Check("تعديل بطاقة", "تعديل بطاقة المحامي", OperationType.OperationIs.Edit, true)) { return; }
                    BtnEdit.Text = "حفظ";
                    BtnEdit.ScreenTip.Text = "حفظ التعديلات";
                    BtnEdit.Image = Real_Estate_Management.Properties.Resources.BtnConform;
                    attachmentControl1.BtnAttachment.Enabled = true;
                    attachmentControl1.BtnScanner.Enabled = true;
                    attachmentControl1.AttachmentsContextMenu.Items[3].Enabled = true;
                    BtnDelete.Enabled = false;
                    BtnNew.Enabled = false;

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
            tbLawyer Obj = (tbLawyer)Bs.Current;

            if (ShowConfirmMSG && !Messages.MessageWarning("هل أنت متاكد من التعديل ؟", "تعديل بطاقة محامي", "إذا ضغط علي زر نعم سوف يتم تعديل بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
            {
                BtnEdit.Text = "تعديل";
                BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة المحامي";
                BtnEdit.Image = Real_Estate_Management.Properties.Resources.BtnEdite;
                attachmentControl1.BtnAttachment.Enabled = false;
                attachmentControl1.BtnScanner.Enabled = false;
                attachmentControl1.AttachmentsContextMenu.Items[3].Enabled = false;
                BtnDelete.Enabled = true;
                BtnNew.Enabled = true;

                SetReadOnly(true);
                IsDirty = false;
                IsNew = false;
                CurrentGuid = Obj.Guid;
                SetData();

                return;
            }

            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                RadCallout.Show(callout, TxtName, "ادخل اسم المحامي أولاً", "اسم المحامي غير مدخل", "ثم إضغط علي زر حفظ لحفظ التغييرات");
                return;
            }

            Guid _PlanGuid = Guid.Empty;
            if (CmbPlanGuid.SelectedValue != null)
            {
                _PlanGuid = (Guid)CmbPlanGuid.SelectedValue;

            }
            else
            {
                _PlanGuid = TbPlans_Rep.lstData.Where(u => u.Name == CmbPlanGuid.Text).FirstOrDefault().Guid;
            }

            DateTime CurrentDate = DBConnect.GetServerDate();
            string _LastAction = string.Format("عملية تعديل بواسطة المستخدم : {0} -  بتاريخ : {1} - الساعة : {2}",
                FrmMain.CurrentUser.name,
                CurrentDate.ToString("yyyy/MM/dd"),
                CurrentDate.ToString("hh:mm tt"));
            string internedLastAction = string.Intern(_LastAction);

            Obj.PlanGuid = _PlanGuid;
            Obj.Statues = TxtStatues.Text;
            Obj.Name = TxtName.Text;
            Obj.IDNumber = TxtIDNumber.Text;
            Obj.Mobile = TxtMobile.Text;
            Obj.MobileAdd = TxtMobileAdd.Text;
            Obj.Email = TxtEmail.Text;
            Obj.VatNumber = TxtVatNumber.Text;
            Obj.OfficeName = TxtOfficeName.Text;
            Obj.Note = TxtNote.Text;
            Obj.LastAction = internedLastAction;

            DBConnect.StartTransAction();
            TbLog_Rep.AddNew(_PlanGuid, "تعديل", "بطاقة محامي", $"تعديل بطاقة محامي بأسم {TxtName.Text}");
            TbLawyer_Rep.Update(Obj);
            attachmentControl1.AddAttachments(Obj.Guid);

            if (DBConnect.CommitTransAction())
            {

                Notifications.ShowNotification(this.components, "تعديل بطاقة محامي", "تمت عملية تعديل بطاقة المحامي بنجاح", internedLastAction);
                Notifications.ShowDesktopAlert(this.components,"تعديل بطاقة محامي", "تمت العملية", "تمت عملية تعديل بطاقة المحامي بنجاح", "تم تعديل بيانات بطاقة المحامي يمكن القيام بالعمليات عليها الأن.");
                BtnEdit.Text = "تعديل";
                BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة المحامي";
                BtnEdit.Image = Real_Estate_Management.Properties.Resources.BtnEdite;
                attachmentControl1.BtnAttachment.Enabled = false;
                attachmentControl1.BtnScanner.Enabled = false;
                attachmentControl1.AttachmentsContextMenu.Items[3].Enabled = false;
                BtnDelete.Enabled = true;
                BtnNew.Enabled = true;

                SetReadOnly(true);
                IsDirty = false;
                IsNew = false;
                CurrentGuid = Obj.Guid;
                SetData();

            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            tbLawyer Obj = (tbLawyer)Bs.Current;

            if (!Check("حذف بطاقة", "حذف بطاقة المحامي", OperationType.OperationIs.Delete, true)) { return; }

            if (ShowConfirmMSG && !Messages.MessageWarning("هل أنت متاكد من حذف البطاقة ؟", "حذف بطاقة المحامي", "إذا ضغط علي زر نعم سوف يتم حذف بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
            {
                IsDirty = false;
                IsNew = false;
                CurrentGuid = Obj.Guid;
                SetData();

                return;
            }
            int NextPosition = Bs.Position + 1 > Bs.Count - 1 ? Bs.Count - 1 : Bs.Position + 1;
            var ListData = Bs.DataSource as List<tbLawyer>;
            Guid NextGuid = ListData[NextPosition].Guid;
            DBConnect.StartTransAction();
            TbLog_Rep.AddNew(Obj.PlanGuid, "حذف", "بطاقة محامي", $"حذف بطاقة محامي بأسم {TxtName.Text}");
            new tbAttachment().DeleteBy("ParentGuid", Obj.Guid);
            TbLawyer_Rep.Delete(Obj.Guid);

            if (DBConnect.CommitTransAction())
            {
                DateTime CurrentDate = DBConnect.GetServerDate();
                string _LastAction = string.Format("عملية حذف بواسطة المستخدم : {0} -  بتاريخ : {1} - الساعة : {2}",
                    FrmMain.CurrentUser.name,
                    CurrentDate.ToString("yyyy/MM/dd"),
                    CurrentDate.ToString("hh:mm tt"));
                string internedLastAction = string.Intern(_LastAction);

                Notifications.ShowNotification(this.components, "حذف بطاقة محامي", "تمت عملية حذف بطاقة المحامي بنجاح", internedLastAction);
                Notifications.ShowDesktopAlert(this.components, "حذف بطاقة محامي", "تمت العملية", "تمت عملية حذف بطاقة المحامي بنجاح", "تم حذف بطاقة المحامي و لا يوجد لها بيانات في قاعدة البيانات الأن.");
                IsDirty = false;
                IsNew = false;
                CurrentGuid = NextGuid;
                SetData();
            }
        }


        #endregion

        #region Main Events
        public bool Check(string Operation, string Command, OperationType.OperationIs OperType, bool IsAction)
        {
            if (IsAction && IsDirty) { TackAction(); }
            if (!FrmMain.IsPermissionGranted(Operation))
            {
                Messages.MessageException(Operation, "لا تملك صلاحية", "لا تملك صلاحية للقيام ب " + Command + " \n تواصل مع المدير.");
                return false;
            }
            if (Bs.Current == null)
            {
                Messages.MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة المحامي أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            tbLawyer Obj = (tbLawyer)Bs.Current;
            Dictionary<bool, string> Check = CheckifOprAllow(Obj, OperType, IsAction);
            if (!Check.Keys.First() && !Operation.Contains("جديد"))
            {
                Messages.MessageException(Operation, "لا يمكن " + Command, Check.Values.First());
                return false;
            }
            if (Obj is null)
            {
                Messages.MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة المحامي أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            return true;

        }

        private Dictionary<bool, string> CheckifOprAllow(tbLawyer DataRow, OperationType.OperationIs operation, bool IsAction)
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
            bool Confirm = Messages.MessageWarning("بطاقة المحامي", "هل تريد حفظ التغيرات ؟", "إذا ضغت علي زر نعم سوف يتم حفظ البيان المفتوح");
            if (Confirm)
            {
                ShowConfirmMSG = false;
                if (BtnNew.Text == "حفظ") { BtnNew.PerformClick(); }
                else if (BtnEdit.Text == "حفظ") { BtnEdit.PerformClick(); }
                ShowConfirmMSG = true;

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
                    attachmentControl1.BtnAttachment.Enabled = false;
                    attachmentControl1.BtnScanner.Enabled = false;
                    attachmentControl1.AttachmentsContextMenu.Items[3].Enabled = false;

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
                RadControl cn = sender as RadControl;
                RadCallout.Show(callout, cn, "لا يمكن تعديل بيانات البطاقة قبل الضغط علي زر تعديل أولاً", "تعديل البيانات", "ثم الضغط علي زر حفظ لحفظ التغييرات");
                args.Cancel = true;
            }

        }
        private void AlertTimer_Tick(object sender, EventArgs e)
        {
            if (RadOverlayManager.IsActive) { RadOverlayManager.Close(); }
            this.ParentForm.Activate();
            AlertTimer.Stop();

        }

        private void CommandBarLabel1_TextChanged(object sender, EventArgs e)
        {
            string NewTxt = commandBarLabel1.Text.Replace("of", "من");
            commandBarLabel1.Text = NewTxt;

        }
        private void TxtViewOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            RadControl cn = sender as RadControl;
            RadCallout.Show(callout, cn, "لا يمك تعديل هذا البيان", "تعديل البيانات", "بيان للعرض فقط و لا يمكن تغييره");

        }
        private void TxtReadOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsDirty)
            {
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
        private void RadFlyoutManager_ContentCreated(ContentCreatedEventArgs e)
        {
            try
            {
                Telerik.WinControls.Extensions.Action action = new Telerik.WinControls.Extensions.Action(() =>
                {
                    if (e.Content is FlyoutReserveContent content)
                    {
                        content.radLabelHeader.Text = "تنشيط بطاقة المحامي";
                        content.radLabelTxt.Text = "سبب التنشيط :";
                        content.RadTxtReserveReason.EmbeddedLabelText = "ادخل سبب التنشيط";

                    }
                });

                this.Invoke(action);

            }
            catch { }
        }

        private void RadFlyoutManager_FlyoutClosed(FlyoutClosedEventArgs e)
        {
            Action action = new Action(() =>
            {
                if (e.Content is FlyoutReserveContent)
                {

                    FlyoutReserveContent content = e.Content as FlyoutReserveContent;
                    if (content != null)
                    {
                        tbLawyer Obj = (tbLawyer)Bs.Current;
                        if (content.Result == DialogResult.OK)
                        {
                            DBConnect.StartTransAction();
                            TxtStatues.Text = Obj.Statues = "غير نشط";
                            BtnReservation.Text = "تنشيط";
                            TbLawyer_Rep.Update(Obj);
                            if (DBConnect.CommitTransAction())
                            {
                                Notifications.ShowDesktopAlert(this.components, "إلغاء تنشيط بطاقة محامي", "إلغاء تنشيط بطاقة المحامي", "تمت عملية إلغاء تنشيط البطاقة بنجاح", "تم إلغاء تنشيط بطاقة المحامي لا يمكن القيام بالعمليات عليها الأن.");
                            }

                            string ReserveReason = $"{content.ReserveReason}";
                            RadCallout.Show(callout, this.BtnReservation, $"عملية إلغاء تنشيط بطاقة المحامي بسبب{ReserveReason} تمت!", "تمت العملية بنجاح");
                        }
                        else
                        {
                            RadCallout.Show(callout, this.BtnReservation, "فشلت عملية إلغاء تنشيط بطاقة المحامي!", "فشلت العملية");
                        }
                    }

                }
                else if (e.Content is FlyoutEmailContent)
                {

                    FlyoutEmailContent content = e.Content as FlyoutEmailContent;
                    RadCallout callout = new RadCallout();

                    if (content != null)
                    {
                        //tbLawyer Obj = (tbLawyer)Bs.Current;
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
            tbLawyer Obj = (tbLawyer)Bs.Current;
            if (Obj.Statues.Equals("غير نشط"))
            {
                if (ShowConfirmMSG && !Messages.MessageWarning("تنشيط بطاقة المحامي", "هل أنت متأكد من تنشيط هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم تنشيط بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
                {
                    return;
                }

                DBConnect.StartTransAction();
                TxtStatues.Text = Obj.Statues = "نشط";
                BtnReservation.Text = "إلغاء تنشيط";
                TbLawyer_Rep.Update(Obj);
                if (DBConnect.CommitTransAction())
                {
                    Notifications.ShowDesktopAlert(this.components, "تنشيط بطاقة المحامي", "تمت العملية", "تمت عملية تنشيط بطاقة المحامي بنجاح", "تم تنشيط بطاقة المحامي لا يمكن القيام بالعمليات عليها الأن.");
                }
            }
            else
            {
                if (ShowConfirmMSG && !Messages.MessageWarning("تنشيط بطاقة المحامي", "هل أنت متأكد من إلغاء تنشيط هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم إلغاء تنشيط بطاقة المحامي \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
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

            tbLawyer Obj = (tbLawyer)Bs.Current;
            var Row = TbLawyer_Rep.dtData.AsEnumerable().Where(u => u.ItemArray[1].Equals(Obj.Guid)).CopyToDataTable();
            ExcelXLSX.ExportToExcelFromDataTable(Row);

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
            RadOverlayManager.Show(this);

            if (IsDirty) { TackAction(); }

            CurrentGuid = Guid.Empty;
            SetData();
            Notifications.ShowDesktopAlert(this.components, "تحديث البيانات", "تمت العملية", "تمت عملية تحديث البيانات بنجاح", "تم تحديث البيانات و يمكن القيام بالعمليات عليها الأن.");

            AlertTimer.Start();
            if (RadOverlayManager.IsActive) { RadOverlayManager.Close(); }
            this.ParentForm.Activate();
        }

        private void NavigatorBtn_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }
        }
        #endregion

        #region report and print

        private bool Readyreport(FastReport.Report rpt)
        {
            tbLawyer Obj = (tbLawyer)Bs.Current;

            if (Obj is null || Obj.Guid.Equals(Guid.Empty))
            {
                Notifications.ShowDesktopAlert(this.components, "الطباعة", "فشلت العملية", "يجب حفظ البطاقة أولاً", "يجب حفظ البطاقة أولاً ثم يمكنك طباعتها.");
                return false;
            }

            Reports.InitReport(rpt, "LawerRpt.frx", false);

            var Plan = TbPlans_Rep.lstData.Where(u => u.Guid.Equals(Obj.PlanGuid)).FirstOrDefault();
            tbAgent.Fill(Plan.OwnerGuid);
            rpt.SetParameterValue("PlanName", Plan.Name);
            rpt.SetParameterValue("OwnerName", tbAgent.lstData[0].name);
            rpt.SetParameterValue("PalnNumber", Plan.Number);
            DataTable Row = TbLawyer_Rep.dtData.AsEnumerable().Where(u => u.ItemArray[1].Equals(Obj.Guid)).CopyToDataTable();
            rpt.RegisterData(Row, "data");

            return true;
        }

        private void MenuDesign_Click(object sender, EventArgs e)
        {
            RadOverlayManager.Show(this);

            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }
            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
            {

                Reports.DesignReport(report);

                RadOverlayManager.Close();

            }


        }

        private void MenuPreview_Click(object sender, EventArgs e)
        {
            RadOverlayManager.Show(this);

            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
            {
                report.Show();

                RadOverlayManager.Close();
                this.ParentForm.Activate();


            }


        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            RadOverlayManager.Show(this);

            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
            {
                RadOverlayManager.Close();
                this.ParentForm.Activate();


                report.Print();

            }


        }


        #endregion

        #region Attachments

        private void BtnAddAttachment_Click(object sender, EventArgs e)
        {
            if (!Check("اضافة مرفق", "تصدير البيانات", OperationType.OperationIs.Edit, false)) { return; }

            OpenFileDialog opf = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = "All Files (*.*)|*.*"
            };

            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAttachment tbattach = new tbAttachment();
                tbattach.Guid = Guid.NewGuid();
                tbattach.ParentGuid = ((tbLawyer)Bs.Current).Guid;
                tbattach.Name = System.IO.Path.GetFileName(opf.FileName);
                tbattach.FileName = tbattach.Name;
                tbattach.FileData = FileHelper.FileToByteArray(opf.FileName);
                if (tbattach.FileData.Length > 10000000)
                {
                    DoctorHelper.Messages.Notifications.ShowDesktopAlert(this.components, "إضافة المرفقات", "فشلت العملية",
                        "تمت إضافة المرفقات", "لا يمكن إضافة مرفق بحجم أكبر من 10 ميغا بايت.");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                tbattach.FileSize = string.Format(new FileSizeFormatProvider(), "{0:fs}", tbattach.FileData.Length);
                tbAttachment.dtData.Rows.Add(tbattach.Guid, tbattach.ParentGuid, tbattach.Name, tbattach.FileName, tbattach.FileSize, tbattach.FileData);
                attachmentControl1.DataGridAttachments.DataSource = tbAttachment.dtData;
                this.Cursor = Cursors.Arrow;

            }

        }

        private void BtnScanner_Click(object sender, EventArgs e)
        {
            if (!Check("جلب من الماسح الضوئي", "تصدير البيانات", OperationType.OperationIs.Add, false)) { return; }

            FrmScanImage2 frmscan = new FrmScanImage2();
            if (frmscan.ShowDialog() == DialogResult.OK)
            {
                if (frmscan.imgSc.fbytes.Length <= 0)
                {
                    Notifications.ShowDesktopAlert(this.components, "إضافة مرفقات", "لم يتم إضافة المرفق", "الملف غير صالح", "حدد ملف صالح ثم اعد المحاولة مرة آخرى.");
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
                attachmentControl1.DataGridAttachments.DataSource = tbAttachment.lstData;
            }
            else
            {
                Notifications.ShowDesktopAlert(this.components, "إضافة مرفقات", "لم يتم إضافة المرفق", "تم إلغاء العملية", "قام المستخدم بإلغاء العملية.");
            }

        }

        private void MenuPreviewAttach_Click(object sender, EventArgs e)
        {
            if (!Check("معاينة مرفق", "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }

            byte[] bfiles = (byte[])attachmentControl1.DataGridAttachments.CurrentRow.Cells[5].Value;
            Guid guid = new Guid(attachmentControl1.DataGridAttachments.CurrentRow.Cells[0].Value.ToString());
            string filename = attachmentControl1.DataGridAttachments.CurrentRow.Cells[3].Value.ToString();

            tbAttachment attach = tbAttachment.FindByFull("guid", guid);

            if (bfiles.Length <= 1)
                FileHelper.RunFile(attach.FileName, attach.FileData);
            else
                FileHelper.RunFile(filename, bfiles);

        }

        private void MenuExtractAttachment_Click(object sender, EventArgs e)
        {
            if (!Check("استخراج مرفق", "تصدير البيانات", OperationType.OperationIs.Print, false)) { return; }
            SaveFileDialog sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Filter = "All Files (*.*)|*.*"
            };

            byte[] bfiles = (byte[])attachmentControl1.DataGridAttachments.CurrentRow.Cells[5].Value;
            Guid guid = new Guid(attachmentControl1.DataGridAttachments.CurrentRow.Cells[0].Value.ToString());
            tbAttachment attach = tbAttachment.FindByFull("guid", guid);

            string filename = attach.Guid == Guid.Empty ? attachmentControl1.DataGridAttachments.CurrentRow.Cells[2].Value.ToString()
                : attach.Name;
            sfd.FileName = filename;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (bfiles.Length <= 1)
                        FileHelper.ByteArraytoFile(attach.FileData, sfd.FileName);
                    else
                        FileHelper.ByteArraytoFile(bfiles, sfd.FileName);

                    Notifications.ShowDesktopAlert(this.components, "إستخراج المرفقات", "تمت العملية", "تمت إستخراج المرفقات بنجاح", "تم عملية إستخراج المرفقات بنجاح و يمكن القيام بالعمليات عليها الأن.");

                }
                catch (Exception)
                {
                    Notifications.ShowDesktopAlert(this.components, "إستخراج المرفقات", "حدث خطأ", "لم يتم إستخراج المرفقات بنجاح", "حدث خطأ عند إستخراج المرفقات.");
                }
            }
        }

        private void MenuDeleteAttachment_Click(object sender, EventArgs e)
        {
            if (!Check("حذف مرفق", "تصدير البيانات", OperationType.OperationIs.Delete, false)) { return; }

            attachmentControl1.DataGridAttachments.Rows.RemoveAt(attachmentControl1.DataGridAttachments.CurrentRow.Index);
        }



        #endregion

    }
}
