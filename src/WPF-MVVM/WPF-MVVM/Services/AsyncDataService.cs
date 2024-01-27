using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Services.Interfaces;

namespace WPF_MVVM.Services
{
    internal class AsyncDataService : IAsyncDataService
    {

        private const int SLEEPTIME = 5000;
        public string GetResult(DateTime dateTime)
        {
            Thread.Sleep(SLEEPTIME);

            return $"Result value {dateTime}";
        }
    }
}
