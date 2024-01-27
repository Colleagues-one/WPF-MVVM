using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Web;

namespace WPF_MVVM_Console
{
    internal class WebServerTest
    {
        public static void Run()
        {
            var server = new WebServer(8080);
            server.RequestReceiver += OnRequestReceived;
            server.Start();
            Console.WriteLine("Сервер запущен!");
            Console.ReadLine();
        }

        private static void OnRequestReceived(object? sender, RequestReceiverEventArgs e)
        {
            var context = e.Context;
            Console.WriteLine("Connection {0}",context.Request.UserHostAddress);
            using var writer = new StreamWriter(context.Response.OutputStream);
            writer.WriteLine("Hello from Test Web Server!");
        }
    }
}
