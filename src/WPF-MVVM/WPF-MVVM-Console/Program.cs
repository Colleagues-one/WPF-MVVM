using System;
using System.Threading;
using System.Collections.Concurrent;

namespace WPF_MVVM_Console
{
    internal class Program
    {
         
        static void Main(string[] args)
        {
            /*int count = 5;
            string message = "Hello World!";
            int timeSleep = 100;
            new Thread(()=>PrintMessage(message,count,timeSleep)) { IsBackground = true }.Start();*/
       
            var values = new List<int>();
            var threads = new Thread[10];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(()=>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        lock (values)
                        {
                            values.Add(Thread.CurrentThread.ManagedThreadId);
                        }
                    }
                });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            Console.WriteLine(String.Join(", ", values));
        }





        public static void PrintMessage(string msg, int count, int timeSleep)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(msg);
                Thread.Sleep(timeSleep);
            }
        }

        private static void CheckThread()
        {
            Console.WriteLine("{0}:{1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
        }
        
    }
}



