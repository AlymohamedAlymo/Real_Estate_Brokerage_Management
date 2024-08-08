using DoctorERP.Helpers;
using DoctorERP.Helpers.NumberToWord;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.RadToastNotificationManager;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.SplashScreen;

namespace DoctorERP.User_Controls
{
    public partial class UCLandsDataEntry : UserControl
    {

        private Guid guid;
        public BindingSource Bs;
        public bool IsDirty = false;
        private bool IsNew = false, ShowConfirmMSG = true, IsProgrammatic = false;
        private readonly string BlockNumber;
        private int CurrentPosition = 0;
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
            RadFlyoutManager.FlyoutClosed -= this.RadFlyoutManager_FlyoutClosed;
            RadFlyoutManager.FlyoutClosed += this.RadFlyoutManager_FlyoutClosed;
            radLabel8.TextAlignment = ContentAlignment.BottomLeft;
            radLabel2.TextAlignment = ContentAlignment.TopLeft;
            radLabel1.TextAlignment = ContentAlignment.TopLeft;
            radLabel6.TextAlignment = ContentAlignment.TopLeft;
            radLabel5.TextAlignment = ContentAlignment.TopLeft;
            radLabel7.TextAlignment = ContentAlignment.TopLeft;
            radLabel10.TextAlignment = ContentAlignment.TopLeft;
            radTotalText.TextAlignment = ContentAlignment.TopLeft;
            radDesktopAlert1.Popup.RootElement.RightToLeft = true;

            SetData();


        }

        #region Main Events

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
            Txtstatus.AutoCompleteCustomSource.AddRange(tbLand.GetUniqueList("status").ToArray());
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
                    if (IsDirty)
                    {
                        MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                }

