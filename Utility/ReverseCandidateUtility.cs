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
        public void ReverseCandidatePrimes(ref List<int> r_candidate_primes, List<int> candidate_primes)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (int i in candidate_primes)
            {
                int temp;
                int.TryParse((ReverseString(i.ToString())), out temp);
                r_candidate_primes.Add(temp);
            }
            int firstRCandPrime = r_candidate_primes[0];
            int lastRCandPrime = r_candidate_primes[r_candidate_primes.Count() - 1];
            int elements = r_candidate_primes.Count();
            sw.Stop();
            string result4;
            result4 = String.Format("");
            result4 += String.Format("Elapsed time to calculate & store Reversed Candidate Primes (in ms): {0}\n", sw.ElapsedMilliseconds);
            result4 += String.Format(" Added qualified values to Rev Candidate Primes list.\n");
            result4 += String.Format(" Rev Candidate prime numbers are from {0} to {1}\n", r_candidate_primes[0],
                                                                             r_candidate_primes[r_candidate_primes.Count() - 1]);
            result4 += String.Format(" Elements in r_candidate_primes: {0}\n", r_candidate_primes.Count());
            lblResult4.UpdateControlSafe(new Action(() => lblResult4.Content = result4));
            //lblResult4.Content += String.Format(" Added qualified values to Rev Candidate Primes list.\n");
            //lblResult4.Content += String.Format(" Rev Candidate prime numbers are from {0} to {1}\n", r_candidate_primes[0],
            //                                                                 r_candidate_primes[r_candidate_primes.Count() - 1]);
            //lblResult4.Content += String.Format(" Elements in r_candidate_primes: {0}\n", r_candidate_primes.Count());
        }

        /* Support method for getting the reverse string */
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
