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
        public void CalculatePrimes(ref List<int> listofprimes, int maxprime)
        {
            //In this example, we keep track of all of our previously calculated primes. 
            //If a number is divisible by a non-prime number, 
            //there is also some prime <= that divisor which it is also divisble by. 
            //This reduces computation by a factor of primes_in_range/total_range.
            //           For up to 13x13, that's                    6/12           so 50% of total computation.
            //int.MaxValue = 2 147 483 647
            //7 digit prime numbers fall in the range: 1,000,000-9,999,999
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 3; i < maxprime + 1; i++) //+1 to increase calculated range to include maxPossPrime
            {
                bool prime = true;
                //stay in index range of 'primes' List<int>
                for (int j = 0; j < listofprimes.Count() && listofprimes[j] * listofprimes[j] <= i; j++)
                {                                     //check for divisors up to Sqrt of i *See NOTES
                    //j*j sequence, 4, 9, 25, 49, 121, 169 (<- usually 13 primes than just 6)
                    if (i % listofprimes[j] == 0)
                    {
                        prime = false;
                        break;
                    }
                }
                if (prime)
                {
                    listofprimes.Add(i);
                }
            }
            int firstprime = listofprimes[0];
            int elements = listofprimes.Count();
            sw.Stop();
            string result2;
            result2 = String.Format("");
            result2 += String.Format("Elapsed time to calculate & store Primes (in ms): {0}\n", sw.ElapsedMilliseconds);
            result2 += String.Format(" Prime numbers from {0} to {1}\n", firstprime, maxprime/*primes[primes.Count()-1]*/);
            result2 += String.Format(" Elements in primes: {0}\n", elements);
            lblResult2.UpdateControlSafe(new Action(() => lblResult2.Content = result2));
            //lblResult2.UpdateControlSafe(new Action( () => lblResult2.Content = String.Format("") ));
            //lblResult2.UpdateControlSafe(new Action( () => lblResult2.Content += String.Format(" Prime numbers from {0} to {1}\n", firstprime, maxprime/*primes[primes.Count()-1]*/) ));
            //lblResult2.UpdateControlSafe(new Action( () => lblResult2.Content += String.Format(" Elements in primes: {0}\n", elements) ));
        }
    }
}
