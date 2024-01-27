using System.Dynamic;
using System.Net;
using System.Net.Sockets;

namespace WPF_MVVM.Web
{
    public class WebServer
    {
        private HttpListener? _httpListener;
        private readonly int _port;
        private bool _enabled;
        private readonly object _syncRoot = new object();

        public int Port => _port;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value)
                    Start();
                else Stop();
            }
        }


        public WebServer(int port) => _port = port;


        public void Start()
        {
            if (_enabled) return;
            lock (_syncRoot)
            {
                if (_enabled) return;
                _httpListener = new HttpListener();
                _httpListener.Prefixes.Add($"http://*:{_port}");
                _httpListener.Prefixes.Add($"http://+:{_port}");
                _enabled = true;
            }
            Listen();
        }

        public void Stop()
        {
            if(! _enabled) return;
            lock (_syncRoot)
            {
                if( _enabled) return;
                _httpListener = null;
                _enabled = false;
            }
        }

        private void Listen()
        {

        }
    }
}
