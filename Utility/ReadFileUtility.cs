using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_palindromicprime3
{
    partial class MainWindow
    {
        public void GetPiFromFile(ref List<int> act_pi_data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader act_pitxtdata = new StreamReader("../../pi_txt_20000_1.txt");
            string act_pi = (act_pitxtdata.ReadToEnd()).ToString();
            act_pitxtdata.Close();
            foreach (char i in act_pi)
            {
                act_pi_data.Add(Convert.ToInt32(i) - 48);
            }
            sw.Stop();
            string result1;//intended for lbl1
            result1 = String.Format("");
            result1 += String.Format("Elapsed time to read Pi to 20000 digits (in ms): {0}\n", sw.ElapsedMilliseconds);
            result1 += String.Format(" PI carried to {0} digits\n  and is of type {1}\n", act_pi_data.Count, act_pi_data.GetType());
            //The calling thread cannot access this object because another thread owns it.
            //          (i.e thread1)                              (i.e. main GUI thread)
            /*lblResult1.Dispatcher.Invoke(new Action(() => lblResult1.Content = results), 
                                         System.Windows.Threading.DispatcherPriority.Normal, 
                                         null);*/
            //*Extension method*
            //All the UI elements which inherits the control will have this
            // UpdateControlSafe extension method
            lblResult1.UpdateControlSafe(new Action(() => lblResult1.Content = result1));
        }
    }
}
