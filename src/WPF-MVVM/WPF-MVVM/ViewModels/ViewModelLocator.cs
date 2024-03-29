﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WPF_MVVM.Models.Decanat;

namespace WPF_MVVM.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();

        public StudentsManagementViewModel StudentsManagement => App.Host.Services.GetRequiredService<StudentsManagementViewModel>();
    }
}
