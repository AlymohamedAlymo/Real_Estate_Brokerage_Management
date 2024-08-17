using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Globalization;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.Windows.Diagrams.Core;
using System.Threading;
using System.ComponentModel;
using Telerik.WinControls.Data;
using DoctorERP.User_Controls;
using DoctorERP.CustomElements;

namespace DoctorERP
{
    public partial class FrmMain : Telerik.WinControls.UI.RadToolbarForm
    {
        public static tbUsers CurrentUser = new tbUsers();
        public static tbAppInfo AppInfo = new tbAppInfo();
        private bool IsViewScreen = false;
        private readonly CultureInfo CultureAR = new CultureInfo("ar-EG");
        public static bool AllowEdit = false;
        private tbLand CurrentLand = null;
        public static List<tbLand> DataLandList = null;
        private ListViewDataItemGroup CurrentBlock = null;
        public static Guid PlanGuid;

        public FrmMain()
        {

            InitializeComponent();
            this.Size = new System.Drawing.Size(1280, 1024);
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            IsViewScreen = File.Exists(AppSetting.GetAppPath() + "settings.txt");

            CultureAR.DateTimeFormat.DayNames = new string[7]{
                "السبت",
                "الأحد",
                "الاثنين",
                "الثلاثاء",
                "الاربعاء",
                "الخميس",
                "الجمعة"};

            Thread.CurrentThread.CurrentCulture = CultureAR;
            Thread.CurrentThread.CurrentUICulture = CultureAR;

            ///* THeme ///////////////////////////////////////

            RadDropDownListElement themesDropDown = new RadDropDownListElement();
            themesDropDown.MinSize = new System.Drawing.Size(200, 40);
            themesDropDown.EnableElementShadow = false;
            themesDropDown.FindDescendant<FillPrimitive>().BackColor = System.Drawing.Color.Transparent;
            themesDropDown.DropDownStyle = RadDropDownStyle.DropDownList;
            themesDropDown.Items.AddRange(new RadListDataItem[]
            {
                new RadListDataItem("Material") { Image =   Properties.Resources.default_small },
                new RadListDataItem("MaterialPink") { Image = Properties.Resources.pink_blue_small },
                new RadListDataItem("MaterialTeal") { Image = Properties.Resources.teal_red_small },
                new RadListDataItem("MaterialBlueGrey") { Image = Properties.Resources.blue_grey_green_small },
                new RadListDataItem("Windows11") { Image = Properties.Resources.blue_grey_green_small },
                new RadListDataItem("Windows8") { Image = Properties.Resources.windows8 },
                new RadListDataItem("VisualStudio2022Light") { Image = Properties.Resources.blue_grey_green_small },
                new RadListDataItem("Office2019Light") { Image = Properties.Resources.blue_grey_green_small },
                new RadListDataItem("Fluent") { Image = Properties.Resources.fluent },
                new RadListDataItem("TelerikMetro") { Image = Properties.Resources.telerik_metro },
                new RadListDataItem("TelerikMetroBlue") { Image = Properties.Resources.fluent_dark },
                new RadListDataItem("telerikMetroTouchTheme1") { Image = Properties.Resources.telerik_metro },

            });
            
            themesDropDown.SelectedIndex = 0;
            themesDropDown.SelectedIndexChanged += delegate (object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
            {
                if (e.Position > -1)
                {
                    ThemeResolutionService.ApplicationThemeName = themesDropDown.Items[e.Position].Text;
                    ControlTraceMonitor.AnalyticsMonitor?.TrackAtomicFeature("ThemeChanged." + ThemeResolutionService.ApplicationThemeName);
                }

            };
            ThemeResolutionService.ApplicationThemeName = themesDropDown.SelectedItem.Text;
            ControlTraceMonitor.AnalyticsMonitor?.TrackAtomicFeature("ThemeChanged." + ThemeResolutionService.ApplicationThemeName);
            MenuTools.Items.Add(themesDropDown);
            ////////////////////////////////////////////////////////////

            RadPageViewStripItem item = this.radPageViewPage1.Item as RadPageViewStripItem;
            item.ButtonsPanel.CloseButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            item.ButtonsPanel.PinButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

            ////// Icon List View ////////////////////
            this.LandsListView.ShowGroups = false;
            this.LandsListView.EnableGrouping = true;
            GroupDescriptor groupByValue = new GroupDescriptor(new SortDescriptor[]
            {
                new SortDescriptor("blocknumber", ListSortDirection.Ascending)
            });

            this.LandsListView.GroupDescriptors.Add(groupByValue);

            this.LandsListView.ViewType = ListViewType.IconsView;
            this.LandsListView.ItemSize = new System.Drawing.Size(45, 45);

            this.LandsListView.ItemSpacing = 8;
            this.LandsListView.AllowEdit = false;
            this.LandsListView.EnableFiltering = true;
            this.LandsListView.HotTracking = true;

            this.LandsListView.RootElement.BackColor = System.Drawing.Color.Transparent;
            this.LandsListView.BackColor = System.Drawing.Color.Transparent;
            this.PageViewCardsHome.PageBackColor = System.Drawing.Color.Transparent;

            this.LandsListView.ListViewElement.DrawFill = false;
            this.LandsListView.ListViewElement.ViewElement.BackColor = System.Drawing.Color.Transparent;
            this.LandsListView.ListViewElement.Padding = new System.Windows.Forms.Padding(15, 8, 8, 8);

            this.LandsListView.RootElement.EnableElementShadow = false;
            this.overviewMainContainer.BackgroundImage = Properties.Resources.Background;
            this.overviewMainContainer.BackgroundImageLayout = ImageLayout.Stretch;
            this.overviewMainContainer.PanelElement.PanelFill.Visibility = ElementVisibility.Collapsed;
            this.LandsListView.GroupItemSize = new System.Drawing.Size(0, 25);
            ////////////////////////////////////
            ///
            LabelDataBase.TextAlignment = ContentAlignment.MiddleCenter;
            LabelServer.TextAlignment = ContentAlignment.MiddleCenter;
            radLabelElement1.TextAlignment = ContentAlignment.MiddleCenter;

            SplitButtonUSER.Items["MenuItemExit"].Click -= MenuExit_Click;
            SplitButtonUSER.Items["MenuItemChangeUser"].Click -= MenuLogOut_Click;
            SplitButtonUSER.Items["MenuButtonUserMangment"].Click -= MenuUsersManagement_Click;
            SplitButtonUSER.Items["MenuButtonUsers"].Click -= MenuLogRpt_Click;

            SplitButtonUSER.Items["MenuItemExit"].Click += MenuExit_Click;
            SplitButtonUSER.Items["MenuItemChangeUser"].Click += MenuLogOut_Click;
            SplitButtonUSER.Items["MenuButtonUserMangment"].Click += MenuUsersManagement_Click;
            SplitButtonUSER.Items["MenuButtonUsers"].Click += MenuLogRpt_Click;

            foreach (var Bubbleitem in bubbleBar1.Items)
            {
                Bubbleitem.Click -= BubbleBar_ButtonClick;
                Bubbleitem.Click += BubbleBar_ButtonClick;
            }


        }

        #region Main Events
        public void FrmMain_Load(object sender, EventArgs e)
        {
            if (!DBConnect.TryToConnect(AppSetting.DataBase))
            {
                FrmConnection connection = new FrmConnection();
                if (connection.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("سوف يتم إعادة تشغيل البرنامج ليتم تطبيق إعدادات الإتصال الجديدة", Application.ProductName, MessageBoxButtons.OK);
                    Application.Restart();
                }

                Application.Exit();
            }
            if (ChangeDataBase(true))
            {
                this.Visible = true;
            }
            else
            {
                Application.Exit();
            }

            bubbleBar1.Visible = panel2.Visible= true;

            this.Size = new System.Drawing.Size(1160, 649);

        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string strTempPath = Settings.GetAppPath();
                strTempPath += "temp";
                Directory.CreateDirectory(strTempPath);
                strTempPath += "\\";
                DirectoryInfo directory = new DirectoryInfo(strTempPath);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
            }
            catch
            {

            }
        }

