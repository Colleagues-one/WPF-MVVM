using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public StudentsManagementViewModel(StudentsManager studentsManager) =>
            _StudentsManager = studentsManager;
    }
}
