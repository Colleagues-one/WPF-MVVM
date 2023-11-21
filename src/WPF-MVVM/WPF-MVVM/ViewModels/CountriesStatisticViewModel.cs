using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.Models;
using WPF_MVVM.Services;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class CountriesStatisticViewModel : BaseViewModel
    {
        private  MainWindowViewModel MainWindowViewModel { get; }

        private DataService _dataService;



        #region Countries : IEnumerable<CountryInfo> - Статистика по странам

        /// <summary>
        /// field Статистика по странам
        /// </summary>
        private IEnumerable<CountryInfo> _Countries;

        /// <summary>
        ///  attribute Статистика по странам
        /// </summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        }

        #endregion



        #region ICommand RefreshData (object) - Команда обновления данных

        /// <summary>
        /// Команда обновления данных
        /// </summary>
        public ICommand RefreshDataCommand { get; }

        private bool CanRefreshDataCommandExecute(object parameter) => true;

        private void OnRefreshDataCommandExecuted(object parameter)
        {
            Countries = _dataService.GetData();
        }

        #endregion


        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {


            MainWindowViewModel = MainModel;

            _dataService = new DataService();

            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
        }

    }
}
