using System.Dynamic;
using System.Net;
using System.Net.Sockets;

namespace WPF_MVVM.Web
{
    public class WebServer
    {
        private event EventHandler<RequestReceiverEventArgs> RequestReceiver; 
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
            //ListenAsync();
        }

        public void Stop()
        {
            if (!_enabled) return;
            lock (_syncRoot)
            {
                if (_enabled) return;
                _httpListener = null;
                _enabled = false;
            }
        }

        private async void ListenAsync()
        {
            if(_httpListener == null) return;
            var listener = _httpListener;
            listener.Start();
          
            while (_enabled)
            {
                var receivedContext = await listener.GetContextAsync().ConfigureAwait(false);
                ProcessRequest(receivedContext);
            }

            listener.Stop();
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            RequestReceiver?.Invoke(this, new RequestReceiverEventArgs(context));
        }
    }
    public class RequestReceiverEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }

        public RequestReceiverEventArgs(HttpListenerContext context) => Context = context;
    }
}
