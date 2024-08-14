using System;
using System.Drawing;
using Telerik.WinControls.UI;

namespace DoctorERP
{
    public class LandIconListViewVisualItem : IconListViewVisualItem
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(IconListViewVisualItem);
            }
        }

        private LightVisualElement LandID = new LightVisualElement();
        private StackLayoutElement VerticalContainer = new StackLayoutElement();
        private StackLayoutElement LandHeaderContainer = new StackLayoutElement();


        //private LightVisualElement roomId = new LightVisualElement();
        //private LightVisualElement roomStatus = new LightVisualElement();
        //private LightVisualElement bookingInfo = new LightVisualElement();
        //private LightVisualElement bookingDuration = new LightVisualElement();
        //private LightVisualElement houseKeepingInfo = new LightVisualElement();
        //private LightVisualElement needsRepair = new LightVisualElement();
        //private StackLayoutElement verticalContainer = new StackLayoutElement();
        //private StackLayoutElement roomHeaderContainer = new StackLayoutElement();
        //private StackLayoutElement roomFooterContainer = new StackLayoutElement();



        protected override void CreateChildElements()
        {
            base.CreateChildElements();

            VerticalContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            VerticalContainer.NotifyParentOnMouseInput = true;
            VerticalContainer.ShouldHandleMouseInput = false;
            VerticalContainer.StretchHorizontally = true;
            VerticalContainer.StretchVertically = true;

            LandHeaderContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            LandHeaderContainer.NotifyParentOnMouseInput = true;
            LandHeaderContainer.ShouldHandleMouseInput = false;
            LandHeaderContainer.Children.Add(LandID);
            LandHeaderContainer.StretchHorizontally = true;

            LandID.NotifyParentOnMouseInput = true;
            LandID.ShouldHandleMouseInput = false;
            LandID.StretchHorizontally = true;
            LandID.Font = new Font("Traditional Arabic", 15, FontStyle.Bold);
            //LandID.CustomFont = "Traditional Arabic";
            //LandID.CustomFontSize = 20;
            //LandID.CustomFontStyle = FontStyle.Bold;
            LandID.Padding = new System.Windows.Forms.Padding(0, 4, -3, 0);
            LandID.Margin = new System.Windows.Forms.Padding(0, 4, -2, 0);


            //TotalInfo.StretchVertically = true;

            //TotalInfo.NotifyParentOnMouseInput = true;
            //TotalInfo.ShouldHandleMouseInput = false;
            //TotalInfo.StretchHorizontally = false;
            //TotalInfo.Alignment = ContentAlignment.MiddleLeft;
            //TotalInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            //TotalInfo.CustomFont = "Traditional Arabic";
            //TotalInfo.CustomFontSize = 10;
            //TotalInfo.CustomFontStyle = FontStyle.Bold;

            VerticalContainer.Children.Add(LandHeaderContainer);
            //VerticalContainer.Children.Add(TotalInfo);

            this.Children.Add(this.VerticalContainer);



        }

        protected override void OnSelect()
        {
            base.OnSelect();
        }

        protected override void DoClick(EventArgs e)
        {
            base.DoClick(e);

            tbLand Land = this.Data.DataBoundItem as tbLand;

            if (Land.status.ToString().Contains("مباع"))
            {
                this.BackColor = Color.DarkRed;
                LandID.ForeColor = Color.White;

            }
            else if (Land.status.ToString().Contains("متاح"))
            {
                this.BackColor = Color.Green;
                LandID.ForeColor = Color.White;

            }
            else if (Land.status.ToString().Contains("محجوز"))
            {
                this.BackColor = Color.Gold;
                LandID.ForeColor = Color.Black;

            }

        }

        protected override void SynchronizeProperties()
        {

            base.SynchronizeProperties();

            this.DrawText = false;
            this.BackColor = Color.White;
            this.DrawFill = true;
            this.DrawBorder = false;
            this.RightToLeft = true;
            LandID.TextAlignment = ContentAlignment.MiddleCenter;
            //TotalInfo.ForeColor = Color.White;
            tbLand Land = this.Data.DataBoundItem as tbLand;
            RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();

            if (Land != null)
            {

                LandID.Text = Land.number.ToString();
                screenTip.RightToLeft = true;
                screenTip.FooterVisible = true;

                screenTip.CaptionLabel.Text = "أرض رقم:  " + Land.number.ToString();
                screenTip.CaptionLabel.Font = new Font("Traditional Arabic", 15, FontStyle.Bold);
                screenTip.CaptionLabel.TextAlignment = ContentAlignment.MiddleCenter;


                 decimal WorkFeeWithVat = (Land.amount * Land.workfee / 100) + ((Land.amount * Land.workfee / 100) * Land.vat / 100);

                decimal WorkFeeValue = (Land.amount * Land.buildingfee / 100);


                string Header = "القيمة الدفترية : " + $"{Land.amount:n}  ر.س";
                string Content = "ضريبة التصرفات : " + $"{WorkFeeValue:n}  ر.س" ;
                string Footer = "السعي + الضريبة : " + $"{WorkFeeWithVat:n}  ر.س";

                screenTip.MainTextLabel.Text = Header + "\n" +
                 Content + "\n" +
                Footer;

                screenTip.MainTextLabel.Font = new Font("Traditional Arabic", 16, FontStyle.Bold);
               // screenTip.MainTextLabel.ForeColor = Color.DarkBlue;
                screenTip.MainTextLabel.TextAlignment = ContentAlignment.MiddleCenter;
                screenTip.FooterTextLabel.Text = "الإجمالي : " + $"{Land.total:n}  ر.س" + "\n"+
                    "المساحة : " + Land.area.ToString("0.00") + " م2";

                screenTip.FooterTextLabel.Font = new Font("Traditional Arabic", 15, FontStyle.Bold);


                //LightVisualElement LandID2 = new LightVisualElement();
                //LandID2.NotifyParentOnMouseInput = true;
                //LandID2.ShouldHandleMouseInput = false;
                //LandID2.StretchHorizontally = true;
                //LandID2.CustomFont = "Traditional Arabic";
                //LandID2.CustomFontSize = 20;
                //LandID2.CustomFontStyle = FontStyle.Bold;
                //LandID2.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
                //LandID2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                //LandID2.Text = "asdasdasd a";


                //StackLayoutElement LandHeaderContainer2 = new StackLayoutElement();

                //LandHeaderContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
                //LandHeaderContainer2.NotifyParentOnMouseInput = true;
                //LandHeaderContainer2.ShouldHandleMouseInput = false;
                //LandHeaderContainer2.Children.Add(LandID2);
                //LandHeaderContainer2.StretchHorizontally = true;


                //StackLayoutElement VerticalContainer2 = new StackLayoutElement();
                //VerticalContainer2.Orientation = System.Windows.Forms.Orientation.Vertical;
                //VerticalContainer2.NotifyParentOnMouseInput = true;
                //VerticalContainer2.ShouldHandleMouseInput = false;
                //VerticalContainer2.StretchHorizontally = true;
                //VerticalContainer2.StretchVertically = true;

                //VerticalContainer2.Size = new Size(200, 200);


                //VerticalContainer2.Children.Add(LandHeaderContainer2);

                //screenTip.Children.Add(VerticalContainer2);

            }
            RadListViewElement list = this.Parent.Parent.Parent as RadListViewElement;

            
            if (Land.status.ToString().Contains("مباع"))
            {
                if (list.SelectedItems.Contains(this.dataItem))
                {
                    this.BackColor = Color.DarkRed;
                    LandID.ForeColor = Color.White;

                }
                else
                {
                    this.BackColor = Color.FromArgb(254, 0, 0);
                    LandID.ForeColor = Color.Black;

                }

            }
            else if (Land.status.ToString().Contains("متاح"))
            {
                if (list.SelectedItems.Contains(this.dataItem))
                {
                    this.BackColor = Color.Green;
                    LandID.ForeColor = Color.White;

                }
                else
                {
                    this.BackColor = Color.FromArgb(0, 255, 1);
                    LandID.ForeColor = Color.Black;

                }

            }
            else if (Land.status.ToString().Contains("محجوز"))
            {
                if (list.SelectedItems.Contains(this.dataItem))
                {
                    this.BackColor = Color.Gold;

                }
                else
                {
                    this.BackColor = Color.FromArgb(255, 255, 0);
                }
                LandID.ForeColor = Color.Black;

            }
            this.ScreenTip = screenTip;
            this.ScreenTip.ShouldApplyTheme = true;
        }

    }
}
