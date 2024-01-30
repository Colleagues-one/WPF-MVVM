using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace WPF_MVVM_Test2.Behaviors
{
    internal class CloseWindow : Behavior<Button>
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
            var button = AssociatedObject;

            var root = FindVisualRoot(button) as Window;
            root?.Close();

        }

        private static DependencyObject FindVisualRoot(DependencyObject obj)
        {
            do
            {
                var parent = VisualTreeHelper.GetParent(obj);
                if (parent is null) return obj;
                obj = parent;
            } while (true);
        }
    }
}
