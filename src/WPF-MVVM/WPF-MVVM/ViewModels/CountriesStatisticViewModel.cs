using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Services;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class CountriesStatisticViewModel : BaseViewModel
    {
        private  MainWindowViewModel MainWindowViewModel { get; }

        private DataService _dataService;

        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            MainWindowViewModel = MainModel;

            _dataService = new DataService();
        }

    }
}