                IsProgrammatic = true;
                Bs.Position = CurrentPosition - 1;
                MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            BtnNew.Text = "(F1) حفظ";
            BtnNew.ScreenTip.Text = "حفظ بطاقة الصنف الجديدة";
            BtnNew.Image = Properties.Resources.GlyphCheck_small;
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
            land.lastaction = "إضافة" + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " " + FrmMain.CurrentUser.name;
            land.north = Txtnorth.Text;
            land.northdesc = Txtnorthdesc.Text;
            land.note = Txtnote.Text;
            land.number = tbLand.GetMaxNumber("Number") + 1;
            land.planguid = tbPlanInfo.lstData.Where(u => u.name == CmbPlanGuid.Text).FirstOrDefault().guid;
            land.reservereason = Txtreservereason.Text;
            land.south = Txtsouth.Text;
            land.southdesc = Txtsouthdesc.Text;
            land.status = Txtstatus.Text;
            land.total = Txttotal.Value;
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
                ShowConfirm();
            }

            return true;
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
            decimal Total = Txttotal.Value;
            if (land.total != Total)
            {
                FrmPriceLog frm = new FrmPriceLog(Guid.Empty, land.guid, land.amount, Total);
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
            land.lastaction = "تعديل" + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " " + FrmMain.CurrentUser.name;
            land.north = Txtnorth.Text;
            land.northdesc = Txtnorthdesc.Text;
            land.note = Txtnote.Text;
            land.planguid = tbPlanInfo.lstData.Where(u => u.name == CmbPlanGuid.Text).FirstOrDefault().guid;
            land.reservereason = Txtreservereason.Text;
            land.south = Txtsouth.Text;
            land.southdesc = Txtsouthdesc.Text;
            land.status = Txtstatus.Text;
            land.total = Txttotal.Value;
            land.west = Txtwest.Text;
            land.westdesc = Txtwestdesc.Text;

            DBConnect.StartTransAction();
            AddAttachments(land.guid);
            land.Update();
            tbLog.AddLog("تعديل", this.Text, land.code.ToString());
            if (DBConnect.CommitTransAction())
            {
                FillGridLog(land.guid);
                ShowConfirm();
            }

            return true;
        }

        #endregion

        private bool CheckifOprAllow(tbLand land, OperationType.OperationIs operation)
        {
            bool GoHead = false, IsEnable = false;

            switch (operation)
            {
                case OperationType.OperationIs.Add:
                    GoHead = FrmMain.CurrentUser.CanAdd;
                    break;
                case OperationType.OperationIs.Edit:
                    GoHead = FrmMain.CurrentUser.CanEdit;
                    break;
                case OperationType.OperationIs.Delete:
                    GoHead = FrmMain.CurrentUser.CanDelete;
                    break;
                case OperationType.OperationIs.Print:
                    GoHead = FrmMain.CurrentUser.CanPrint;
                    break;
                default:
                    break;
            }

            if (GoHead)
            {
                IsEnable = !tbBillBody.IsExist("LandGuid", land.guid) || tbSaleOrderBody.IsExist("LandGuid", land.guid);
            }
            return IsEnable;

        }

        private void SetReadOnly(bool IsReadOnly)
        {
            List<RadControl> NotUsedControls = new List<RadControl>() 
            { Txtnumber, radWorkFeeValue, radBuildingFeeValue, radVatValue, radWorkFeeWithVat, Txttotal, Txtlastaction, Txtworkfee,Txtbuildingfee, Txtvat };
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

        private void ShowConfirm()
        {
            radToastNotificationManager1.ShowNotification(0);
            FrmConfirm frmconfirm = new FrmConfirm();
            frmconfirm.ShowDialog();
            FrmMain.DataHasChanged = true;
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

            Txtvat.Value = Vatfee;
            Txtworkfee.Value = Workfee;
            Txtbuildingfee.Value = Buildingfee;
            radWorkFeeValue.Text = (Amount * Workfee / 100).ToString("000,0.00");
            radBuildingFeeValue.Text = (Amount * Buildingfee / 100).ToString("000,0.00");
            radVatValue.Text = ((Amount * Workfee / 100) * Vatfee / 100).ToString("000,0.00");
            radWorkFeeWithVat.Text = ((Amount * Workfee / 100) + ((Amount * Workfee / 100) * Vatfee / 100)).ToString("000,0.00");
            Helpers.NumberToWord.CurrencyInfo currency = new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia);
            ToWord toWord = new ToWord(total, currency)
            {
                ArabicPrefixText = string.Empty,
                EnglishSuffixText = string.Empty
            };
            Txttotal.Value = total;
            radTotalText.Text = toWord.ConvertToArabic();

        }

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
                if (IsDirty) { CurrentPosition = Bs.Position; }

            }

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
                //AllowCancel = true,
                Footnote = new RadTaskDialogFootnote("ملحوظة: " + FootNote),
                CommandAreaButtons = {
                    RadTaskDialogButton.OK,
                }

            };
            page.CommandAreaButtons[0].Text = "موافق";
            RadTaskDialogButton result = RadTaskDialog.ShowDialog(page, RadTaskDialogStartupLocation.CenterScreen);
            return true;
        }

        private void TackAction()
        {
            bool Confirm = MessageWarning("بطاقات الأراضي", "هل تريد حفظ التغيرات ؟", "إذا ضغت علي زر نعم سوف يتم حفظ البيان المفتوح");
            if (Confirm)
            {
                ShowConfirmMSG = false;
                if (BtnNew.Text == "(F1) حفظ") { Add(); }
                else if (BtnEdit.Text == "(F2) حفظ") { Edit(); }
            }
        }

        private void ShowDesktopAlert(string Header, string Content, string Footer)
        {

            radDesktopAlert1.CaptionText = "<html><b>\nبطاقات الأراضي";
            radDesktopAlert1.ContentText = "<html><i>" +
                Header +
                "</i><b><span><color=Blue>" +
                "\n" + Content + "\n" +
                "</span></b>" +
                Footer;
            radDesktopAlert1.ContentImage = Properties.Resources.information50;
            radDesktopAlert1.Opacity = 0.9f;
            radDesktopAlert1.Show();

        }
        #endregion

        #region Buttons
        private void BtnContract_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }
            tbLand land = (tbLand)Bs.Current;
            if (land is null || land.guid.Equals(Guid.Empty))
            {
                MessageException("بطاقات الأراضي", "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة الأرض أولاَ و بعد ذلك يمكنك اصدار عقد البيع");
                return;
            }
            if (land.status.Equals("مباع"))
            {
                MessageException("بطاقات الأراضي", "الأرض مباعة مسبقاً", "لا يمكن اصدار عقد بيع لبطاقة أرض مباعة مسبقاَ");
                return;

            }
            if (land.status.Equals("محجوز"))
            {
                MessageException("بطاقات الأراضي", "الأرض محجوزة مسبقاً", "الأرض محجوزة مسبقاً، يجب فك الحجز قبل بيعها");
                return;
            }

            List<tbLand> lst = new List<tbLand> { land };
            FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 0, lst);
            frm.Show();

        }

        private void RadFlyoutManager_FlyoutClosed(FlyoutClosedEventArgs e)
        {
            Action action = new Action(() =>
            {
                var content = e.Content as FlyoutInteractiveContent;
                if (content != null)
                {

                    tbLand land = (tbLand)Bs.Current;


                    RadCallout callout = new RadCallout();
                    callout.ArrowDirection = Telerik.WinControls.ArrowDirection.Up;
                    //callout.ThemeName = this.CurrentThemeName;
                    if (content.Result == DialogResult.OK)
                    {
                        DBConnect.StartTransAction();
                        Txtstatus.Text = land.status = "محجوز";
                        BtnReservation.Text = "إلغاء الحجز";
                        Txtreservereason.Visible = Txtreservereason.Visible = true;
                        land.reservereason = content.FirstName;
                        Txtreservereason.Text = content.FirstName;
                        land.Update();
                        if (DBConnect.CommitTransAction())
                        {
                            ShowConfirm();
                        }

                        string fullName = $"{content.FirstName} {content.LastName}";
                        RadCallout.Show(callout, this.BtnReservation, $"The student {fullName} was registered!", "Success");
                    }
                    else
                    {
                        RadCallout.Show(callout, this.BtnReservation, "The student was not registered!", "Cancelled");
                    }
                }

            });

            this.Invoke(action);
        }


        private void BtnReservation_Click(object sender, EventArgs e)
        {
            if (IsDirty) { TackAction(); }

            if (Bs.Current == null)
            {
                MessageException("بطاقات الأراضي", "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة الأرض أولاَ و بعد ذلك يمكنك حجز البطاقة");
                return;
            }
            tbLand land = (tbLand)Bs.Current;
            if (land is null || land.guid.Equals(Guid.Empty))
            {
                MessageException("بطاقات الأراضي", "يجب حفظ البطاقة أولا", "قم بحفظ بطاقة الأرض أولاَ و بعد ذلك يمكنك حجز البطاقة");
                return;
            }

            if (land.status.Equals("متاح"))
            {
                if (!MessageWarning("حجز بطاقة أرض", "هل أنت متأكد من حجز هذا الصنف ؟", "إذا ضغت علي زر نعم سوف يتم تسجيل بطاقة الأرض بحالة محجوز"))
                {
                    return;
                }

                RadFlyoutManager.Show(this, typeof(FlyoutInteractiveContent));


                //FrmReservereason frm = new FrmReservereason(Txtreservereason.Text);
                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    DBConnect.StartTransAction();
                //    Txtstatus.Text = land.status = "محجوز";
                //    BtnReservation.Text = "إلغاء الحجز";
                //    Txtreservereason.Visible = Txtreservereason.Visible = true;
                //    land.reservereason = frm.reservereason;
                //    Txtreservereason.Text = frm.reservereason;
                //    land.Update();
                //    if (DBConnect.CommitTransAction())
                //    {
                //        ShowConfirm();
                //    }
                //}
            }
            else if (land.status.Equals("مباع"))
            {
                MessageBox.Show("لا يمكن حجز الصنف المباع", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (land.status.Equals("أمر بيع"))
            {
                MessageBox.Show("لا يمكن حجز الصنف مضاف لأمر البيع", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (land.status.Equals("محجوز"))
            {
                if (MessageBox.Show("هل أنت متأكد من إلغاء حجز هذا الصنف ؟", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    return;

                DBConnect.StartTransAction();
                Txtstatus.Text = land.status = "متاح";
                BtnReservation.Text = "حجز";
                land.reservereason = string.Empty;
                Txtreservereason.Visible = Txtreservereason.Visible = false;
                land.Update();
                if (DBConnect.CommitTransAction())
                {
                    ShowConfirm();

                }
            }

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!CheckifOprAllow(new tbLand(), OperationType.OperationIs.Add))
            {
                MessageBox.Show("ليس لديك صلاحية", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (BtnNew.Text == "(F1) جديد")
            {
                if (IsDirty)
                {
                    MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }

                BtnNew.Text = "(F1) حفظ";
                BtnNew.ScreenTip.Text = "حفظ بطاقة الأرض الجديدة";
                BtnNew.Image = Properties.Resources.GlyphCheck_small;
                IsDirty = true;
                guid = Guid.Empty;
                Bs.AddNew();
                Bs.MoveLast();
                NewFill();
            }
            else if (BtnNew.Text == "(F1) حفظ")
            {
                Add();
                BtnNew.Text = "(F1) جديد";
                BtnNew.ScreenTip.Text = "إضافة بطاقة أرض جديدة";
                BtnNew.Image = Properties.Resources.plus;
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
            if (!CheckifOprAllow(land, OperationType.OperationIs.Edit))
            {
                MessageBox.Show("ليس لديك صلاحية", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (BtnEdit.Text == "(F2) تعديل")
            {
                if (IsDirty)
                {
                    MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }

                BtnEdit.Text = "(F2) حفظ";
                BtnEdit.ScreenTip.Text = "حفظ التعديلات";
                BtnEdit.Image = Properties.Resources.GlyphCheck_small;
                BtnAttachment.Enabled = true;
                BtnScanner.Enabled = true;
                IsDirty = true;
                guid = land.guid;
                SetReadOnly(false);
                Txtarea.Focus();
            }
            else if (BtnEdit.Text == "(F2) حفظ")
            {
                Edit();
                BtnEdit.Text = "(F2) تعديل";
                BtnEdit.ScreenTip.Text = "تعديل بيانات بطاقة الأرض";
                BtnEdit.Image = Properties.Resources.edit;
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
            if (IsDirty)
            {
                MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            tbLand land = (tbLand)Bs.Current;
            if (!CheckifOprAllow(land, OperationType.OperationIs.Delete))
            {
                MessageBox.Show("ليس لديك صلاحية", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (tbBillBody.IsExist("LandGuid", land.guid))
            {
                MessageBox.Show("لا يمكن حذف الصنف لأنه مستخدم في العقود", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            if (tbSaleOrderBody.IsExist("LandGuid", land.guid))
            {
                MessageBox.Show("لا يمكن حذف الصنف لأنه مستخدم في أوامر البيع", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }


            if (MessageBox.Show("هل أنت متأكد من الحذف ؟", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            tbPriceLog pricelog = new tbPriceLog();
            tbAttachment attach = new tbAttachment();

            DBConnect.StartTransAction();

            tbLog.AddLog("حذف", this.Text, land.code.ToString());
            attach.DeleteBy("ParentGuid", land.guid);
            land.Delete();
            pricelog.DeleteBy("ParentGuid", land.guid);

            if (DBConnect.CommitTransAction())
            {
                ShowConfirm();
                IsNew = false;
                guid = Guid.Empty;
                SetData();

            }

        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            if (!CheckifOprAllow(new tbLand(), OperationType.OperationIs.Add))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandImport frm = new FrmLandImport();
            frm.ShowDialog();

        }

        private void BtnResfresh_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                DialogResult msgboxres = MessageBox.Show("هل تريد حفظ التغيرات", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (msgboxres == DialogResult.Yes)
                {
                    ShowConfirmMSG = false;
                    if (IsNew) { Add(); }
                    else if (!IsNew) { Edit(); }
                }
            }

            guid = Guid.Empty;
            SetData();
            ShowConfirm();

        }

        private void NavigatorBtn_Click(object sender, EventArgs e)
        {
            tbLand land = (tbLand)Bs.Current;
            if (IsDirty) 
            {
                CurrentPosition = land.number;
                //MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }
        private void MenuDesign_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                Reports.DesignReport(report);
        }

        private void MenuPreview_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            FastReport.Report report = new FastReport.Report();
            if (Readyreport(report))
                report.Show();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                MessageBox.Show("يجب إتخاذ إجراء في العملية الحالية أولاَ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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
                if (e.Button == MouseButtons.Right)
                {
                    //var hit = DataGridAttachments.HitTest(e.X, e.Y);
                    //DataGridAttachments.CurrentCell = DataGridAttachments[hit.ColumnIndex, hit.RowIndex];
                    //DataGridAttachments.ContextMenuStrip = ConMenuAttachments;

                }
            }
            catch
            {
                DataGridAttachments.ContextMenuStrip = null;

            }
        }

        private void MenuPreviewAttach_Click(object sender, EventArgs e)
        {
            //byte[] bfiles = (byte[])DataGridAttachments.rows[0].cell, DataGridAttachments.CurrentRow.Index].Value;
            //Guid guid = new Guid(DataGridAttachments[colguid.Name, DataGridAttachments.CurrentRow.Index].Value.ToString());
            //string filename = (string)DataGridAttachments[colfilename.Name, DataGridAttachments.CurrentRow.Index].Value;


            //tbAttachment attach = tbAttachment.FindByFull("guid", guid);

            //if (bfiles.Length <= 1)
            //    FileHelper.RunFile(attach.FileName, attach.FileData);
            //else
            //    FileHelper.RunFile(filename, bfiles);


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

            //byte[] bfiles = (byte[])DataGridAttachments[colfiledata.Name, DataGridAttachments.CurrentRow.Index].Value;
            //Guid guid = new Guid(DataGridAttachments[colguid.Name, DataGridAttachments.CurrentRow.Index].Value.ToString());
            //tbAttachment attach = tbAttachment.FindByFull("guid", guid);
            //sfd.FileName = attach.Name;


            //if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    try
            //    {
            //        if (bfiles.Length <= 1)
            //            FileHelper.ByteArraytoFile(attach.FileData, sfd.FileName);
            //        else
            //            FileHelper.ByteArraytoFile(bfiles, sfd.FileName);
            //        ShowConfirm();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
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

        private void PageAttachments_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsDirty)
            {
                ShowDesktopAlert(".أولآ تعديل زر علي الضغط يجب", "البيانات  تعديل يمكنك ذلك بعد", "التعديل لحفظ حفظ زر علي الضغط ثم");

            }
        }

        private void radDesktopAlert1_Opening(object sender, System.ComponentModel.CancelEventArgs args)
        {
            //var arg = args as RadPopupOpeningEventArgs;
            //arg.CustomLocation = new Point(1951, 967);
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

        }
        private void DataGridPriceLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbLand land = (tbLand)Bs.Current;

            if (land is null || land.guid.Equals(Guid.Empty))
            {
                return;
            }
            if (DataGridPriceLog.Rows.Count == 0) { return; }
            Guid guid = (Guid)DataGridPriceLog.Rows[0].Cells[2].Value;
            tbPriceLog pricel = tbPriceLog.FindBy("Guid", guid);
            FrmPriceLog frmtable = new FrmPriceLog(guid, land.guid, pricel.oldprice, pricel.newprice);
            frmtable.ShowDialog();
            FillGridLog(land.guid);

        }

        #endregion



    }
}
