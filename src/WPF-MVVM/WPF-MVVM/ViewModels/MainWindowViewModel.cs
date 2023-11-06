using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {

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
        
    }
}
