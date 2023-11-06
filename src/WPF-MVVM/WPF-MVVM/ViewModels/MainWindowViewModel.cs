using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {

        #region Заголовок окна
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
    }
}
