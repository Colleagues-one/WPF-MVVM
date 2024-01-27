using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.Services.Interfaces;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class WebServerViewModel:BaseViewModel
    {

        private readonly IWebServerService _server;

        #region Enabled

        public bool Enabled
        {
            get => _server.Enabled;
            set
            {
                _server.Enabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ICommand Start (object) - Запуск сервера


        /// <summary>
        /// Запуск сервера
        /// </summary>
        public ICommand StartCommand => new RelayCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object parameter) => !Enabled;

        private void OnStartCommandExecuted(object parameter)
        {
            _server.Start();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion

        #region ICommand Stop (object) - Остановка сервера

        /// <summary>
        /// Остановка сервера
        /// </summary>
        public ICommand StopCommand => new RelayCommand(OnStopCommandExecuted, CanStopCommandExecute);

        private bool CanStopCommandExecute(object parameter) => Enabled;

        private void OnStopCommandExecuted(object parameter)
        {
            _server.Stop();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion

        public WebServerViewModel(IWebServerService server)
        {
            _server = server;
        }
    }
}
