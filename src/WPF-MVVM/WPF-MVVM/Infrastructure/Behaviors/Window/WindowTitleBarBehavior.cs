using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace WPF_MVVM.Infrastructure.Behaviors.Window
{
    internal class WindowTitleBarBehavior : Behavior<UIElement>
    {
        private Window _window;


        protected override void OnAttached()
        {
            _window = AssociatedObject as Window ?? AssociatedObject.FindVisualParent<Window>();
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;

        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
                DragMove();
            else
                Maximize();
        }

        private void DragMove()
        {
            _window?.DragMove();
        }

        private void Maximize()
        {
            _window.WindowState = _window.WindowState switch
            {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => _window.WindowState
            };
        }
    }
}


