using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_Test
{
    public class Model
    {
        private readonly ObservableCollection<int> _myValues = new ObservableCollection<int>();

        public readonly ReadOnlyObservableCollection<int> MyPublicValues;

        public Model()
        {
            MyPublicValues = new ReadOnlyObservableCollection<int>(_myValues);
        }
        public void AddValue(int value)
        {
            _myValues.Add(value);
        }
        public void RemoveValue(int index)
        {
            if (index >= 0 && index < _myValues.Count) _myValues.RemoveAt(index);
        }
        public int Sum => MyPublicValues.Sum(); 



    }
}
