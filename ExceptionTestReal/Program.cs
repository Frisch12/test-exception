using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace ExceptionTestReal
{
    internal class Program
    {
        private static bool DoRun = false;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            DoRun = true;
            for (int i = 0; i < 16; i++)
            {
                new Thread(Run).Start();
            }
            
            Console.WriteLine("Start work...");

            string line;
            do
            {
                line = Console.ReadLine();
            } while (line != "exit");
            Console.WriteLine("Terminating...");
            DoRun = false;
        }

        private static void Run()
        {
            int i = 0;
            while (DoRun)
            {
                try
                {
                    Func<Task> doWork = async () =>
                    {
                        await ((Func<Task>) (async () => await Task.Run(() => throw new NotImplementedException()).ConfigureAwait(false)))().ConfigureAwait(false);
                    };
                    doWork.ShouldThrow<NotImplementedException>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed: " + ex);
                }
                if (++i % 10000 == 0)
                {
                    Console.WriteLine("Working...");
                    i = 0;
                }
            }
        }
    }
}