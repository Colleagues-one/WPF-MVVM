using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_MVVM.Infrastructure.Commands.Base;
using WPF_MVVM.Views;

namespace WPF_MVVM.Infrastructure.Commands
{
    internal class ManageStudentsCommand : Command
    {
        private StudentsManagementWindow _window;

        public override bool CanExecute(object parameter) => _window == null;

        public override void Execute(object parameter)
        {
            var window = new StudentsManagementWindow { Owner = Application.Current.MainWindow };
            _window = window;
            window.Closed += OnWindowClosed;
            window.ShowDialog();

        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= OnWindowClosed;
            _window = null;
        }
    }
}
