using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DoctorERP.CustomElements.Flyout;
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
    public partial class UCLandsDataEntry : UserControl
    {

        private Guid guid;
        public BindingSource Bs;
        public bool IsDirty = false, IsLoad = true;
        private bool IsNew = false, ShowConfirmMSG = true, IsProgrammatic = false;
        private readonly string BlockNumber;
        private readonly int CurrentPosition = 0;
        private decimal TotalLand;

        public UCLandsDataEntry(Guid _guid, bool _isNew, string _BlockNumber)
        {
            InitializeComponent();

            guid = _guid;
            BlockNumber = _BlockNumber;
            IsNew = _isNew;

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
            BtnSaleOrder.Click -= MenuSaleOrder_Click;
            BtnSaleOrder.Click += MenuSaleOrder_Click;
            BtnWordExport.Click -= BtnExportWord_Click;
            BtnWordExport.Click += BtnExportWord_Click;
            MenuPreviewAttachment.Click -= MenuPreviewAttach_Click;
            MenuPreviewAttachment.Click += MenuPreviewAttach_Click;
            MenuExtractAttachement.Click -= MenuExtractAttachment_Click;
            MenuExtractAttachement.Click += MenuExtractAttachment_Click;
            MenuDeleteAttachment.Click -= MenuDeleteAttachment_Click;
            MenuDeleteAttachment.Click += MenuDeleteAttachment_Click;
            BtnContract.Click -= BtnContract_Click;
            BtnContract.Click += BtnContract_Click;
            BtnReservation.Click -= BtnReservation_Click;
            BtnReservation.Click += BtnReservation_Click;

            RadFlyoutManager.FlyoutClosed -= this.RadFlyoutManager_FlyoutClosed;
            RadFlyoutManager.FlyoutClosed += this.RadFlyoutManager_FlyoutClosed;

            radLabel8.TextAlignment = ContentAlignment.BottomLeft;
            radLabel2.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel1.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel17.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel16.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel15.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel14.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel3.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel4.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel9.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel12.TextAlignment = ContentAlignment.MiddleLeft;
            radLabel5.TextAlignment = ContentAlignment.MiddleCenter;
            Txtnumber.TextAlignment = ContentAlignment.MiddleCenter;
            Txtstatus.TextAlignment = ContentAlignment.MiddleCenter;
            radTotalText.TextAlignment = ContentAlignment.TopLeft;
            radDesktopAlert1.Popup.RootElement.RightToLeft = true;

            SetData();
            IsLoad = false;
        }



        #region Main Events

        private Dictionary<bool, string> CheckifOprAllow(tbLand land, OperationType.OperationIs operation)
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
                IsSoldorInOrder = !tbBillBody.IsExist("LandGuid", land.guid);
                Pair.Add(IsSoldorInOrder, "تم إنشاء عقد بيع مسبقاً لبطاقة الأرض");
                if (IsSoldorInOrder)
                {
                    Pair.Clear();
                    IsSoldorInOrder = !tbSaleOrderBody.IsExist("LandGuid", land.guid);
                    Pair.Add(IsSoldorInOrder, "تم إنشاء أمر بيع مسبقاً لبطاقة الأرض");
                }
            }
            return Pair;

        }

        private void SetReadOnly(bool IsReadOnly)
        {
            List<RadControl> NotUsedControls = new List<RadControl>()
            { Txtnumber, radWorkFeeValue, radBuildingFeeValue, radVatValue, radWorkFeeWithVat, Txtlastaction, Txtworkfee,Txtbuildingfee, Txtvat };
            foreach (RadControl control in MainPanel.Controls)
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

        private string FormatingNumber(decimal number)
        {
            return $"{number:n}  ريال";
        }

        private void CalcTotal()
        {

            tbTaxDiscount.Fill();
            tbTaxDiscount TaxDiscount = tbTaxDiscount.lstData[0];

            decimal Amount = Txtamount.Value;
            decimal Salesfee = TaxDiscount.salesfee;
            decimal Buildingfee = Chkisbuildingfee.Checked ? TaxDiscount.buildingfee : 0;
            decimal Workfee = Chkisworkfee.Checked ? TaxDiscount.workfee : 0;
            decimal Vatfee = Chkisvat.Checked ? TaxDiscount.vat : 0;

            decimal total = Amount + (Amount * Salesfee / 100) + (Amount * Buildingfee / 100) +
                (Amount * Workfee / 100) + ((Amount * Workfee / 100) * Vatfee / 100);

            Txtbuildingfee.Value = Buildingfee;
            Txtworkfee.Value = Workfee;
            Txtvat.Value = Vatfee;
            radBuildingFeeValue.Text = FormatingNumber(Amount * Buildingfee / 100);
            radWorkFeeValue.Text = FormatingNumber(Amount * Workfee / 100);
            radVatValue.Text = FormatingNumber((Amount * Workfee / 100) * Vatfee / 100);
            radWorkFeeWithVat.Text = FormatingNumber((Amount * Workfee / 100) + ((Amount * Workfee / 100) * Vatfee / 100));


            radAmountBuildingfee.Text = FormatingNumber(Amount + (Amount * TaxDiscount.salesfee / 100) + (Amount * TaxDiscount.buildingfee / 100));
            radLandWorkfee.Text = FormatingNumber(Amount + (Amount * TaxDiscount.salesfee / 100) + (Amount * TaxDiscount.workfee / 100));
            radlandfee.Text = FormatingNumber(Amount + (Amount * TaxDiscount.salesfee / 100) + (Amount * TaxDiscount.workfee / 100) +
                ((Amount * TaxDiscount.workfee / 100) * TaxDiscount.vat / 100));

            radTextBox1.Text = FormatingNumber(Amount + (Amount * TaxDiscount.salesfee / 100) + (Amount * TaxDiscount.workfee / 100) + (Amount * TaxDiscount.buildingfee / 100) +
                ((Amount * TaxDiscount.workfee / 100) * TaxDiscount.vat / 100));

            TotalLand = total;


            CurrencyInfo currency = new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia);
            NumberToWord toWord = new NumberToWord(Amount, currency)
            {
                ArabicPrefixText = string.Empty,
                EnglishSuffixText = string.Empty
            };

            radTotalText.Text = toWord.ConvertToArabic();


        }

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
        public void TackAction()
        {
            bool Confirm = MessageWarning("بطاقات الأراضي", "هل تريد حفظ التغيرات ؟", "إذا ضغت علي زر نعم سوف يتم حفظ البيان المفتوح");
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
                    BtnNew.ScreenTip.Text = "إضافة بطاقة أرض جديدة";
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
                    BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة الأرض";
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
        //private void ShowConfirm()
        //{
        //    //string Header, string Content, string Footer
        //    //radToastNotificationManager1.ShowNotification(0);
        //    ShowDesktopAlert(".أولآ تعديل زر علي الضغط يجب", "البيانات  تعديل يمكنك ذلك بعد", "التعديل لحفظ حفظ زر علي الضغط ثم");
        //    FrmMain.DataHasChanged = true;
        //}

        private void ShowDesktopAlert(string Header, string Content, string ContentHighlight, string Footer)
        {
            //Header = TextHelper.ReverseText(Header);
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
                MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة الأرض أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            tbLand land = (tbLand)Bs.Current;
            var Check = CheckifOprAllow(land, OperType);
            if (!Check.Keys.First() && !Operation.Contains("جديد"))
            {
                MessageException(Operation, "لا يمكن " + Command, Check.Values.First());
                return false;
            }
            if (land is null || land.guid.Equals(Guid.Empty))
            {
                MessageException(Operation, "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة الأرض أولاَ و بعد ذلك يمكنك " + Command);
                return false;
            }
            else if (land.status.Equals("محجوز") && !Operation.Contains("حجز") && !Operation.Contains("جديد"))
            {
                MessageException(Operation, "الأرض محجوزة مسبقاً", "الأرض محجوزة مسبقاً، يجب فك الحجز قبل " + Command);
                return false;
            }
            return true;

        }



        #region Binding
        private void SetData()
        {


            if (FrmMain.PlanGuid != Guid.Empty)
            {
                tbLand.Fill("PlanGuid", FrmMain.PlanGuid);
            }
            else if (FrmMain.PlanGuid == Guid.Empty)
            {
                tbLand.Fill();
            }

            Bs = new BindingSource(tbLand.lstData, string.Empty);
            Bs.PositionChanged += new EventHandler(Bs_PositionChanged);
            BindingNavigatorLands.BindingSource = Bs;

            Cmblandtype.AutoCompleteCustomSource.AddRange(tbLand.GetUniqueList("LandType").ToArray());
            Txtblocknumber.AutoCompleteCustomSource.AddRange(tbLand.GetUniqueList("blocknumber").ToArray());
            Txtdeednumber.AutoCompleteCustomSource.AddRange(tbLand.GetUniqueList("deednumber").ToArray());

            tbPlanInfo.Fill();
            CmbPlanGuid.DataSource = tbPlanInfo.lstData;
            CmbPlanGuid.ValueMember = "guid";
            CmbPlanGuid.DisplayMember = "name";

            foreach (RadControl control in MainPanel.Controls)
            {
                if (control.Name.StartsWith("rad")) { continue; }
                control.DataBindings.Clear();
                string dataMember = control.Name.Remove(0, 3);
                string propertyName = control is RadSpinEditor ? "Value" : control is RadCheckBox ? "Checked"
                    : control is RadMultiColumnComboBox ? "SelectedValue" : "Text";
                Binding ControlBinding = new System.Windows.Forms.Binding(propertyName, Bs, dataMember, false);
                control.DataBindings.Add(ControlBinding);
            }
            Binding SearchBinding = new System.Windows.Forms.Binding("Text", Bs, "number", false);


            if (IsNew) { BtnNew.PerformClick(); return; }

            if (!guid.Equals(Guid.Empty))
            {
                Bs.Position = tbLand.lstData.FindIndex(delegate (tbLand land) { return land.guid == guid; });
                return;
            }
            Bs.MoveLast();
        }

        private void Bs_PositionChanged(object sender, EventArgs e)
        {
            tbLand obj = (tbLand)Bs.Current;

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

        private void FillBindingCurrentData(tbLand land)
        {

            if (land.status == "محجوز")
            {
                BtnReservation.Text = "إلغاء الحجز";
                Txtreservereason.Visible = true;
            }
            else
            {
                BtnReservation.Text = "حجز";
                Txtreservereason.Visible = false;
            }

            CalcTotal();
            FillDataGridAttachments(land.guid);
            FillGridLog(land.guid);
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
            BtnNew.ScreenTip.Text = "حفظ بطاقة الصنف الجديدة";
            BtnNew.Image = Properties.Resources.BtnConform;
            Txtreservereason.Visible = false;
            BtnReservation.Text = "حجز";

            CalcTotal();
            FillGridLog(Guid.Empty);
            FillDataGridAttachments(Guid.Empty);
            SetReadOnly(false);

            foreach (RadControl control in MainPanel.Controls)
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

            tbTaxDiscount.Fill();

            if (tbTaxDiscount.lstData.Count > 0)
            {
                tbTaxDiscount TaxDiscount = tbTaxDiscount.lstData[0];
                Chkisbuildingfee.Checked = TaxDiscount.isbuildingfee;
                Txtbuildingfee.Text = TaxDiscount.buildingfee.ToString();
                Chkisvat.Checked = TaxDiscount.isvat;
                Txtvat.Text = TaxDiscount.vat.ToString();
                Chkisworkfee.Checked = TaxDiscount.isworkfee;
                Txtworkfee.Text = TaxDiscount.workfee.ToString();
            }
            Txtstatus.Text = "متاح";
            Cmblandtype.Text = "ارض";
            if (!BlockNumber.Equals(string.Empty))
            {
                Txtblocknumber.Text = BlockNumber;
            }
            else if (BlockNumber.Equals(string.Empty))
            {
                Txtblocknumber.Text = "0";
            }
            Txtnumber.Text = RadMenueTxtSearch.Text = (tbLand.GetMaxNumber("Number") + 1).ToString();
            CmbPlanGuid.SelectedValue = FrmMain.PlanGuid;

            Txtarea.Focus();

        }

        private bool Add()
        {
            CalcTotal();

            if (ShowConfirmMSG)
            {
                if (!MessageWarning("هل أنت متاكد من الإضافة ؟", "إضافة بطاقة أرض جديدة", "إذا ضغت علي زر نعم سوف يتم إضافة بطاقة الأرض الجديدة"))
                    return false;
            }

            tbTaxDiscount.Fill();
            tbTaxDiscount taxdiscount = tbTaxDiscount.lstData[0];

            tbLand land = new tbLand();

            land.guid = Guid.NewGuid();
            land.amount = Txtamount.Value;
            land.area = Txtarea.Value;
            land.blocknumber = int.Parse(Txtblocknumber.Text);
            land.deednumber = Txtdeednumber.Text;
            land.east = Txteast.Text;
            land.eastdesc = Txteastdesc.Text;
            land.isdiscountfee = taxdiscount.isdiscountfee;
            land.discountfee = taxdiscount.discountfee;
            land.isdiscounttotal = taxdiscount.isdiscounttotal;
            land.issalefee = taxdiscount.issalefee;
            land.salesfee = taxdiscount.salesfee;
            land.discountfeevalue = taxdiscount.discountfeevalue;
            land.discounttotal = taxdiscount.discounttotal;
            land.discounttotalvalue = taxdiscount.discounttotalvalue;
            land.isvat = Chkisvat.Checked;
            land.isworkfee = Chkisworkfee.Checked;
            land.isbuildingfee = Chkisbuildingfee.Checked;
            land.buildingfee = Txtbuildingfee.Value;
            land.vat = Txtvat.Value;
            land.workfee = Txtworkfee.Value;
            land.landtype = Cmblandtype.Text;
            land.lastaction = "عملية إضافة" + " - بتاريخ  " + DateTime.Now.ToString("dd/MM/yyyy") 
                +" - الساعة  " + DateTime.Now.ToString("hh:mm tt") + " - عن طريق المستخدم  " + FrmMain.CurrentUser.name;
            land.north = Txtnorth.Text;
            land.northdesc = Txtnorthdesc.Text;
            land.note = Txtnote.Text;
            land.number = tbLand.GetMaxNumber("Number") + 1;
            land.planguid = tbPlanInfo.lstData.Where(u => u.name == CmbPlanGuid.Text).FirstOrDefault().guid;
            land.reservereason = Txtreservereason.Text;
            land.south = Txtsouth.Text;
            land.southdesc = Txtsouthdesc.Text;
            land.status = Txtstatus.Text;
            land.total = TotalLand;
            land.west = Txtwest.Text;
            land.westdesc = Txtwestdesc.Text;
            land.code = land.number;

            tbPriceLog log = new tbPriceLog
            {
                guid = Guid.NewGuid(),
                username = FrmMain.CurrentUser.name,
                actdate = DateTime.Now.Date,
                actno = 1,
                changedate = DateTime.Now.Date,
                parentguid = land.guid
            };
            log.oldprice = log.newprice = land.amount;

            DBConnect.StartTransAction();
            tbLog.AddLog("إضافة", this.Text, land.code.ToString());
            AddAttachments(land.guid);
            land.Insert();
            log.Insert();

            if (DBConnect.CommitTransAction())
            {
                ShowNotification("إضافة بطاقة أرض جديدة", "تمت عملية إضافة بطاقة الأرض بنجاح", "عملية إضافة بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("إضافة بطاقة أرض جديدة", "تمت العملية", "تمت عملية إضافة بطاقة الأرض بنجاح", "بطاقة الأرض الجديدة تمت إضافتها يمكن القيام بالعمليات عليها الأن.");
                FrmMain.DataHasChanged = true;
            }

            return true;
        }

        private void ShowNotification(string Header, string Content, string Note)
        {
            radToastNotificationManager1.ToastNotifications[0].Xml = "<toast launch=\"readMoreArg\">\r\n  <visual>\r\n    <binding template=\"ToastGeneric\">\r\n   " +
    "   <text>"+ Header + "</text>\r\n   " +
    "   <text>"+ Content + "</text>\r\n  " +
    "    <text placement=\"attribution\">"+ Note + "</text>\r\n    </binding>\r\n  </visual>\r\n</toast>";
            radToastNotificationManager1.ShowNotification(0);

        }
        private bool Edit()
        {

            CalcTotal();

            if (ShowConfirmMSG)
            {
                if (!MessageWarning("هل أنت متاكد من التعديل ؟", "تعديل بطاقة أرض", "إذا ضغت علي زر نعم سوف يتم تعديل بيانات بطاقة الأرض"))
                    return false;
            }
            Guid CurrentGuid = ((tbLand)Bs.Current).guid;
            tbLand land = tbLand.lstData.Where(u => u.guid == CurrentGuid).FirstOrDefault();
            decimal Amount = Txtamount.Value;
            if (land.amount != Amount)
            {
                FrmPriceLog frm = new FrmPriceLog(Guid.Empty, land.guid, land.amount, Amount);
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
                else
                {
                    return false;
                }
            }

            land.amount = Txtamount.Value;
            land.area = Txtarea.Value;
            land.blocknumber = int.Parse(Txtblocknumber.Text);
            land.deednumber = Txtdeednumber.Text;
            land.east = Txteast.Text;
            land.eastdesc = Txteastdesc.Text;
            land.isvat = Chkisvat.Checked;
            land.isworkfee = Chkisworkfee.Checked;
            land.isbuildingfee = Chkisbuildingfee.Checked;
            land.buildingfee = Txtbuildingfee.Value;
            land.vat = Txtvat.Value;
            land.workfee = Txtworkfee.Value;
            land.landtype = Cmblandtype.Text;
            land.lastaction = "عملية تعديل" + " - بتاريخ  " + DateTime.Now.ToString("dd/MM/yyyy")+
                " - الساعة  " + DateTime.Now.ToString("hh:mm tt") + " - عن طريق المستخدم  " + FrmMain.CurrentUser.name;
            land.north = Txtnorth.Text;
            land.northdesc = Txtnorthdesc.Text;
            land.note = Txtnote.Text;
            land.planguid = tbPlanInfo.lstData.Where(u => u.name == CmbPlanGuid.Text).FirstOrDefault().guid;
            land.reservereason = Txtreservereason.Text;
            land.south = Txtsouth.Text;
            land.southdesc = Txtsouthdesc.Text;
            land.status = Txtstatus.Text;
            land.total = TotalLand;
            land.west = Txtwest.Text;
            land.westdesc = Txtwestdesc.Text;

            DBConnect.StartTransAction();
            AddAttachments(land.guid);
            land.Update();
            tbLog.AddLog("تعديل", this.Text, land.code.ToString());
            if (DBConnect.CommitTransAction())
            {
                FillGridLog(land.guid);
                ShowNotification("تعديل بطاقة أرض جديدة", "تمت عملية تعديل بطاقة الأرض بنجاح", "عملية إضافة بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("تعديل بطاقة أرض جديدة", "تمت العملية", "تمت عملية تعديل بطاقة الأرض بنجاح", "تم تعديل بيانات بطاقة الأرض يمكن القيام بالعمليات عليها الأن.");
                FrmMain.DataHasChanged = true;

            }

            return true;
        }

        #endregion




        private void CommandBarLabel1_TextChanged(object sender, EventArgs e)
        {
            string NewTxt = commandBarLabel1.Text.Replace("of", "من");
            commandBarLabel1.Text = NewTxt;

        }
        private void Txt_ValueChanged(object sender, EventArgs e)
        {
            if (sender is RadSpinEditor)
            {
                CalcTotal();
            }
        }
        private void Txtblocknumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void Chkisvat_CheckStateChanged(object sender, EventArgs e)
        {
            radWorkFeeValue.Enabled = Txtworkfee.Enabled = Chkisworkfee.Checked;
            radBuildingFeeValue.Enabled = Txtbuildingfee.Enabled = Chkisbuildingfee.Checked;
            if (Chkisworkfee.Checked)
            {
                radVatValue.Enabled = Txtvat.Enabled = Chkisvat.Checked;

            }
            else if (!Chkisworkfee.Checked)
            {
                radVatValue.Enabled = Txtvat.Enabled = Chkisvat.Checked = false;
            }

            CalcTotal();
        }
        private void RadMenueTxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsDirty) { TackAction(); }

            }

        }

        private void Txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsDirty)
            {
                RadCallout callout = new RadCallout();
                callout.ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle;
                callout.AssociatedControl = radLabel11;
                callout.ArrowDirection = Telerik.WinControls.ArrowDirection.Right;
                callout.AutoClose = true;
                callout.CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle;
                callout.DropShadow = true;
                var cn = sender as RadControl;

                RadCallout.Show(callout, cn, "لقد تم إرسال الملف Lands.pdf \n عبر الإيميل بنجاح", "تمت العملية", "sdfsdfs");

                // ShowDesktopAlert(".أولآ تعديل زر علي الضغط يجب", "البيانات  تعديل يمكنك ذلك بعد", "التعديل لحفظ حفظ زر علي الضغط ثم");
            }
        }

        private void radTotalText_MouseDown(object sender, MouseEventArgs e)
        {
            RadCallout radCallout = new RadCallout();
            radCallout.AssociatedControl = this.radLabel11;
            radLabel11.Text = "تم إحتساب الحافز بناءَ علي : قيمة الأرض + قيمة ضريبة التصرفات العقارية + قيمة عمولة السعي + القيمة المضافة لعمولة السعي";
            radCallout.CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle;
            radCallout.ArrowDirection = Telerik.WinControls.ArrowDirection.Up;
            radCallout.ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle;
            radCallout.AutoClose = true;
            radCallout.DropShadow = true;
            radCallout.Show(this.radTotalText);

        }
        private void Txtstatus_TextChanged(object sender, EventArgs e)
        {
            if (Txtstatus.Text == "متاح") { Txtstatus.BackColor = Color.FromArgb(0, 255, 1); }
            else if (Txtstatus.Text == "مباع") { Txtstatus.BackColor = Color.FromArgb(254, 0, 0); }
            else if (Txtstatus.Text == "محجوز") { Txtstatus.BackColor = Color.FromArgb(255, 255, 0); }

            //if (Txtstatus.Text == "متاح") { Txtstatus.ForeColor = Color.Green; }
            //else if (Txtstatus.Text == "مباع") { Txtstatus.ForeColor = Color.Red; }
            //else if (Txtstatus.Text == "محجوز") { Txtstatus.ForeColor = Color.Yellow; }

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
                        tbLand land = (tbLand)Bs.Current;
                        if (content.Result == DialogResult.OK)
                        {
                            DBConnect.StartTransAction();
                            Txtstatus.Text = land.status = "محجوز";
                            BtnReservation.Text = "إلغاء الحجز";
                            Txtreservereason.Visible = Txtreservereason.Visible = true;
                            land.reservereason = content.ReserveReason;
                            Txtreservereason.Text = content.ReserveReason;
                            land.Update();
                            if (DBConnect.CommitTransAction())
                            {
                                ShowDesktopAlert("حجز بطاقة أرض", "إلغاء حجز بطاقة أرض", "تمت عملية إلغاء حجز البطاقة بنجاح", "تم إلغاء حجز بطاقة الأرض يمكن القيام بالعمليات عليها الأن.");
                                FrmMain.DataHasChanged = true;
                            }

                            string fullName = $"{content.ReserveReason}";
                            RadCallout.Show(callout, this.BtnReservation, $"عملية حجز بطاقة الأرض بسبب حجز {fullName} تمت!", "تمت العملية بنجاح");
                        }
                        else
                        {
                            RadCallout.Show(callout, this.BtnReservation, "فشلت عملية حجز بطاقة الأرض!", "فشلت العملية");
                        }
                    }

                }
                else if (e.Content is FlyoutEmailContent)
                {

                    FlyoutEmailContent content = e.Content as FlyoutEmailContent;
                    RadCallout callout = new RadCallout();

                    if (content != null)
                    {
                        tbLand land = (tbLand)Bs.Current;
                        if (content.Result == DialogResult.OK)
                        {
                            FastReport.Export.Email.EmailExport email = new FastReport.Export.Email.EmailExport();
                            FastReport.Report report = new FastReport.Report();
                            if (Readyreport(report))
                            {
                                report.Prepare();
                                // create an instance of HTML export filter
                                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                                // show the export options dialog and do the export
                                report.Export(export, Application.ExecutablePath + "Lands.pdf");


                                MemoryStream ms = new MemoryStream();
                                using (FileStream file = new FileStream(Application.ExecutablePath + "Lands.pdf", FileMode.Open, FileAccess.Read))
                                    file.CopyTo(ms);
                                DynamicAttachement dynamicAttachement = new DynamicAttachement
                                {
                                    attachData = ms,
                                    attachFileName = Application.ExecutablePath + "Lands.pdf"
                                };

                                tbAttachment tbAttachment = new tbAttachment();

                                SendEmail.SendMail(content.ToMail, "subject", "messagebody", dynamicAttachement, new tbAttachment(), "ali", content.FromMail, content.PassWord
                                    , "smtp-mail.outlook.com", 587, true);

                            }

                            RadCallout.Show(callout, this.BtnEmailExport, $"لقد تم إرسال الملف Lands.pdf \n عبر الإيميل بنجاح", "تمت العملية");
                        }
                        else
                        {
                            RadCallout.Show(callout, this.BtnReservation, "فشلت عملية حجز بطاقة الأرض!", "فشلت العملية");
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


        private void MenuSaleOrder_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إنشاء أمر بيع", OperationType.OperationIs.Add)) { return; }
            tbLand land = (tbLand)Bs.Current;
            FrmSaleOrder frm = new FrmSaleOrder(land);
            frm.Show(this);
        }

        private void BtnContract_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إنشاء عقد بيع", OperationType.OperationIs.Add)) { return; }
            tbLand land = (tbLand)Bs.Current;
            List<tbLand> lst = new List<tbLand> { land };
            FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 0, lst);
            frm.Show();

        }

        private void BtnReservation_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "حجز بطاقة الأرض", OperationType.OperationIs.Edit)) { return; }
            tbLand land = (tbLand)Bs.Current;
            if (land.status.Equals("محجوز"))
            {
                if (!MessageWarning("حجز بطاقة الأرض", "هل أنت متأكد من إلغاء حجز هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم إلغاء حجز بطاقة الأرض \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
                {
                    return;
                }

                DBConnect.StartTransAction();
                Txtstatus.Text = land.status = "متاح";
                BtnReservation.Text = "حجز";
                land.reservereason = string.Empty;
                Txtreservereason.Visible = Txtreservereason.Visible = false;
                land.Update();
                if (DBConnect.CommitTransAction())
                {
                    ShowDesktopAlert("حجز بطاقة أرض", "تمت العملية", "تمت عملية حجز بطاقة الأرض بنجاح", "تم حجز بطاقة الأرض لا يمكن القيام بالعمليات عليها الأن.");
                    FrmMain.DataHasChanged = true;
                }
            }
            else if (land.status.Equals("متاح"))
            {
                if (!MessageWarning("حجز بطاقة الأرض", "هل أنت متأكد من حجز هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم حجز بطاقة الأرض \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
                {
                    return;
                }
                RadFlyoutManager.Show(this, typeof(FlyoutReserveContent));
            }


        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

            if (BtnNew.Text == "جديد")
            {
                if (!Check("إضافة جديد", "إضافة بطاقة أرض جديدة", OperationType.OperationIs.Add)) { return; }

                BtnNew.Text = "حفظ";
                BtnNew.ScreenTip.Text = "حفظ بطاقة الأرض الجديدة";
                BtnNew.Image = Properties.Resources.BtnConform;
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
                BtnNew.ScreenTip.Text = "إضافة بطاقة أرض جديدة";
                BtnNew.Image = Properties.Resources.BtnAddNew;
                SetReadOnly(true);
                IsDirty = false;
                IsNew = false;
                guid = Guid.Empty;
                SetData();

            }

        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {

            tbLand land = (tbLand)Bs.Current;

            if (BtnEdit.Text == "تعديل")
            {
                if (!Check("تعديل بطاقة", "تعديل بطاقة الأرض", OperationType.OperationIs.Edit)) { return; }

                BtnEdit.Text = "حفظ";
                BtnEdit.ScreenTip.Text = "حفظ التعديلات";
                BtnEdit.Image = Properties.Resources.BtnConform;
                BtnAttachment.Enabled = true;
                BtnScanner.Enabled = true;
                IsDirty = true;
                guid = land.guid;
                SetReadOnly(false);
                Txtarea.Focus();
            }
            else if (BtnEdit.Text == "حفظ")
            {
                Edit();
                BtnEdit.Text = "تعديل";
                BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة الأرض";
                BtnEdit.Image = Properties.Resources.BtnEdite;
                BtnAttachment.Enabled = false;
                BtnScanner.Enabled = false;
                SetReadOnly(true);
                IsDirty = false;
                IsNew = false;
                guid = land.guid;
                SetData();

            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!Check("حذف بطاقة", "حذف بطاقة الأرض", OperationType.OperationIs.Delete)) { return; }
            if (!MessageWarning("حذف بطاقة الأرض", "هل أنت متأكد من حذف هذه البطاقة ؟", "إذا ضغط علي زر نعم سوف يتم حذف بطاقة الأرض \n إذا ضغط علي لا سوف يتم تجاهل التغييرات"))
            {
                return;
            }

            DBConnect.StartTransAction();
            tbPriceLog pricelog = new tbPriceLog();
            tbAttachment attach = new tbAttachment();
            tbLand land = (tbLand)Bs.Current;
            tbLog.AddLog("حذف", this.Text, land.code.ToString());
            attach.DeleteBy("ParentGuid", land.guid);
            land.Delete();
            pricelog.DeleteBy("ParentGuid", land.guid);
            if (DBConnect.CommitTransAction())
            {
                ShowNotification("حذف بطاقة أرض", "تمت عملية حذف بطاقة الأرض بنجاح", "عملية حذف بواسطة المستخدم : " + FrmMain.CurrentUser.name + " بتاريخ : " + DateTime.Now.ToString("yyyy/MM/dd") + " الساعة : " + DateTime.Now.ToString("hh:mm tt"));
                ShowDesktopAlert("حذف بطاقة أرض", "تمت العملية", "تمت عملية حذف بطاقة الأرض بنجاح", "تم حذف بطاقة الأرض و لا يوجد لها بيانات في قاعدة البيانات الأن.");
                FrmMain.DataHasChanged = true;
                IsNew = false;
                guid = Guid.Empty;
                SetData();
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إستيراد البيانات", OperationType.OperationIs.Add)) { return; }
            FrmLandImport frm = new FrmLandImport();
            frm.ShowDialog();

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

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!Check(toolmenu.Text, "إرسال بريد إلكتروني", OperationType.OperationIs.Add)) { return; }
            RadFlyoutManager.Show(this, typeof(FlyoutEmailContent));

        }

        private void BtnExportWord_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }

            if (!CheckifOprAllow(new tbLand(), OperationType.OperationIs.Add).Keys.First())
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "تصدير البيانات إلى ملف pdf";
                sfd.Filter = "Word File (.docx ,.doc)|*.docx;*.doc";
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    report.Prepare();
                    // create an instance of HTML export filter
                    FastReport.Export.OoXML.Word2007Export export = new FastReport.Export.OoXML.Word2007Export();
                    // show the export options dialog and do the export
                    if (export.ShowDialog())
                        report.Export(export, sfd.FileName);
                }

            }

        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }

            if (!CheckifOprAllow(new tbLand(), OperationType.OperationIs.Add).Keys.First())
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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
                    // create an instance of HTML export filter
                    FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                    // show the export options dialog and do the export
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
            if (IsDirty) { TackAction(); }
            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                Reports.DesignReport(report);
        }

        private void MenuPreview_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                report.Show();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                report.Print();
        }

        #endregion




        #region report and print
        private bool Readyreport(FastReport.Report rpt)
        {
            tbLand land = (tbLand)Bs.Current;

            if (land is null || land.guid.Equals(Guid.Empty))
            {
                MessageBox.Show("يجب حفظ البطاقة أولا", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            Reports.InitReport(rpt, "land.frx", false);
            tbLand.Fill(land.guid);

            tbAgent.Fill("agenttype", 0);

            tbPlanInfo.Fill();
            rpt.RegisterData(tbPlanInfo.dtData, "planinfodata");
            rpt.RegisterData(tbAgent.dtData, "ownerdata");

            rpt.SetParameterValue("TotalText", radTotalText.Text);

            rpt.SetParameterValue("UserName", FrmMain.CurrentUser.name);

            rpt.RegisterData(tbLand.dtData, "data");
            rpt.RegisterData(tbPlanInfo.dtData, "tbPlanInfo");

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

            OpenFileDialog opf = new OpenFileDialog();
            opf.RestoreDirectory = true;
            opf.Filter = "All Files (*.*)|*.*";

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
                    MessageBox.Show("لا يمكن إضافة مرفق بحجم أكبر من 10 ميغا بايت", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (!FrmMain.CurrentUser.IsAdmin)
            {
                if (!FrmMain.CurrentUser.CanPrint)
                {
                    MessageBox.Show("ليس لديك صلاحية للقيام بهذا الإجراء", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.Filter = "All Files (*.*)|*.*";

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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MenuDeleteAttachment_Click(object sender, EventArgs e)
        {
            if (!FrmMain.CurrentUser.CanDelete)
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataGridAttachments.Rows.RemoveAt(DataGridAttachments.CurrentRow.Index);
        }

        private void AlertTimer_Tick(object sender, EventArgs e)
        {
            if (RadOverlayManager.IsActive) { RadOverlayManager.Close(); }
            this.ParentForm.TopMost = false;
            AlertTimer.Stop();

        }

        private void BtnScanner_Click(object sender, EventArgs e)
        {

            FrmScanImage2 frmscan = new FrmScanImage2();
            if (frmscan.ShowDialog() == DialogResult.OK)
            {
                if (frmscan.imgSc.fbytes.Length <= 0)
                {
                    MessageBox.Show("الملف غير صالح، لم يتم إضافة المرفق", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("لم يتم إضافة المرفق", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        #endregion



        #region Price Log
        private void FillGridLog(Guid landGuid)
        {
            tbPriceLog.Fill("ParentGuid", landGuid);
            DataGridPriceLog.DataSource = tbPriceLog.dtData;

            DataGridPriceLog.Columns[0].IsVisible = false;
            DataGridPriceLog.Columns[1].IsVisible = false;

            DataGridPriceLog.Columns[2].HeaderText = "المستخدم";
            DataGridPriceLog.Columns[3].HeaderText = "وقت الحركة";
            DataGridPriceLog.Columns[4].HeaderText = "السعر القديم";
            DataGridPriceLog.Columns[5].HeaderText = "السعر الجديد";

            DataGridPriceLog.Columns[6].IsVisible = false;
            DataGridPriceLog.Columns[7].IsVisible = false;

            foreach (var row in DataGridPriceLog.Rows)
            {
                row.Cells[4].Value = string.Format("{0:0.00}", double.Parse(row.Cells[4].Value.ToString()));
                row.Cells[5].Value = string.Format("{0:0.00}", double.Parse(row.Cells[5].Value.ToString()));

            }

        }

        private void DataGridPriceLog_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            tbLand land = (tbLand)Bs.Current;
            if (land is null || land.guid.Equals(Guid.Empty)) { return; }
            if (DataGridPriceLog.Rows.Count == 0) { return; }
            Guid guid = (Guid)DataGridPriceLog.Rows[0].Cells[0].Value;
            tbPriceLog pricel = tbPriceLog.FindBy("Guid", guid);
            FrmPriceLog frmtable = new FrmPriceLog(guid, land.guid, pricel.oldprice, pricel.newprice);
            frmtable.ShowDialog();
            FillGridLog(land.guid);
        }

        #endregion




    }
}
