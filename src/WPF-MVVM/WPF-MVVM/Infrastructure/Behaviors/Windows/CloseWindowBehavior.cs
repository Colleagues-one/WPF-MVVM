using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace WPF_MVVM.Infrastructure.Behaviors.Windows
{
    internal class CloseWindowBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClickButton;

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClickButton;
        }

        private void OnClickButton(object sender, RoutedEventArgs e) =>
            (AssociatedObject.FindVisualRoot() as Window)?.Close();
    }
}
