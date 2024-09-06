using System.ComponentModel;
using Telerik.WinControls;

namespace DoctorERP.CustomElements
{
    public class BubbleBar : RadControl
    {
        BubbleBarElement element;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BubbleBarElement Element
        {
            get
            {
                return this.element;
            }
        }

        [RadEditItemsAction]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RadItemOwnerCollection Items
        {
            get
            {
                return this.element.Items;
            }
        }

        protected override void CreateChildItems(RadElement parent)
        {
            this.element = new BubbleBarElement
            {
                AutoSizeMode = RadAutoSizeMode.WrapAroundChildren
            };
            parent.Children.Add(this.element);
        }
    }
}
