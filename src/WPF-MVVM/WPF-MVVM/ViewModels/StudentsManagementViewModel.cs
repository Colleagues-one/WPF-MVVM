using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class StudentsManagementViewModel : BaseViewModel
    {
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
    }
}
