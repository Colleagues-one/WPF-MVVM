using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WPF_MVVM_Test.Base;

namespace WPF_MVVM_Test
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Model _model;

        public event PropertyChangedEventHandler PropertyChanged;

      

        public ViewModel()
        {
            _model = new Model();
           
        }

        

      
    }
}
