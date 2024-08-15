using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.Windows;
using System.Drawing;

namespace DoctorERP.CustomElements
{
    public class ScreenTipCustomize : RadScreenTipElement
    {
        private  LightVisualElement Header_Element = new LightVisualElement();
        private  LightVisualElement Content_Element = new LightVisualElement();
        private  LightVisualElement Footer_Element = new LightVisualElement();
        private readonly RadLineItem LineItem = new RadLineItem();
        private readonly RadLineItem LineItem2 = new RadLineItem();

        private readonly StackLayoutElement VerticalContainer = new StackLayoutElement();
        private readonly StackLayoutElement HorizontalContainer = new StackLayoutElement();


        public LightVisualElement HeaderElement
        {
            get
            {
                return Header_Element;
            }
        }
        public LightVisualElement ContentElement
        {
            get
            {
                return Content_Element;
            }
        }

        public LightVisualElement FooterElement
        {
            get
            {
                return Footer_Element;
            }
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();

            VerticalContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            VerticalContainer.NotifyParentOnMouseInput = true;
            VerticalContainer.ShouldHandleMouseInput = false;
            VerticalContainer.StretchHorizontally = true;
            VerticalContainer.StretchVertically = true;
            ////LandHeaderContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            ////LandHeaderContainer.NotifyParentOnMouseInput = true;
            ////LandHeaderContainer.ShouldHandleMouseInput = false;

            HorizontalContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            HorizontalContainer.NotifyParentOnMouseInput = true;
            HorizontalContainer.ShouldHandleMouseInput = false;
            HorizontalContainer.StretchHorizontally = true;
            HorizontalContainer.StretchVertically = true;

            //LandHeaderContainer.StretchHorizontally = true;
            Header_Element.DrawFill = true;
            Header_Element.DrawText = true;
            Header_Element.GradientStyle = GradientStyles.Solid;
            Header_Element.TextImageRelation = TextImageRelation.ImageBeforeText;
            Header_Element.NotifyParentOnMouseInput = true;
            Header_Element.ShouldHandleMouseInput = false;
            Header_Element.StretchHorizontally = true;
            Header_Element.StretchVertically = false;
            Header_Element.BackColor = Color.LightGray;
            Header_Element.RightToLeft = true;
            Header_Element.Padding = new Padding(0, 2, 0, 0);
            Header_Element.Margin = new Padding(0, 2, 0, 0);
            //Header_Element.AutoSize = false;

            Content_Element.DrawFill = true;
            Content_Element.DrawText = true;
            Content_Element.GradientStyle = GradientStyles.Solid;
            Content_Element.TextImageRelation = TextImageRelation.ImageBeforeText;
            Content_Element.NotifyParentOnMouseInput = true;
            Content_Element.ShouldHandleMouseInput = false;
            Content_Element.StretchHorizontally = true;
            Content_Element.StretchVertically = true;
            Content_Element.BackColor = Color.White;
            Content_Element.RightToLeft = true;
            //Content_Element.AutoSize = true;


            Footer_Element.DrawFill = true;
            Footer_Element.DrawText = true;
            Footer_Element.GradientStyle = GradientStyles.Solid;
            Footer_Element.TextImageRelation = TextImageRelation.ImageBeforeText;
            Footer_Element.NotifyParentOnMouseInput = true;
            Footer_Element.ShouldHandleMouseInput = false;
            Footer_Element.StretchHorizontally = true;
            Content_Element.StretchVertically = false;
            Footer_Element.RightToLeft = true;
            Footer_Element.AutoSize = true;

            VerticalContainer.Children.AddRange(Header_Element, LineItem, Content_Element);

            //VerticalContainer.Children.Add(LandHeaderContainer);
            this.Children.Add(this.VerticalContainer);
            this.AutoSize = true;
            this.StretchHorizontally = true;
            this.StretchVertically = true;

            this.BackColor = Color.White;
        }

    }
}
