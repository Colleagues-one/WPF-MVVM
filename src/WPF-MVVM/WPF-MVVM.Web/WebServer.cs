using System.Dynamic;
using System.Net;
using System.Net.Sockets;

namespace WPF_MVVM.Web
{
    public class WebServer
    {
        public event EventHandler<RequestReceiverEventArgs> RequestReceiver; 
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
                _httpListener = HttpListenerCreator($"http://*:{_port}/", $"http://+:{_port}/");
                _enabled = true;
                ListenAsync();
            }
        }

        public void Stop()
        {
            if (!_enabled) return;
            lock (_syncRoot)
            {
                if (!_enabled) return;
                _httpListener = null;
                _enabled = false;
            }
        }

        private async void ListenAsync()
        {
            if(_httpListener == null) return;
             var listener = _httpListener;

            //System.Net.HttpListenerException: "Failed to listen on prefix 'http://*:8080/' because it conflicts with an existing registration on the machine."
            // при перезапуске сервера
           
            listener.Start();
            
            HttpListenerContext context = null;
            while (_enabled)
            {
                var getContextTask = listener.GetContextAsync();
                if (context != null)
                    ProcessRequestAsync(context);
                context = await getContextTask.ConfigureAwait(false);
            }

            listener.Stop();
        }

        public HttpListener HttpListenerCreator(params string[] prefixes)
        {
            try
            {
                var listener = new HttpListener();
                
                    foreach (var prefix in prefixes)
                    {
                        listener.Prefixes.Add(prefix);
                    }
                    listener.Start();
                    listener.Stop();
                return listener;
            }
            catch (HttpListenerException ex)
            {
                return new HttpListener();
            }
        }


        private async void ProcessRequestAsync(HttpListenerContext context)
        {
            await Task.Run(() => RequestReceiver?.Invoke(this, new RequestReceiverEventArgs(context)));
        }
    }
    public class RequestReceiverEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }

        public RequestReceiverEventArgs(HttpListenerContext context) => Context = context;
    }
}
