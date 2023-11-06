﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM.ViewModels.Base
{
    internal abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? PropertyName = null)
        {
            if(Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName); return true;
        }

        ~BaseViewModel() => Dispose(false); 

        public void Dispose() => Dispose(true);
        

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || disposed) return;
            disposed = true;
            // освобождение управляемых ресурсов
        }
    }
}
