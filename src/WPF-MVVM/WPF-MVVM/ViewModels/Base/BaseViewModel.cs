using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WPF_MVVM.ViewModels.Base
{
    internal abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if(Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName); return true;
        }

        ~BaseViewModel() => Dispose(false); 

        public void Dispose() => Dispose(true);
        

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed) return;
            _disposed = true;
            // освобождение управляемых ресурсов
        }
    }
}
