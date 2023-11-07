using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.Models;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {

        #region TestDataPoints :IEnumerable<DataPoint> - Тестовый
        /// <summary>
        /// Тестовый набор дданных визуализации данных
        /// </summary>
        private IEnumerable<DatePoint> _testDataPoints;
        /// <summary>
        /// Тестовый набор дданных визуализации данных
        /// </summary>
        public IEnumerable<DatePoint> TestDatePoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        } 
        #endregion

        #region Title Заголовок окна
        private string _title = "MainWindow";

        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _title;
           /* set сеттер если бы бы базовый класс не имел виртуального метода Set
            {
                if (Equals(value, _title)) return;
                _title = value;
                OnPropertyChanged();
                Set(ref _title, value);
            }*/ 
            set => Set(ref _title, value);
        }
        #endregion

        #region Status Статус окна
        private string _status = "Status";
        /// <summary> статус программы </summary>
        public string Status { get => _status; set => Set(ref _status, value); }
        #endregion

        #region Commands 

        #region CloseAppCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object sender) => true;

        private void OnCloseApplicationCommandExecuted(object sender)
        {
            Application.Current.Shutdown();
        }

        #endregion
        #endregion


        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand =
                new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion
            var data_Points = new List<DatePoint>((int)(360/0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                data_Points.Add((new DatePoint{XValue = x, YValue = y}));
            }

            TestDatePoints = data_Points;
        }
    }
}
