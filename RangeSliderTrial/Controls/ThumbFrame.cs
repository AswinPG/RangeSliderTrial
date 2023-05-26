using Android.Content;
using Android.Views;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeSliderTrial.Controls
{
    public class ThumbBorder : Border
    {
        public ThumbBorder()
        {
                
        }
    }



    public class ThumbBorderHandler : BorderHandler
    {
#if ANDROID
        protected override ContentViewGroup CreatePlatformView()
        {
            if (VirtualView == null)
            {
                throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a ContentViewGroup");
            }

            var viewGroup = new TouchControlledBorder(Context);

            // We only want to use a hardware layer for the entering view because its quite likely
            // the view will invalidate several times the Drawable (Draw).
            viewGroup.SetLayerType(Android.Views.LayerType.Hardware, null);

            return viewGroup;
        }
#endif
    }

#if ANDROID
    public class TouchControlledBorder : ContentViewGroup
    {
        public TouchControlledBorder(Context context) : base(context)
        {
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    Parent?.RequestDisallowInterceptTouchEvent(true);
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    Parent?.RequestDisallowInterceptTouchEvent(false);
                    break;
            }
            return base.OnTouchEvent(e);
        }
    }
#endif


}
