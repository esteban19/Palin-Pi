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
        /* Search for Prime number in sequence of PI using the prime number list */
        public void Palindromic_Search(List<int> prime_list, List<int> pi_sequence, string search_method)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool found = false;
            string result6 = "";
            int max_search = 19993; //19993 is last search
            int pi_index = 0;
            while (found == false && pi_index <= max_search)
            {
                string pi_str_seven = pi_sequence[pi_index].ToString() + pi_sequence[pi_index + 1].ToString() +
                                  pi_sequence[pi_index + 2].ToString() + pi_sequence[pi_index + 3].ToString() +
                                  pi_sequence[pi_index + 4].ToString() + pi_sequence[pi_index + 5].ToString() +
                                  pi_sequence[pi_index + 6].ToString();
                int pi_int_seven = Convert.ToInt32(pi_str_seven);

                //Console.WriteLine("PI(str)7: Value {0}. PI(int)7: Type {1} Value {2}", pi_str_seven, pi_int_seven.GetType(), pi_int_seven);
                switch (search_method)
                {
                    case "binary":
                        {
                            string binary_result = BinarySearch(prime_list, pi_int_seven, 0, prime_list.Count() - 1);
                            if (binary_result.Contains("Success"))
                            {
                                sw.Stop();
                                result6 += String.Format("Binary Search algorithm returns:\n {0}\n", binary_result);
                                //Console.WriteLine("Binary Search algorithm returns:\n {0}", binary_result);
                                found = true;
                                result6 += String.Format(" 7 digit palindromic prime # at position {0} and value {1}\n", pi_index, pi_int_seven);
                                result6 += String.Format(" Elapsed time to search PI for the 7-digit palindrome (in ms): {0}\n", sw.ElapsedMilliseconds);
                                //Console.WriteLine(" 7 digit palindromic prime # at position {0} and value {1}", pi_index, pi_int_seven);
                            }
                            else //Key not found.
                            {
                                //increment pi_index towards max_search by 1
                                pi_index++;
                            }
                            break;
                        }
                    case "linear":
                        {
                            string linear_result = LinearSearch(prime_list, pi_int_seven, prime_list.Count() - 1);
                            if (linear_result.Contains("Success"))
                            {
                                sw.Stop();
                                result6 += String.Format("Linear Search algorithm returns:\n {0}\n", linear_result);
                                found = true;
                                result6 += String.Format(" 7 digit palindromic prime # at position {0} and value {1}\n", pi_index, pi_int_seven);
                                result6 += String.Format(" Elapsed time to search PI for the 7-digit palindrome (in ms): {0}\n", sw.ElapsedMilliseconds);
                            }
                            else //Key not found.
                            {
                                //increment pi_index towards max_search by 1
                                pi_index++;
                            }
                            break;
                        }
                    default:
                        {
                            sw.Stop();
                            found = true; //not really, just used to skip unnecessary result strings below.
                            result6 += String.Format("Invalid index parameter in MainWindow.Palindromic_Search()");
                            result6 += String.Format(" [int search_method] parameter must be 0 or 1.");
                            result6 += String.Format(" Elapsed time (in ms): {0}\n", sw.ElapsedMilliseconds);
                            break;
                        }
                }//end switch
            }//end while (finished all 20000)
            if (found == false)
            {
                sw.Stop();
                result6 += String.Format("Search algorithm did not find any 7 digit palindromic prime in PI.\n");
                result6 += String.Format(" Elapsed time to run search algorithm (in ms): {0}\n", sw.ElapsedMilliseconds);
            }
            //print result to label 6
            lblResult6.UpdateControlSafe(new Action(() => lblResult6.Content = result6));
        }

        /* Pass in the key from the sequence of PI and search it against known 7 digit prime # palindromes */
        public static string BinarySearch(List<int> palindrome_cprimes, int key, int imin, int imax)
        {
            while (imax >= imin)
            {
                //calculate midpoint for roughly equal partitions
                int imid = midpoint(imin, imax);
                //determine which subarray to search
                if (palindrome_cprimes[imid] < key)
                {
                    //Console.Write("palindrome_cprimes[{0}]={1} too small, ", imid, palindrome_cprimes[imid]);
                    //change min index to search upper subarray
                    imin = imid + 1;
                    //Console.Write("increasing imin. Imax[{0}] Imin[{1}]\n", imax, imin);
                }
                else if (palindrome_cprimes[imid] > key)
                {
                    //Console.Write("palindrome_cprimes[{0}]={1} too large, ", imid, palindrome_cprimes[imid]);
                    //change max index to search lower subarray
                    imax = imid - 1;
                    //Console.Write("decreasing imax. Imax[{0}] Imin[{1}]\n", imax, imin);
                }
                else
                {
                    //key found at index imid
                    //Console.WriteLine();
                    return "Success. Found at palindrome_cprimes[" + imid.ToString() + "] = " + (palindrome_cprimes[imid]).ToString() + ".";
                }
            }
            return "Key not found. Imax[" + imax.ToString() + "] Imin[" + imin.ToString() + "]";
        }

        /* Support method for Binary search */
        public static int midpoint(int min, int max)
        {
            int mid;
            return mid = min + ((int)(Math.Floor(((double)max - (double)min) / 2.0)));
        }

        /* Linear search algorithm (slightly slower than binary search) */
        public static string LinearSearch(List<int> palindrome_list, int totest, int max_index) //NOTE: totest = value to search for from PI sequence
        {
            int k_index = 0;
            while (k_index <= max_index)//[0]=1003001 to [667]=9989899, max_index=667
            {
                //Console.Write("[{0}] ", palindrome_list[k_index]);
                if (palindrome_list[k_index] == totest)
                {
                    return "Success. Value " + totest.ToString() + " exists at palindrome_list[" + k_index.ToString() + "]=" + (palindrome_list[k_index]).ToString() + ".";
                    //Console.WriteLine();
                    //Console.WriteLine("Value {0} exists at palindrome_list[{1}]={2}.", totest, k_index, palindrome_list[k_index]);
                }
                k_index++;
            }
            return "Key not found.";
        }
    }
}
