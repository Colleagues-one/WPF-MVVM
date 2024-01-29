using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace WPF_MVVM.Infrastructure.Behaviors
{
    internal class WindowTitleBarBehavior : Behavior<UIElement>
    {
        private Window _window;


        protected override void OnAttached()
        {
            _window = AssociatedObject as Window ?? AssociatedObject.Find;
        }

        protected override void OnDetaching()
        {

        }
    }
    
}
