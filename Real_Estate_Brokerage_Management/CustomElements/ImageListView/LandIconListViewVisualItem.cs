using System;
using System.Drawing;
using Telerik.WinControls.UI;

namespace DoctorERP.CustomElements
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

        private readonly LightVisualElement LandID = new LightVisualElement();
        private readonly StackLayoutElement VerticalContainer = new StackLayoutElement();
        private readonly StackLayoutElement LandHeaderContainer = new StackLayoutElement();

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
            VerticalContainer.Children.Add(LandHeaderContainer);
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
            LandID.Font = new Font("Traditional Arabic", 15, FontStyle.Bold);
            tbLand Land = this.Data.DataBoundItem as tbLand;

            ScreenTipCustomize screenTipCustomize = new ScreenTipCustomize();

            if (Land != null)
            {
                if (Land.number.ToString().Length == 1)
                {
                    LandID.Padding = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length - 1), 0);
                    LandID.Margin = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length - 1), 0);

                }
                else if (Land.number.ToString().Length == 2)
                {
                    LandID.Padding = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length), 0);
                    LandID.Margin = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length), 0);

                }
                else if (Land.number.ToString().Length == 3)
                {
                    LandID.Padding = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length + 1), 0);
                    LandID.Margin = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length + 1), 0);

                }
                else
                {
                    LandID.Padding = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length + 2), 0);
                    LandID.Margin = new System.Windows.Forms.Padding(0, 4, -(Land.number.ToString().Length + 2), 0);

                }

                LandID.Text = Land.number.ToString();

                screenTipCustomize.HeaderElement.Text = "أرض رقم:  " + Land.number.ToString();
                screenTipCustomize.HeaderElement.Font = new Font("Traditional Arabic", 15, FontStyle.Bold);
                screenTipCustomize.HeaderElement.TextAlignment = ContentAlignment.MiddleCenter;

                decimal WorkFeeWithVat = (Land.amount * Land.workfee / 100) + ((Land.amount * Land.workfee / 100) * Land.vat / 100);
                decimal WorkFeeValue = (Land.amount * Land.buildingfee / 100);
                string Header = "القيمة الدفترية :     " + $"{Land.amount:n}  ر.س";
                string Content = "ضريبة التصرفات :  " + $"{WorkFeeValue:n}  ر.س";
                string Footer = "السعي + الضريبة : " + $"{WorkFeeWithVat:n}  ر.س";
                screenTipCustomize.ContentElement.Text = Header + "\n" +
                 Content + "\n" +
                Footer + "\n" +
                "الإجمالي الشامل  :  " + $"{Land.total:n}  ر.س" + "\n" +
                    "المساحة : " + Land.area.ToString("0.00") + " م2";
                screenTipCustomize.ContentElement.Font = new Font("Traditional Arabic", 18, FontStyle.Bold);
                screenTipCustomize.ContentElement.TextAlignment = ContentAlignment.MiddleLeft;
            }
            this.ScreenTip = screenTipCustomize;

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
        }

    }
}
