using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Extensions;

namespace WPF_MVVM.Infrastructure.Behaviors
{
    internal class WindowSystemIconBehavior : Behavior<Image>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;
            e.Handled = true;

            if (e.ClickCount > 1) window.Close();
            else
            {
                window.SendMessage(WM.SYSCOMMAND, SC.KEYMENU);
            }
        }

    }
}
