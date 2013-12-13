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
        public void StoreCandidatePrime(List<int> primes, ref List<int> candidate_primes, int minPossPrime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (int i in primes)
            {
                if (i >= minPossPrime)
                {
                    candidate_primes.Add(i);
                }
            }
            int firstCandPrime = candidate_primes[0];
            int lastCandPrime = candidate_primes[candidate_primes.Count() - 1];
            int elements = candidate_primes.Count();
            sw.Stop();
            string result3;
            result3 = String.Format("");
            result3 += String.Format("Elapsed time to store Candidate Primes (in ms): {0}\n", sw.ElapsedMilliseconds);
            result3 += String.Format(" Added qualified values to Candidate Primes list.\n");
            result3 += String.Format(" Candidate prime numbers are from {0} to {1}\n", candidate_primes[0],
                                                                             candidate_primes[candidate_primes.Count() - 1]);
            result3 += String.Format(" Elements in candidate_primes: {0}\n", candidate_primes.Count());
            lblResult3.UpdateControlSafe(new Action(() => lblResult3.Content = result3));
            //lblResult3.Content += String.Format(" Added qualified values to Candidate Primes list.\n");
            //lblResult3.Content += String.Format(" Candidate prime numbers are from {0} to {1}\n", candidate_primes[0],
            //                                                                 candidate_primes[candidate_primes.Count() - 1]);
            //lblResult3.Content += String.Format(" Elements in candidate_primes: {0}\n", candidate_primes.Count());
        }
    }
}
