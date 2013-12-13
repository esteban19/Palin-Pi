using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_palindromicprime3
{
    partial class MainWindow
    {
        public void DefaultContentForLabels(string search_algorithm)
        {
            lblResult1.Content = "1. Read Pi to 20,000 digits.";
            lblResult2.Content = "2. Calculate and Store primes (2-10,000,000)";
            lblResult3.Content = "3. Store 7-digit primes (1,000,000-9,999,999)";
            lblResult4.Content = "4. Store reversed 7-digit primes.";
            lblResult5.Content = "5. Compare and Store palindromic primes.";
            if (search_algorithm.Contains("binary"))
                lblResult6.Content = "6. Binary Search algorithm attempts to find a 7-digit palindromic prime in Pi.";
            else
                lblResult6.Content = "6. Linear Search algorithm attempts to find a 7-digit palindromic prime in Pi.";
        }
    }
}
