using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_palindromicprime3
{
    partial class MainWindow
    {
        public void StorePalindromicCandidatePrimes(ref List<int> palindromic_cprimes, List<int> r_candidate_primes, List<int> candidate_primes)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int j_index = 0;
            foreach (int i in candidate_primes)
            {
                bool equality_test = false;
                //compare the reversed List, with Original List
                equality_test = CompareInt(i, r_candidate_primes.ElementAt<int>(j_index));
                /*store it if its a match*/
                if (equality_test)
                {
                    palindromic_cprimes.Add(i);
                }
                j_index++;
            }
            int firstPalindCprime = palindromic_cprimes[0];
            int lastPalindCprime = palindromic_cprimes[palindromic_cprimes.Count() - 1];
            int elements = palindromic_cprimes.Count();
            sw.Stop();
            string result5;
            result5 = String.Format("");
            result5 += String.Format("Elapsed time to compare & store Palindromic Candidate Primes (in ms): {0}\n", sw.ElapsedMilliseconds);
            result5 += String.Format(" Added qualified values to Palindromic Candidate Primes list.\n");
            result5 += String.Format(" Palindromic Candidate prime numbers are from {0} to {1}\n", palindromic_cprimes[0],
                                                                             palindromic_cprimes[palindromic_cprimes.Count() - 1]);
            result5 += String.Format(" Elements in palindromic_cprimes: {0}\n", palindromic_cprimes.Count());
            lblResult5.UpdateControlSafe(new Action(() => lblResult5.Content = result5));
            //lblResult5.Content += String.Format(" Added qualified values to Palindromic Candidate Primes list.\n");
            //lblResult5.Content += String.Format((" Palindromic Candidate prime numbers are from {0} to {1}\n"), palindromic_cprimes[0],
            //                                                                 palindromic_cprimes[palindromic_cprimes.Count() - 1]);
            //lblResult5.Content += String.Format(" Elements in palindromic_cprimes: {0}\n", palindromic_cprimes.Count());
        }

        /* Support method for comparing two 7-digit List<int> (original & reversed) checking for palindromes */
        public static bool CompareInt(int a, int b)
        {
            if (a == b)
                return true;
            else
                return false;
        }

    }
}
