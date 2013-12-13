using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_palindromicprime3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadBtnLinear_Checked(object sender, RoutedEventArgs e)
        {
            if (RadBtnLinear.IsChecked == true)
            {
                Style styleY = this.FindResource("YellowText") as Style;
                Style styleG = this.FindResource("GreenText") as Style;
                RadBtnLinear.Style = styleY;
                RadBtnBinary.Style = styleG;
            }
        }

        private void RadBtnBinary_Checked(object sender, RoutedEventArgs e)
        {
            if (RadBtnBinary.IsChecked == true)
            {
                Style styleY = this.FindResource("YellowText") as Style;
                Style styleG = this.FindResource("GreenText") as Style;
                RadBtnBinary.Style = styleY;
                RadBtnLinear.Style = styleG;
            }
        }

        private void FileHelp_Click(object sender, RoutedEventArgs e)
        {
            string alg;
            if (RadBtnLinear.IsChecked == true)
            {
                alg = "linear";
                DefaultContentForLabels(alg);
            }
            else 
            {
                alg = "binary";
                DefaultContentForLabels(alg);
            }
        }

        private void FileExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /*Implements synchronous thread waiting during Button event*/
        ManualResetEvent syncEvent0 = new ManualResetEvent(false);//red light #0
        ManualResetEvent syncEvent1 = new ManualResetEvent(false);//red light #1
        ManualResetEvent syncEvent2 = new ManualResetEvent(false);//red light #2
        ManualResetEvent syncEvent3 = new ManualResetEvent(false);//red light #3
        ManualResetEvent syncEvent4 = new ManualResetEvent(false);//red light #4
     
        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            /* select search algorithm */
            string search_algorithm = "binary";//default
            if (RadBtnLinear.IsChecked == true)
                search_algorithm = "linear";

            /* Redraw appropriate labels' content */
            DefaultContentForLabels(search_algorithm);
            
            List<int> act_pi_data = new List<int>();
            Thread t1 = new Thread(
                () => 
                {
                    /*Open file and obtain 20,000 digits of PI*/
                    GetPiFromFile(ref act_pi_data);
                    syncEvent0.Set();
                }
            );
            t1.Start();

            int minPossPrime = 1000000; //1,000,000
            int maxPossPrime = 10000000; //10,000,000
            List<int> primes = new List<int>(); //C# List<T> msdn.microsoft.com/en-us/library/6sh2ey19.aspx
            primes.Add(2);
            Thread t2 = new Thread( 
                () => 
                {
                    syncEvent0.WaitOne();
                    /*Calculate 7 digit prime numbers falling in the range: 1,000,000-9,999,999*/
                    CalculatePrimes(ref primes, maxPossPrime);
                    syncEvent1.Set();
                    syncEvent0.Reset();
                }
            );
            t2.Start();
            
            List<int> candidate_primes = new List<int>(); //C# List<T> msdn.microsoft.com/en-us/library/6sh2ey19.aspx
            Thread t3 = new Thread(
                () =>
                {
                    syncEvent1.WaitOne();
                    /* Stores all elements 1,000,000-9,999,999 in a new List<int> candidate_primes*/
                    StoreCandidatePrime(primes, ref candidate_primes, minPossPrime);
                    syncEvent2.Set();
                    syncEvent1.Reset();
                }
            );
            t3.Start();

            List<int> r_candidate_primes = new List<int>();            
            Thread t4 = new Thread(
                () =>
                {
                    syncEvent2.WaitOne();
                    /*Get the reverse string*/
                    ReverseCandidatePrimes(ref r_candidate_primes, candidate_primes);
                    syncEvent3.Set();
                    syncEvent2.Reset();
                }
            );
            t4.Start();

            List<int> palindromic_cprimes = new List<int>();
            Thread t5 = new Thread(
                () =>
                {
                    syncEvent3.WaitOne();
                    /*Compare candidate_primes(7-digit primes) with r_candidate_primes(reversed) 
                     * & save in palindromic_cprimes(potential candidates) */
                    StorePalindromicCandidatePrimes(ref palindromic_cprimes, r_candidate_primes, candidate_primes);
                    syncEvent4.Set();
                    syncEvent3.Reset();
                }
            );
            t5.Start();

            Thread t6 = new Thread(
                () =>
                {
                    syncEvent4.WaitOne();
                    /*Then the potential candidates should be compared to the actual PI data*/
                    Palindromic_Search(palindromic_cprimes, act_pi_data, search_algorithm);
                    syncEvent4.Reset();
                }
            );
            t6.Start();

            //* NOTES
            //If a number has divisors,
            //at least one of them must be less than or equal to the square root of the number. 
            //When you check divisors, 
            //you only need to check up to the square root, 
            //not all the way up to the number being tested.
            //(end notes)

            /*Not necessary, but just in case*/
            //primes.Clear();
            //candidate_primes.Clear();
            //r_candidate_primes.Clear();
            //palindromic_cprimes.Clear();
        }
    }
}
