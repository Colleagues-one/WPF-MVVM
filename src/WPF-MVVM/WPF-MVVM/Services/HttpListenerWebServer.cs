using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using WPF_MVVM.Web;

namespace WPF_MVVM.Services.Interfaces
{
    internal class HttpListenerWebServer : IWebServerService
    {
        private WebServer _server = new WebServer(8080);
        public bool Enabled { get=>_server.Enabled; set => _server.Enabled = value; }
        public void Start()=>_server.Start();

        public void Stop()=>_server.Stop();

        public HttpListenerWebServer()
        {
            _server.RequestReceiver += OnRequestReceived;
        }

        private void OnRequestReceived(object sender, RequestReceiverEventArgs e)
        {
            using (var writter = new StreamWriter(e.Context.Response.OutputStream))
            {
               writter.WriteLine("WPF-MVVM Application " + DateTime.Now);          
            }
        }
    }
}