        public static bool DataHasChanged = false;
        public void LoadLandData()
        {

            if (PlanGuid != Guid.Empty)
            {
                tbLand.Fill("PlanGuid", PlanGuid);
            }
            else if (PlanGuid == Guid.Empty)
            {
                tbLand.Fill();
            }

            DataLandList = tbLand.lstData.OrderBy(u => u.number).ToList();
            if (DataHasChanged)
            {
                LandsListView.DataSource = DataLandList;
                LandsListView.DisplayMember = "number";
                LandsListView.ValueMember = "guid";
                LandsListView.SelectedItem = null;

            }

        }

        private bool ChangeDataBase(bool OpenDef)
        {
            string defDatabase = string.Empty;

            if (File.Exists(AppSetting.GetAppPath() + DBConnect.DataBaseDefFile) && OpenDef)
            {
                defDatabase = File.ReadAllText(AppSetting.GetAppPath() + DBConnect.DataBaseDefFile);
                AppSetting.DataBase = defDatabase;

                if (IsViewScreen)
                {
                    DBConnect.TryToConnect(AppSetting.DataBase);
                    tbUsers.Fill();
                    CurrentUser = tbUsers.lstData[0];
                    LoadSettings();

                    MainMenu.Visible = false;
                    bubbleBar1.Visible = false;
                    return true;
                }

                FrmLogin frmlog = new FrmLogin();
                if (frmlog.ShowDialog(this) == DialogResult.OK)
                {
                    CurrentUser = frmlog.user;
                    LoadSettings();

                    return true;
                }
                else
                    return false;

            }
            else
            {

                FrmDataBases frmdb = new FrmDataBases(AppSetting.DataBase, OpenDef);
                if (frmdb.ShowDialog() == DialogResult.OK)
                {
                    FrmLogin frmlog = new FrmLogin();
                    if (frmlog.ShowDialog() == DialogResult.OK)
                    {
                        CurrentUser = frmlog.user;
                        LoadSettings();

                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    if (OpenDef)
                    {
                        Application.Exit();
                        return false;
                    }
                    return false;
                }
            }

        }

        private  void LoadSettings()
        {
            tbAppInfo.Fill();
            AppInfo = tbAppInfo.lstData[0];

            this.Text = AppInfo.AppTitle + " - " + CurrentUser.name;

            DataGUIAttribute.CurrencyFormat = AppInfo.CurrecnyFormat;
            DataGUIAttribute.QtyFormat = AppInfo.QtyFormat;

            //if (AppInfo.background.Length > 0)
            //    PnlMain.BackgroundImage = FileHelper.ByteArrayToImage(AppInfo.background);
            //else
            //    PnlMain.BackgroundImage = null;

            if (AppInfo.Logo.Length > 0)
                PicLogo.Image = FileHelper.ByteArrayToImage(AppInfo.Logo);
            else
                PicLogo.Image = null;

            tbPlanInfo.Fill();
            tbVATSettings.Fill();
            tbEditPassword.Fill();

            CompanyNameBarLabel.Text = tbVATSettings.lstData[0].CompanyName;
            CompanyNameLabel.Text = tbVATSettings.lstData[0].CompanyName;
            SplitButtonUSER.Text = FrmMain.CurrentUser.name;
            LabelDataBase.Text = "قاعدة البيانات: " + DBConnect.DBConnection.Database;
            LabelServer.Text = "المخدم: " + DBConnect.DBConnection.DataSource;
            MenuHeaderCurrentUser.Text = "المستخدم الحالي: " + FrmMain.CurrentUser.name;
            AllowEdit = FrmMain.CurrentUser.CanEdit;
            TmrStatic.Enabled = true;

            FillStatic();
            DataHasChanged= true;
            LoadLandData();

            MenuStripItems h = new MenuStripItems();
            List<RadMenuItem> lst = h.GetAllMenuStripItems(MainMenu.NearItems);
            string[] arrExMenu = { MenuFile.Name, MenuCards.Name, MenuActions.Name, MenuPay.Name, MenuReports.Name, MenuTools.Name };
            foreach (var item in lst)
            {
                if (!IsPermissionGranted(item.Text) && !arrExMenu.Contains(item.Name))
                {
                    item.Visibility = ElementVisibility.Hidden;
                }
            }

            DataHasChanged = false;


        }

        public static bool IsPermissionGranted(string PermissionName)
        {
            if (FrmMain.CurrentUser.IsAdmin)
                return true;

            tbUsersPermissions userper = tbUsersPermissions.FindBy("UserGuid", FrmMain.CurrentUser.guid, PermissionName);
            return userper.PermissionValue;
        }

        private void TmrStatic_Tick(object sender, EventArgs e)
        {
            FillStatic();

        }

        private void FillStatic()
        {
            int totalorders = vwSalesOrderRpt.GetSalesOrderRemainCount();
            BtnSalesOrder.Text = string.Format("أمر بيع ({0})", totalorders);

            if (totalorders <= 0)
                BtnSalesOrder.Image = global::DoctorERP.Properties.Resources.NoSalesOrder;
            else
                BtnSalesOrder.Image = global::DoctorERP.Properties.Resources.SalesOrder;
            LoadLandData();
        }

        private void RBallLands_CheckStateChanged(object sender, EventArgs e)
        {
            if (radRadioButton1.CheckState == CheckState.Checked)
            {
                this.LandsListView.DataSource = null;

                this.LandsListView.DataSource = DataLandList.OrderBy(u => u.blocknumber).ToList();

                this.LandsListView.DisplayMember = "blocknumber";
                this.LandsListView.ValueMember = "guid";

                LandsListView.SelectedItem = null;
                this.LandsListView.ShowGroups = true;

            }
            if (radRadioButton1.CheckState == CheckState.Unchecked)
            {
                this.LandsListView.DataSource = null;

                this.LandsListView.DataSource = DataLandList.OrderBy(u => u.number).ToList();

                this.LandsListView.DisplayMember = "number";
                this.LandsListView.ValueMember = "guid";

                LandsListView.SelectedItem = null;


                this.LandsListView.ShowGroups = false;

            }

        }

        private void PageViewCardsHome_PageRemoving(object sender, Telerik.WinControls.UI.RadPageViewCancelEventArgs e)
        {

            if (PageViewCardsHome.Pages.Count == 1)
            {
                e.Cancel = true;
                return;
            }
            if (e.Page.Name == "LandsCard")
            {
                try
                {
                    UCLandsCards ucLand = e.Page.Controls[0] as UCLandsCards;
                    if (ucLand.IsDirty)
                    {
                        ucLand.TackAction();
                        return;
                    }
                }
                catch { }
                
            }
        }

        private void OverviewLandsView_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {

            if (e.VisualItem is IconListViewVisualItem)
            {
                e.VisualItem = new LandIconListViewVisualItem();
            }
            else
            {
                if (!e.DataItem.Text.Contains("بلوك رقم")) 
                {
                    e.DataItem.Text = "بلوك رقم : " + e.DataItem.Text;
                }
            }

        }

        public void BubbleBar_ButtonClick(object sender, EventArgs e)
        {
            RadButtonElement control = sender as RadButtonElement;
            if (control.Name == "BtnAgentCard")
            {
                MenuAgentBuyCard.PerformClick();

            }
            else if (control.Name == "BtnAgentCard")
            {
                MenuSellBill.PerformClick();

            }
            else if (control.Name == "BtnPayContract")
            {
                MenuPayContract.PerformClick();

            }
            else if (control.Name == "BtnCalc")
            {
                MenuCalc.PerformClick();

            }
            else if (control.Name == "BtnPlanInfoMap")
            {
                tbAttachment attach = tbAttachment.FindByFull("ParentGuid", tbPlanInfo.lstData[0].guid, "map");
                if (attach.Guid.Equals(Guid.Empty))
                {
                    MessageBox.Show("لم يتم إضافة ملف مرفق في معلومات المخطط", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                FileHelper.RunFile(attach.FileName, attach.FileData);

            }
            else if (control.Name == "BtnPlanInfoArea")
            {
                tbAttachment attach = tbAttachment.FindByFull("ParentGuid", tbPlanInfo.lstData[0].guid, "area");
                if (attach.Guid.Equals(Guid.Empty))
                {
                    MessageBox.Show("لم يتم إضافة ملف مرفق في معلومات المخطط", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                FileHelper.RunFile(attach.FileName, attach.FileData);

            }
            else if (control.Name == "BtnPlnInfoPrices")
            {
                tbAttachment attach = tbAttachment.FindByFull("ParentGuid", tbPlanInfo.lstData[0].guid, "prices");
                if (attach.Guid.Equals(Guid.Empty))
                {
                    MessageBox.Show("لم يتم إضافة ملف مرفق في معلومات المخطط", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                FileHelper.RunFile(attach.FileName, attach.FileData);

            }
            else if (control.Name == "BtnOther")
            {
                tbAttachment attach = tbAttachment.FindByFull("ParentGuid", tbPlanInfo.lstData[0].guid, "other");
                if (attach.Guid.Equals(Guid.Empty))
                {
                    MessageBox.Show("لم يتم إضافة ملف مرفق في معلومات المخطط", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }

                FileHelper.RunFile(attach.FileName, attach.FileData);

            }
            else if (control.Name == "BtnSalesOrder")
            {
                MenuSaleOrder.PerformClick();

            }
            else if (control.Name == "BtnLogOut")
            {
                MenuLogOut.PerformClick();

            }
            else if (control.Name == "btnExit")
            {
                MenuExit.PerformClick();

            }


        }

        private void MenuStripOverviewLandsView_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null) { return; }

            if (e.ClickedItem.Name == "MenuShowLandCard")
            {
                if (!IsPermissionGranted("بطاقات الأراضي"))
                {
                    MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                RadOverlayManager.Show(this);

                tbLand land = new tbLand();
                if (LandsListView.SelectedItem != null) { land = DataLandList.Where(u => u.guid == (Guid)LandsListView.SelectedItem.Value).FirstOrDefault(); }
                RadPageViewPage enumerableIterator = PageViewCardsHome.Pages.Where(u => u.Name == "LandsCard").FirstOrDefault();
                if (enumerableIterator == null)
                {
                    RadPageViewPage radPageViewPage = new RadPageViewPage();
                    radPageViewPage.Name = "LandsCard";
                    radPageViewPage.Text = "بطاقات الأراضي";

                    DoctorERP.User_Controls.UCLandsCards uCLands = new User_Controls.UCLandsCards(land.guid, false, string.Empty);
                    uCLands.Dock = DockStyle.Fill;
                    
                    radPageViewPage.Controls.Add(uCLands);
                    PageViewCardsHome.Pages.Add(radPageViewPage);
                    PageViewCardsHome.SelectedPage = radPageViewPage;
                    radPageViewPage.Item.ButtonsPanel.CloseButton.ToolTipText = "إغلاق الصفحة";
                    radPageViewPage.Item.ButtonsPanel.PinButton.ToolTipText = "تثبيت الصفحة";

                }
                else
                {
                    UCLandsCards UC = PageViewCardsHome.Pages["LandsCard"].Controls[0] as UCLandsCards;
                    UC.Bs.Position = land.number - 1;
                    PageViewCardsHome.SelectedPage = enumerableIterator;

                }
                this.TopMost = true;
                RadOverlayManager.Close();
                LandsListView.SelectedItems.Clear();
                this.TopMost = false;

            }
            else if (e.ClickedItem.Name == "MenuShowCalc")
            {
                vwSelectLand land = vwSelectLand.FindBy("Number", LandsListView.SelectedItem.Text);
                FrmCalc frm = new FrmCalc(land);
                frm.Show();
                LandsListView.SelectedItems.Clear();

            }
            else if (e.ClickedItem.Name == "MenuSendLandToSell")
            {
                List<tbLand> lst = new List<tbLand>();

                foreach (var item in LandsListView.SelectedItems)
                {
                    tbLand land = DataLandList.Where(u => u.guid == (Guid)item.Value).FirstOrDefault();
                    lst.Add(land);

                }
                if (lst.Count == 0)
                {
                    if (CurrentLand != null)
                    {
                        lst.Add(CurrentLand);

                    }
                    else
                    {

                        foreach (var BlockLand in CurrentBlock.Items)
                        {
                            tbLand land = DataLandList.Where(u => u.guid == (Guid)BlockLand.Value).FirstOrDefault();
                            if (land != null)
                            {
                                if (land.status == "متاح") { lst.Add(land); }

                            }

                        }
                    }
                }



                FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 0, lst);
                frm.Owner = this;
                frm.Show();
                LandsListView.SelectedItems.Clear();

            }
            else if (e.ClickedItem.Name == "MenuExtraContract")
            {
                List<tbLand> lst = new List<tbLand>();

                foreach (var item in LandsListView.SelectedItems)
                {
                    tbLand land = DataLandList.Where(u => u.guid == (Guid)item.Value).FirstOrDefault();
                    lst.Add(land);

                }
                if (lst.Count == 0)
                {
                    lst.Add(CurrentLand);
                }

                FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 2, lst);
                frm.Owner = this;
                frm.Show();
                LandsListView.SelectedItems.Clear();

            }

        }

        private void OverviewLandsView_ItemMouseClick(object sender, ListViewItemEventArgs e)
        {

            if (e.Item is ListViewDataItemGroup)
            {
                CurrentBlock = e.Item as ListViewDataItemGroup;
                if (CurrentBlock != null)
                {
                    MenuStripOverviewLandsView.Items["MenuSendLandToSell"].Text = "بيع الأراضي المتاح بالكامل";
                    MenuStripOverviewLandsView.Items["MenuExtraContract"].Visible = false;
                    MenuStripOverviewLandsView.Items["MenuShowLandCard"].Visible = false;
                    MenuStripOverviewLandsView.Items["MenuShowCalc"].Visible = false;

                }
            }
            else
            {
                CurrentLand = e.Item.DataBoundItem as tbLand;
                if (CurrentLand != null)
                {
                    if (CurrentLand.status == "متاح")
                    {

                        MenuStripOverviewLandsView.Items["MenuExtraContract"].Visible = false;
                        MenuStripOverviewLandsView.Items["MenuShowLandCard"].Visible = true;
                        MenuStripOverviewLandsView.Items["MenuShowCalc"].Visible = true;
                        MenuStripOverviewLandsView.Items["MenuSendLandToSell"].Visible = true;

                        if (LandsListView.SelectedItems.Count > 1)
                        {
                            MenuStripOverviewLandsView.Items["MenuSendLandToSell"].Text = "بيع الأراضي المحددة";
                            MenuStripOverviewLandsView.Items["MenuExtraContract"].Visible = false;
                            MenuStripOverviewLandsView.Items["MenuShowLandCard"].Visible = false;
                            MenuStripOverviewLandsView.Items["MenuShowCalc"].Visible = false;

                        }
                        else
                        {
                            MenuStripOverviewLandsView.Items["MenuSendLandToSell"].Text = "بيع الأرض";
                        }

                    }
                    else if (CurrentLand.status == "مباع")
                    {
                        MenuStripOverviewLandsView.Items["MenuSendLandToSell"].Visible = false;
                        MenuStripOverviewLandsView.Items["MenuExtraContract"].Visible = true;
                        MenuStripOverviewLandsView.Items["MenuShowLandCard"].Visible = true;
                        MenuStripOverviewLandsView.Items["MenuShowCalc"].Visible = true;

                    }
                    else
                    {
                        MenuStripOverviewLandsView.Items["MenuSendLandToSell"].Visible = false;
                        MenuStripOverviewLandsView.Items["MenuExtraContract"].Visible = false;
                        MenuStripOverviewLandsView.Items["MenuShowLandCard"].Visible = true;
                        MenuStripOverviewLandsView.Items["MenuShowCalc"].Visible = true;

                    }


                }

                LandsListView.SelectedItem = e.Item;


            }
        }

        #endregion

        #region Main Menu

        private void radMenuItem8_Click(object sender, EventArgs e)
        {
            FrmOfficeRptNetVatFeePayRemainNoDis frm = new FrmOfficeRptNetVatFeePayRemainNoDis("صافي العمولة بدون حسم و الضريبة");
            frm.Owner = this;
            frm.Show();

        }

        private void MenuLogOut_Click(object sender, EventArgs e)
        {
            AppSetting.DataBase = DBConnect.DBConnection.Database;
            FrmLogin frmlog = new FrmLogin();
            if (frmlog.ShowDialog(this) == DialogResult.OK)
            {
                CurrentUser = frmlog.user;
                LoadSettings();
            }
            else
            {
                Application.Exit();
            }
        }

        private void MenuBackupToFile_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TmrStatic.Enabled = false;
            FrmBackupRestore frmbak = new FrmBackupRestore(FrmBackupRestore.BackupRestore.backup, AppSetting.DataBase);
            frmbak.ShowDialog();
            TmrStatic.Enabled = true;
        }

        private void MenuRestore_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TmrStatic.Enabled = false;
            FrmBackupRestore frmbak = new FrmBackupRestore(FrmBackupRestore.BackupRestore.restore, AppSetting.DataBase);
            frmbak.ShowDialog();
            TmrStatic.Enabled = true;

        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuAppInfoSettings_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAppInfo frmAppInfo = new FrmAppInfo();
            frmAppInfo.ShowDialog();
            LoadSettings();
        }

        private void MenuFrmTable_Click(object sender, EventArgs e)
        {
            FrmTable frmtable = new FrmTable(Guid.Empty);
            frmtable.Owner = this;
            frmtable.Show();
        }

        private void MenuSearch_Click(object sender, EventArgs e)
        {
            FrmReport frmreport = new FrmReport("تقرير");
            frmreport.Owner = this;
            frmreport.Show();
        }

        private void MenuEmailSettings_Click(object sender, EventArgs e)
        {
            FrmEmailSettings frmemail = new FrmEmailSettings();
            frmemail.ShowDialog();
        }

        private void MenuUsersManagement_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmUsers frmuser = new FrmUsers(Guid.Empty);
            frmuser.ShowDialog();
        }

        private void MenuOpenFile_Click(object sender, EventArgs e)
        {
            ChangeDataBase(true);
        }

        private void MenuSendEmail_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FastReport.Report rpt = new FastReport.Report();
            Reports.InitReport(rpt, "hello.frx", false);

            rpt.Prepare();
            FastReport.Export.Pdf.PDFExport pdfex = new FastReport.Export.Pdf.PDFExport();
            pdfex.Author = "";
            pdfex.Creator = "";
            pdfex.EmbeddingFonts = true;
            pdfex.HasMultipleFiles = false;
            pdfex.Producer = string.Empty;

            DynamicAttachement dynamicattach = new DynamicAttachement();
            dynamicattach.attachFileName = "attach.pdf";
            dynamicattach.attachData = new MemoryStream();

            pdfex.Export(rpt, dynamicattach.attachData);

            tbEmailSettings.Fill();
            tbEmailSettings emailsettings = tbEmailSettings.lstData[0];

            FrmSendMail frmsendemail = new FrmSendMail(emailsettings, dynamicattach);
            frmsendemail.ShowDialog();
        }

        private void MenuArchiveCard_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            MenuBackupToFile.PerformClick();
        }

