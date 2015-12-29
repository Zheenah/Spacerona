using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source
{
    public static class InputEventSystem
    {
        public static event EventHandler LeftTap, RightTap;


        public static void OnLeftTap(object sender)
        {
            EventHandler handler = LeftTap;
            if (handler != null) handler(sender, EventArgs.Empty);
        }
        public static void OnRightTap(object sender)
        {
            EventHandler handler = RightTap;
            if (handler != null) handler(sender, EventArgs.Empty);
        }

    }
}
