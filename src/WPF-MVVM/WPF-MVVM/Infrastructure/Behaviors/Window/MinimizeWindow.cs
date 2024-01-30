using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WPF_MVVM.Infrastructure.Behaviors.Window
{
    internal class MinimizeWindow : Behavior<Button>
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
            window.WindowState = WindowState.Minimized;
        }
    }
}
