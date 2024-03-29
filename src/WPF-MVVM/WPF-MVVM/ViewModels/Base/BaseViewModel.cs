﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;


namespace WPF_MVVM.ViewModels.Base
{
    internal abstract class BaseViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            var handlers = PropertyChanged;
            if (handlers is null) return;
            var invocationList = handlers.GetInvocationList();
            var arg = new PropertyChangedEventArgs(propertyName);
            foreach (var action in invocationList)
            {
                if (action.Target is DispatcherObject dispObject)
                {
                    dispObject.Dispatcher.Invoke(action, this, arg);
                }
                else action.DynamicInvoke(this, arg);
            }
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if(Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName); return true;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var value_target_service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var root_object_service = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            

            OnInitialized(value_target_service?.TargetObject, value_target_service?.TargetProperty, root_object_service?.RootObject);

            return this;
        }

        #region Расширение разметки для базовой модели
        private WeakReference _targetRef;
        private WeakReference _rootRef;

        public object TargetObject => _targetRef?.Target;
        public object RootObject => _rootRef?.Target;

        protected virtual void OnInitialized(object target, object property, object root)
        {
            _targetRef = new WeakReference(target);
            _rootRef = new WeakReference(root);

        } 
        #endregion

        ~BaseViewModel() => Dispose(false); 

        public void Dispose() => Dispose(true);
        

        private bool _disposed;

        // освобождение управляемых ресурсов
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed) return;
            _disposed = true;
        }
    }
}
