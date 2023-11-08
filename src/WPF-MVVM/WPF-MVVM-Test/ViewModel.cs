using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Test;

namespace WPF_MVVM_Test
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Add
        {
            set
            {
                _model.AddValue(value);
                OnPropertyChanged("Sum"); // уведомление View о том, что изменилась сумма
            }
        }

        public int Remove
        {
            set { _model.RemoveValue(value); 
                OnPropertyChanged("Sum"); }
        }
        private readonly Model _model;

        public ViewModel()
        {
            _model = new Model();
        }

        public int Sum => _model.Sum;

        public ReadOnlyObservableCollection<int> MyValues => _model.MyPublicValues;
    }
}
