using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace WPF_MVVM.Infrastructure.Behaviors
{
    internal class WindowTitleBarBehavior : Behavior<UIElement>
    {
        private Window _window;


        protected override void OnAttached()
        {
            _window = AssociatedObject as Window ?? AssociatedObject.FindLogicalParent<Window>();
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;

        }


        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1) return;
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;
            window?.DragMove();

        }
    }
}
    

