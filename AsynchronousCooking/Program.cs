using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousCooking
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------");

            var sitesList = new string[]
                {   "https://www.google.com",
                    "https://www.microsoft.com",
                    "https://tuoitre.vn",
                    "https://facebook.com"
                };

            var result = string.Empty;

            // The code below use async method to download 4 sites
            // A task of download site async will be executed in another thread (which connects to network card to download site)
            // main task is not blocked
            // after finished first download, the main task is resumed at the first keyword "await"
            // The the second task of download site started
            // Therefore in the code below, main thread is not blocked, however 4 tasks were not executed concurently
            /*
            result += await DownloadSiteAsync(sitesList[0]) + "\n";
            result += await DownloadSiteAsync(sitesList[1]) + "\n";
            result += await DownloadSiteAsync(sitesList[2]) + "\n";
            result += await DownloadSiteAsync(sitesList[3]) + "\n";
            */

            // The code below start 4 tasks concurently
            /*
            var task1 = DownloadSiteAsync(sitesList[0]);
            var task2 = DownloadSiteAsync(sitesList[1]);
            var task3 = DownloadSiteAsync(sitesList[2]);
            var task4 = DownloadSiteAsync(sitesList[3]);

            
            result += await task1 + "\n";
            result += await task2 + "\n";
            result += await task3 + "\n";
            result += await task4 + "\n";
            
            */
            int[] a = new int[] { 5,2,-1,7,0,9};
            int[] b= new int[] { 9,8,5,15,-2-1,88};
            GetMatches(a, b);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(result);
        }

        private static async Task<string> DownloadSiteAsync(string url)
        {
            Console.WriteLine($"Start {url}: {Thread.CurrentThread.ManagedThreadId}");

            var webclient = new WebClient();
            var result = await Task.Run( () => webclient.DownloadString(url));
            Console.WriteLine($"End {url}: {Thread.CurrentThread.ManagedThreadId}");

            return result.Length.ToString();
        }

        private static int[] GetMatches(int[] a, int[] b)
        {
            var result = new int[] { };

            SortAsc(a);
           // SortAsc(b);

            Console.WriteLine(a.ToString());
            return result;
        }
        

        // return the index that the element should be inserted
       private static int BinarySearch(int[] array, int e, int l, int r)
        {
            if (l >= r)
                return (e < array[l]) ? l : l+1;

            var mid = (l + r) / 2;
            if (e == array[mid]) return mid + 1;
            if (e > array[mid]) return BinarySearch(array, e, mid + 1, r);
            return BinarySearch(array, e, l, mid - 1);
        }

        // Function to sort an array a[] of size 'n' 
        private static void SortAsc(int[] a)
        {
            int i, loc, j, k, selected;
            int n = a.Length;

            for (i = 1; i < n; ++i)
            {
                j = i - 1;
                selected = a[i];

                // find location where selected sould be inseretd 
                loc = BinarySearch(a, selected, 0, j);

                // Move all elements after location to create space 
                while (j >= loc)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = selected;
            }
        }

    }
}
