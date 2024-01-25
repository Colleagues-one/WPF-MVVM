using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WPF_MVVM.Services.Interfaces;

namespace WPF_MVVM.Services
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataService, DataService>();
            services.AddTransient<IAsyncDataService, AsyncDataService>();
            //services.AddTransient<IDataService, DataService>(); создается когда требуется

            /* services.AddScoped<IDataService, DataService>(); создание области для обработки сервиса
             using (var scope = App.Host.Services.CreateScope())
             {
                 var data = scope.ServiceProvider.GetRequiredService<IDataService>();
             }*/
            return services;
        }
    }
}
