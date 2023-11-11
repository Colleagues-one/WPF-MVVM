using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_MVVM_Test.Base;

namespace WPF_MVVM_Test
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Model _model;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Sum => _model.Sum;

        public ReadOnlyObservableCollection<int> MyValues => _model.MyPublicValues;

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName); return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region SelectedItem : int - Selected Item from collection

        /// <summary>
        /// field Selected Item from collection
        /// </summary>
        private int _SelectedItem;

        /// <summary>
        ///  attribute Selected Item from collection
        /// </summary>
        public int SelectedItem
        {
            get => _SelectedItem;
            set
            {
                Set(ref _SelectedItem, value);
                if(SelectedItem >=0 )
                Item = MyValues[_SelectedItem];
            }

        }

        #endregion

        #region Item : int - value in text box item

        /// <summary>
        /// field value in text box item
        /// </summary>
        private int? _Item;

        /// <summary>
        ///  attribute value in text box item
        /// </summary>
        public int? Item
        {
            get => _Item;
            set
            {
                Set(ref _Item, value);
                
            }

        }

        #endregion



        #region ICommand AddItem (int) - Adding new item in collection

        /// <summary>
        /// Adding new item in collection
        /// </summary>
        public ICommand AddItemCommand { get; }

        private bool CanAddItemCommandExecute(object parameter)
        {
            if(parameter == null) return false;
            if (int.TryParse(parameter.ToString(), out int number) && number != 0)
            {
                Item = number;
                return true;
            }
            else return false;
        }

        private void OnAddItemCommandExecuted(object parameter)
        {
            int number = Convert.ToInt32(parameter);
            _model.AddValue(number);
            OnPropertyChanged(nameof(Sum));
            Item = null;
        }

        #endregion

        #region ICommand DeleteItem (object) - Deleting selected item from collection

        /// <summary>
        /// Deleting selected item from collection
        /// </summary>
        public ICommand DeleteItemCommand { get; }

        private bool CanDeleteItemCommandExecute(object parameter) => _SelectedItem != null && _SelectedItem >=0 && MyValues.Count > SelectedItem;

        private void OnDeleteItemCommandExecuted(object parameter)
        {
                _model.RemoveValue(SelectedItem);
                
            OnPropertyChanged(nameof(Sum));
        }

        #endregion

        public ViewModel()
        {
            _model = new Model();
            AddItemCommand = new RelayCommand(OnAddItemCommandExecuted,CanAddItemCommandExecute);
            DeleteItemCommand = new RelayCommand(OnDeleteItemCommandExecuted, CanDeleteItemCommandExecute);
        }

        

      
    }
}
