using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_palindromicprime3
{
    public static class ControlExtension
    {
        public static void UpdateControlSafe(this Control control, Action code)
        {
            // Uses the Dispatcher.CheckAccess method to determine if  
            // the calling thread has access to the thread the UI object is on.

            // Checking if this thread does NOT has access to the object. 
            if (!control.Dispatcher.CheckAccess())
            {
                control.Dispatcher.BeginInvoke(code);
            }
            else //Thread has access to the object
            {
                code.Invoke();
            }
        }
    }
}
