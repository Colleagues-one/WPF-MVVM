using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.Models;
using WPF_MVVM.Services;
using WPF_MVVM.Services.Interfaces;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class CountriesStatisticViewModel : BaseViewModel
    {
        public MainWindowViewModel MainWindowViewModel { get; internal set; }

        private readonly IDataService _dataService;



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

        #region SelectedCountry : CountryInfo - Выбранная страна из списка стран

        /// <summary>
        /// field Выбранная страна из списка стран
        /// </summary>
        private CountryInfo _SelectedCountry;

        /// <summary>
        ///  attribute Выбранная страна из списка стран
        /// </summary>
        public CountryInfo SelectedCountry
        {
            get => _SelectedCountry;
            set => Set(ref _SelectedCountry, value);
        }

        #endregion



        #region Commands

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

        #endregion


        public CountriesStatisticViewModel(IDataService service)
        {
            _dataService = service;

            #region Commands

            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);

            #endregion
        }

        /// <summary>
        /// отладочный конструктор, используемый в процессе разработки в визуальном дизайнере
        /// </summary>
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
            {
                throw new InvalidOperationException(
                    "Вызов конструктора, не предназначенного для использования в обычном режиме!");
            }

            _Countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
            {
                Name = $"Country {i}",
                ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                {
                    Name = $"Province{i}",
                    Location = new Point(i, j),
                    Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                    {
                        Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                        Count = k
                    }).ToArray()
                }).ToArray()
            }).ToArray();
        }
    }
}
