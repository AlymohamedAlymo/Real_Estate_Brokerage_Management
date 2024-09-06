using System;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace DoctorERP.CustomElements
{
    public class BubbleBarElement : RadElement
    {
        FillPrimitive fill;
        BorderPrimitive border;
        StackLayoutPanel panel;
        RadItemOwnerCollection items;

        public BubbleBarElement()
        {
        }

        protected override void InitializeFields()
        {
            base.InitializeFields();

            this.Shape = new RoundRectShape(12);

            this.items = new RadItemOwnerCollection();
            this.items.ItemTypes = new Type[] { typeof(RadButtonElement) };
            this.items.ItemsChanged += new ItemChangedDelegate(Items_ItemsChanged);
        }

        public RadItemOwnerCollection Items
        {
            get
            {
                return items;
            }
        }

        protected override void CreateChildElements()
        {
            fill = new FillPrimitive
            {
                BackColor = Color.Transparent,
                BackColor2 = Color.Transparent,

                NumberOfColors = 2,
                GradientStyle = GradientStyles.Linear,
                GradientAngle = 90,
                AutoSizeMode = RadAutoSizeMode.WrapAroundChildren
            };
            this.Children.Add(fill);

            border = new BorderPrimitive
            {
                GradientStyle = GradientStyles.Solid,
                ForeColor = Color.FromArgb(0, 0, 0),
                AutoSizeMode = RadAutoSizeMode.WrapAroundChildren
            };

            this.Children.Add(border);

            panel = new StackLayoutPanel
            {
                Orientation = System.Windows.Forms.Orientation.Horizontal,
                Alignment = ContentAlignment.MiddleCenter,
                StretchHorizontally = false
            };
            this.Children.Add(panel);

            this.items.Owner = panel;
        }


        void Items_ItemsChanged(RadItemCollection changed, RadItem target, ItemsChangeOperation operation)
        {
            if (operation == ItemsChangeOperation.Inserted || operation == ItemsChangeOperation.Set)
            {
                target.AddBehavior(new BubbleBarMouseOverBehavior());
                target.BackColor = Color.Transparent;

            }
        }
    }
}
