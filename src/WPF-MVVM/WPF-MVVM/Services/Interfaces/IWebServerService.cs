using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM.Services.Interfaces
{
    internal interface IWebServerService
    {
        public bool Enabled { get; set; }
        public void Start();
        public void Stop();
    }
}
