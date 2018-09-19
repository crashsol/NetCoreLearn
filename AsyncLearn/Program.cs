using System;
using System.Threading.Tasks;

namespace AsyncLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting....");

            var progress = new Progress<double>();

            progress.ProgressChanged += (sender, e) =>
            {
                Console.WriteLine(e);
            };
            MyMethodAsync(progress).Wait();

            Console.ReadLine();

        }


        static async Task MyMethodAsync(IProgress<double> progress)
        {
            double precentComplete = 0;
            bool done = false;
            while (!done)
            {
                await Task.Delay(100);
                if (progress != null)
                {
                    progress.Report(precentComplete);
                }
                precentComplete++;
                if (precentComplete == 100)
                {
                    done = true;
                }
            }
        }
    }


   
}
