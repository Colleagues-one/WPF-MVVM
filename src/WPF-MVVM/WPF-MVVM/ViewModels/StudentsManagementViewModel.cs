using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.Models.Decanat;
using WPF_MVVM.Services.Students;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class StudentsManagementViewModel : BaseViewModel
    {
        private readonly StudentsManager _StudentsManager;

        public IEnumerable<Student> Students => _StudentsManager.Students;
        public IEnumerable<Group> Groups => _StudentsManager.Groups;



        #region Title : string - заголовок окна

        /// <summary>
        /// field заголовок окна
        /// </summary>
        private string _Title = "Управление студентами";

        /// <summary>
        ///  attribute заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region SelectedGroup : Group - Выбранная группа студентов
        
        /// <summary>
        /// field Выбранная группа студентов
        /// </summary>
        private Group _SelectedGroup;

        /// <summary>
        ///  attribute Выбранная группа студентов
        /// </summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }

        #endregion

        #region SelectedStudent : Student - Выбранный студент

        /// <summary>
        /// field Выбранный студент
        /// </summary>
        private Student _SelectedStudent;

        /// <summary>
        ///  attribute Выбранный студент
        /// </summary>
        public Student SelectedStudent
        {
            get => _SelectedStudent;
            set => Set(ref _SelectedStudent, value);
        }

        #endregion


        #region Commands

        #region ICommand EditStudent (object) - Редактирование студента

        private ICommand _EditStudentCommand;
        /// <summary>
        /// Редактирование студента
        /// </summary>
        public ICommand EditStudentCommand =>
            _EditStudentCommand ??= new RelayCommand(OnEditStudentCommandExecuted, CanEditStudentCommandExecute);

        private static bool CanEditStudentCommandExecute(object parameter) => parameter is Student;

        private void OnEditStudentCommandExecuted(object parameter)
        {   
            var student = (Student)parameter;

        }

        #endregion

        #region ICommand AddStudent (object) - Добавление студента

        private ICommand _AddStudentCommand;

        /// <summary>
        /// Добавление студента
        /// </summary>
        public ICommand AddStudentCommand =>
            _AddStudentCommand ??= new RelayCommand(OnAddStudentCommandExecuted, CanAddStudentCommandExecute);

        private static bool CanAddStudentCommandExecute(object parameter) => parameter is Group;

        private void OnAddStudentCommandExecuted(object parameter)
        {
            var group = (Group)parameter;
        }

        #endregion

        #endregion


        public StudentsManagementViewModel(StudentsManager studentsManager) =>
            _StudentsManager = studentsManager;
    }
}
