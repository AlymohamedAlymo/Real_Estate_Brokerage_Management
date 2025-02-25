﻿using System.Drawing;
using Telerik.WinControls;

namespace Real_Estate_Management.CustomElements
{
    /// <summary>
    /// Behavior class defining the OnMouseOver behavior of the BubbleBar
    /// </summary>
	public class BubbleBarMouseOverBehavior : PropertyChangeBehavior
    {
        public BubbleBarMouseOverBehavior()
            : base(RadItem.IsMouseOverProperty)
        {


        }


        public override void OnPropertyChange(RadElement element, RadPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                element.ResetValue(RadElement.ScaleTransformProperty);

                AnimatedPropertySetting animatedExpand = new AnimatedPropertySetting(
                    RadElement.ScaleTransformProperty,
                    new SizeF(0.96f, 0.96f),
                    new SizeF(1.1f, 1.1f),
                    5, 30);

                animatedExpand.ApplyValue(element);
            }
            else
            {
                AnimatedPropertySetting animatedExpand = new AnimatedPropertySetting(
                    RadElement.ScaleTransformProperty,
                    null, new SizeF(0.96f, 0.96f), 5, 30);

                animatedExpand.ApplyValue(element);
            }
        }
    }
}
