using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace WPF_MVVM.Infrastructure.Behaviors.Window
{
    internal class WindowStateChange : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClickButton;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClickButton;
        }

        private void OnClickButton(object sender, RoutedEventArgs e)
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;
            window.WindowState = window.WindowState switch
            {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => window.WindowState
            };
        }
    }
}