        private void MenuArchiveOut_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void MenuArchiveOutRpt_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void MenuConnectionSettings_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmConnection frmcon = new FrmConnection();
            frmcon.ShowDialog();
        }

        private void MenuSendSMS_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            tbSMSsettings.Fill();
            FrmSendFreeSMS frmsms = new FrmSendFreeSMS(tbSMSsettings.lstData[0].Mobile, tbSMSsettings.lstData[0].MessageBody);
            frmsms.ShowDialog();
        }

        private void MenuTreeDoc_Click(object sender, EventArgs e)
        {
             RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmActivate frmact = new FrmActivate();
            if (frmact.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void MenuChangeDatabase_Click(object sender, EventArgs e)
        {
            TmrStatic.Enabled = false;
            if (ChangeDataBase(false))
            {

            }
        }

        private void MenuMobileSettings_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmSMSSettings frmsms = new FrmSMSSettings(Guid.Empty);
            frmsms.ShowDialog();
        }

        private void MenuAgentBuyCard_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAgent agent = new FrmAgent(Guid.Empty, true, 1, false);
            agent.Owner = this;
            agent.Show();
        }

        private void MenuLandTree_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandTree frm = new FrmLandTree("شجرة الأصناف");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuLandCard_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            RadOverlayManager.Show(this);

            RadPageViewPage enumerableIterator = PageViewCardsHome.Pages.Where(u => u.Name == "LandsCard").FirstOrDefault();
            if (enumerableIterator == null)
            {
                RadPageViewPage radPageViewPage = new RadPageViewPage();
                radPageViewPage.Name = "LandsCard";
                radPageViewPage.Text = "بطاقات الأراضي";

                DoctorERP.User_Controls.UCLandsCards uCLands = new User_Controls.UCLandsCards(Guid.Empty, false, string.Empty);
                uCLands.Dock = DockStyle.Fill;

                radPageViewPage.Controls.Add(uCLands);
                PageViewCardsHome.Pages.Add(radPageViewPage);
                PageViewCardsHome.SelectedPage = radPageViewPage;
            }
            else
            {
                UCLandsCards UC = PageViewCardsHome.Pages["LandsCard"].Controls[0] as UCLandsCards;
                PageViewCardsHome.SelectedPage = enumerableIterator;

            }
            this.TopMost = true;

            RadOverlayManager.Close();
            this.TopMost = false;

        }

        private void MenuPlanInfo_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmPlanInformation frmplan = new FrmPlanInformation(Guid.Empty);
            frmplan.Owner = this;
            frmplan.Show();
        }

        private void MenuPayIn_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmPay frm = new FrmPay(Guid.Empty, 0, true);
            frm.Owner = this;
            frm.Show();
        }

        private void MenuPayOut_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmPay frm = new FrmPay(Guid.Empty, 1, true);
            frm.Owner = this;
            frm.Show();
        }

        private void MenuSellBill_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 0, new List<tbLand>());
            frm.Show(this);
        }

        private void MenuImportBuyer_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmBuyerImport frm = new FrmBuyerImport();
            frm.ShowDialog();
        }

        private void MenuImportLand_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandImport frm = new FrmLandImport();
            frm.ShowDialog();
        }

        private void MenuReturnBill_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 1, new List<tbLand>());
            frm.Show(this);
        }

        private void MenuDelayBill_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmBillHeader frm = new FrmBillHeader(Guid.Empty, true, 2, new List<tbLand>());
            frm.Show(this);
        }
        private void MenuAgentStatement_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAgentStatment frmagent = new FrmAgentStatment("كشف حساب عميل");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuOwnerCard_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAgent agent = new FrmAgent(Guid.Empty, true, 0, false);
            agent.Owner = this;
            agent.Show();
        }

        private void MenuCalc_Click(object sender, EventArgs e)
        {
            FrmCalc frm = new FrmCalc(new vwSelectLand());
            frm.Show(this);
        }

        private void MenuTaxandDiscountSettings_Click(object sender, EventArgs e)
        {
            FrmTaxAndDiscount frm = new FrmTaxAndDiscount(Guid.Empty);
            frm.ShowDialog();
        }

        private void MenuWorkFeeeRpt_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmWorkFeeRpt frm = new FrmWorkFeeRpt("عمولة السعي");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuPayContract_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmPayContractOne frm = new FrmPayContractOne(Guid.Empty, true, 0);
            frm.Show(this);
        }

        private void MenuAgentSearch_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAgentReport frmagent = new FrmAgentReport("بحث عن عميل");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuLandRpt_Click_1(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandReport frmagent = new FrmLandReport("بحث عن صنف");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuDailySales_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmDailySellRpt frmagent = new FrmDailySellRpt("مبيعات يومية");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuDialySalesGrouped_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmDailySellGrouped frmagent = new FrmDailySellGrouped("مبيعات يومية تجميعي");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuLandQty_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandSalesRpt frm = new FrmLandSalesRpt("جرد المبيعات");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuLandQtyRpt_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandQtyRpt frm = new FrmLandQtyRpt("جرد حسب الكمية");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuLandQtyAllRpt_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandQtyRpt frm = new FrmLandQtyRpt("جرد جميع الأصناف");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuBuidlingFeeRpt_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmBuildingFeeRpt frmagent = new FrmBuildingFeeRpt("ضريبة التصرفات العقارية");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuLandQtyInStore_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandQtyInStore frm = new FrmLandQtyInStore("الأًصناف المتبقية");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuAccountCard_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAccount frm = new FrmAccount(Guid.Empty, true, false);
            frm.Owner = this;
            frm.Show();
        }

        private void MenuPayContractDelay_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmPayContractOne frm = new FrmPayContractOne(Guid.Empty, true, 1);
            frm.Show(this);
        }

        private void MenuWorkFeeVATRpt_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmWorkFeeVATRpt frm = new FrmWorkFeeVATRpt("ضريبة عمولة السعي");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuLandTransRpt_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmLandTrans frm = new FrmLandTrans(Guid.Empty, true);
            frm.Show(this);
        }

        private void MeuAccBalance_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAccBalance frm = new FrmAccBalance("أرصدة الحسابات");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuAccStatementPay_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAccountStatment frm = new FrmAccountStatment("كشف حساب");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuGeneralAccountStatic_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmGeneralAccount frmac = new FrmGeneralAccount();
            frmac.Show(this);
        }

        private void MenuDailyReutrnSalesGrouped_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmDailyReturnSellGrouped frmagent = new FrmDailyReturnSellGrouped("مرتجع مبيعات يومية تجميعي");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuDailyReturnSellRpt_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmDailyReturnSellRpt frmagent = new FrmDailyReturnSellRpt("مرتجع مبيعات يومية");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuVatSettings_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmVATSettings frm = new FrmVATSettings(Guid.Empty);
            frm.ShowDialog();
            tbVATSettings.Fill();
        }

        private void BtnMatCard_Click(object sender, EventArgs e)
        {
            MenuLandCard.PerformClick();
        }

        private void BtnDelyBill_Click(object sender, EventArgs e)
        {
            MenuDelayBill.PerformClick();
        }

        private void MenuArea_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            tbAttachment attach = tbAttachment.FindByFull("ParentGuid", tbPlanInfo.lstData[0].guid, "area");
            if (attach.Guid.Equals(Guid.Empty))
            {
                MessageBox.Show("لم يتم إضافة ملف مرفق في معلومات المخطط", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            FileHelper.RunFile(attach.FileName, attach.FileData);
        }

        private void MenuBankManager_Click(object sender, EventArgs e)
        {
            FrmListManager frm = new FrmListManager();
            frm.ShowDialog();
            tbBanks.Fill();
        }

        private void MenuPaymentsNotes_Click(object sender, EventArgs e)
        {
            FrmPaymentNotes frm = new FrmPaymentNotes(Guid.Empty);
            frm.ShowDialog();
            tbPaymentsNotes.Fill();
        }

        private void MenuAgetnStatementDetails_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAgentStatementDetails frmagent = new FrmAgentStatementDetails("كشف حساب عميل");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuGeneralSalesReport_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmGeneralSalesReport frmagent = new FrmGeneralSalesReport("تقرير المبيعات العام");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuSetPasswordFromEdit_Click(object sender, EventArgs e)
        {
            FrmEditPassWord frm = new FrmEditPassWord(Guid.Empty, false);
            frm.ShowDialog();
            tbEditPassword.Fill();
        }

        private void MenuAddAmounttoAgentBalance_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmAddAmountoAgent frmagent = new FrmAddAmountoAgent("توزيع مبلغ على عقود عميل");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuSaleOrder_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmSaleOrder frm = new FrmSaleOrder(Guid.Empty, true);
            frm.Show(this);
        }

        private void MenuSalesOrderrpt_Click(object sender, EventArgs e)
        {
                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmSaleOrderRpt frmagent = new FrmSaleOrderRpt("أوامر البيع");
            frmagent.Owner = this;
            frmagent.Show();
        }

        private void MenuPayRpt_Click(object sender, EventArgs e)
        {
            FrmPayRpt frm = new FrmPayRpt("حركة السندات");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuContractRpt_Click(object sender, EventArgs e)
        {
            FrmContractRpt frm = new FrmContractRpt("حركة العقود");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuBuildingFeeNumbersRpt_Click(object sender, EventArgs e)
        {
            FrmBuildingFeeNumberRpt frm = new FrmBuildingFeeNumberRpt("أرقام ضريبة التصرفات العقارية");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuCash_Click(object sender, EventArgs e)
        {
            FrmCashRpt frm = new FrmCashRpt("الصندوق");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuLandNetPrice_Click(object sender, EventArgs e)
        {
            FrmOwnerLandNetPrice frm = new FrmOwnerLandNetPrice("صافي قيمة الأرض الدفترية");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuPriceFeeDiscount_Click(object sender, EventArgs e)
        {
            FrmOfficeRptPriceFeeDiscount frm = new FrmOfficeRptPriceFeeDiscount("قيمة دفترية و عمولات و خصم");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuNetPriceVatRpt_Click(object sender, EventArgs e)
        {
            FrmOfficeRptNetPriceVAT frm = new FrmOfficeRptNetPriceVAT("صافي القيمة و العمولة و الضريبة");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuOwnerNetPriceAndRemain_Click(object sender, EventArgs e)
        {
            FrmOwnerLandNetPriceandPayments frm = new FrmOwnerLandNetPriceandPayments("صافي القيمة الدفترية و الواصل و المتبقي");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuOfficeNetPrice_Click(object sender, EventArgs e)
        {
            FrmOwnerLandNetPriceandPayments frm = new FrmOwnerLandNetPriceandPayments("صافي القيمة الدفترية");
            frm.Owner = this;
            frm.Show();
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmOfficeRptFees frm = new FrmOfficeRptFees("تقرير عمولة السعي");
            frm.Owner = this;
            frm.Show();
        }

        private void MenuOfficeRptFeeVatPayRemains_Click(object sender, EventArgs e)
        {
            FrmOfficeRptNetVatFeePayRemain frm = new FrmOfficeRptNetVatFeePayRemain("صافي العمولة و الضريبة");
            frm.Owner = this;
            frm.Show();
        }

        private void صافيالعمولةبدونالحسموالضريبةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOfficeRptNetVatFeePayRemainNoDis frm = new FrmOfficeRptNetVatFeePayRemainNoDis("صافي العمولة بدون حسم و الضريبة");
            frm.Owner = this;
            frm.Show();
        }


        private void MenuAllowEdit_Click(object sender, EventArgs e)
        {
            RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (AllowEdit)
            {
                FrmEditPassWord frm = new FrmEditPassWord(Guid.Empty, true);
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
            }

        }

        private void MenuFreeze_Click(object sender, EventArgs e)
        {

            AppSetting.DataBase = DBConnect.DBConnection.Database;
            FrmLogin frmlog = new FrmLogin();
            if (frmlog.ShowDialog() == DialogResult.OK)
            {
                CurrentUser = frmlog.user;
                LoadSettings();

                return;
            }
            else
                return;
        }

        private void MenuPayContractOut_Click(object sender, EventArgs e)
        {

                       RadMenuItem toolmenu = (RadMenuItem)sender;
            if (!IsPermissionGranted(toolmenu.Text))
            {
                MessageBox.Show("لا تملك صلاحية للقيام بهذا العمل", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmPayContractOne frm = new FrmPayContractOne(Guid.Empty, true, 2);
            frm.Show(this);
        }

        private void MenuLogRpt_Click(object sender, EventArgs e)
        {
            FrmLogRpt frm = new FrmLogRpt("سجل المستخدمين");
            frm.Owner = this;
            frm.Show();
        }

        #endregion

        private void radMenuButtonItem1_Click(object sender, EventArgs e)
        {
            RadMessageBox.Show("يرجى إعادة تشغيل البرنامج ليتم تطبيق إعدادات الإتصال الجديدة", Application.ProductName, MessageBoxButtons.OK);
            RadMessageBoxForm form = new RadMessageBoxForm();
            form.ShowDialog();
            form.ShowDetails();

        }

    }
}
