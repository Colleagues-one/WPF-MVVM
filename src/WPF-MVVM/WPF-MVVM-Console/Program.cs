using System;
using System.Threading;
using System.Collections.Concurrent;

namespace WPF_MVVM_Console
{
    internal class Program
    {

        private static bool THREAD_UPDATE = true;
         
        static void Main(string[] args)
        {
            var clockThread = new Thread(ThreadMethod);
            clockThread.IsBackground = true;
            clockThread.Priority = ThreadPriority.AboveNormal;
       
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

            if (!clockThread.Join(100))
            {
                clockThread.Interrupt();
            }

            Console.WriteLine(String.Join(", ", values));
        }


        public static void ThreadMethod()
        {
            while (THREAD_UPDATE)
            {
                Console.Title = DateTime.Now.ToString();
            }
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



