using System.Windows;
using System;
using WPF_MVVM.Models.Decanat;
using WPF_MVVM.Services.Interfaces;
using WPF_MVVM.Views;

namespace WPF_MVVM.Services
{
    internal class WindowsUserDialogService : IUserDialogService
    {
        public bool Edit(object item)
        {
            if(item == null) throw new ArgumentNullException(nameof(item));
            switch (item)
            {
                case Student student:
                    return EditStudent(student);
                default:
                    throw new NotSupportedException(
                        $"Редактирование объекта типа {item.GetType().Name} не поддерживается!");
            }

        }

        private static bool EditStudent(Student student)
        {
            var dlg = new StudentEditorWindow()
            {
                FirstName = student.Name,
                LastName = student.Surname,
                Patronymic = student.Patronymic,
                Birthday = student.Birthday,
                Rating = student.Rating,
                Description = student.Description,
                Owner = Application.Current.Windows.OfType<StudentsManagementWindow>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            if (dlg.ShowDialog() != true) return false;
            
            student.Name = dlg.FirstName;
            student.Surname = dlg.LastName;
            student.Patronymic = dlg.Patronymic;
            student.Birthday = dlg.Birthday;
            student.Rating = dlg.Rating;
            student.Description = dlg.Description;
            
            return true;
        }
        

        public void ShowInformation(string information, string caption) =>
            MessageBox.Show(
                information,
                caption,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        public void ShowWarning(string message, string caption) =>
            MessageBox.Show(
                message,
                caption,
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

        public void ShowError(string message, string caption)=>
        MessageBox.Show(
            message,
            caption,
            MessageBoxButton.OK,
            MessageBoxImage.Error);

        public bool Confirm(string message, string caption, bool exclamation = false) =>
            MessageBox.Show(
                message,
                caption,
                MessageBoxButton.YesNo,
                exclamation
                    ? MessageBoxImage.Exclamation
                    : MessageBoxImage.Question) == MessageBoxResult.Yes;
        
    }
}
