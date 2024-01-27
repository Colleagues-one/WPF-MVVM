using System;
using System.Threading;

namespace WPF_MVVM_Console
{
    internal class Program
    {
         
        static void Main(string[] args)
        {
            int count = 5;
            string message = "Hello World!";
            int timeSleep = 100;
            new Thread(()=>PrintMessage(message,count,timeSleep)) { IsBackground = true }.Start();
       
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



