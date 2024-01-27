using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class WebServerViewModel:BaseViewModel
    {
        #region Enabled

        private bool _enabled;
        public bool Enabled { get => _enabled; set => Set(ref _enabled, value); }

        #endregion


        #region ICommand Start (object) - Запуск сервера


        /// <summary>
        /// Запуск сервера
        /// </summary>
        public ICommand StartCommand => new RelayCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object parameter) => !_enabled;

        private void OnStartCommandExecuted(object parameter)
        {
            Enabled = true;
        }

        #endregion


        #region ICommand Stop (object) - Остановка сервера

        /// <summary>
        /// Остановка сервера
        /// </summary>
        public ICommand StopCommand => new RelayCommand(OnStopCommandExecuted, CanStopCommandExecute);

        private bool CanStopCommandExecute(object parameter) => _enabled;

        private void OnStopCommandExecuted(object parameter)
        {
            Enabled = false;
        }

        #endregion
    }
}
