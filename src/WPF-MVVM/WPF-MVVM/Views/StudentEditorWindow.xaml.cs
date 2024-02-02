using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_MVVM.ViewModels;

namespace WPF_MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для StudentEditorWindow.xaml
    /// </summary>
    public partial class StudentEditorWindow 
    {
        #region FirstName Dependency Property : string - Имя

        /// <summary>
        /// Имя Property Register
        /// </summary>
        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register(
                nameof(FirstName),
                typeof(string),
                typeof(StudentEditorWindow),
                new PropertyMetadata(default(string)));

        /// <summary>
        /// Имя Property 
        /// </summary>
        //[Category("")]
        [Description("Имя")]
        public string FirstName { get => (string)GetValue(FirstNameProperty); set => SetValue(FirstNameProperty, value); }

        #endregion

        #region LastName Dependency Property : string - Фамилия

        /// <summary>
        /// Фамилия Property Register
        /// </summary>
        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register(
                nameof(LastName),
                typeof(string),
                typeof(StudentEditorWindow),
                new PropertyMetadata(default(string)));


        /// <summary>
        /// Фамилия Property 
        /// </summary>
        //[Category("")]
        [Description("Фамилия")]
        public string LastName { get=>(string) GetValue(LastNameProperty); set => SetValue(LastNameProperty, value); }

        #endregion

        #region Patronymic Dependency Property : string - Отчество

        /// <summary>
        /// Отчество Property Register
        /// </summary>
        public static readonly DependencyProperty PatronymicProperty =
            DependencyProperty.Register(
                nameof(Patronymic),
                typeof(string),
                typeof(StudentEditorWindow),
                new PropertyMetadata(default(string)));


        /// <summary>
        /// Отчество Property 
        /// </summary>
//[Category("")]
        [Description("Отчество")]
        public string Patronymic { get => (string)GetValue(PatronymicProperty); set => SetValue(PatronymicProperty, value); }

        #endregion

        #region Rating Dependency Property : double - Рейтинг

        /// <summary>
        /// Рейтинг Property Register
        /// </summary>
        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register(
                nameof(Rating),
                typeof(double),
                typeof(StudentEditorWindow),
                new PropertyMetadata(default(double)));


        /// <summary>
        /// Рейтинг Property 
        /// </summary>
//[Category("")]
        [Description("Рейтинг")]
        public double Rating { get => (double)GetValue(RatingProperty); set => SetValue(RatingProperty, value); }
        #endregion

        #region Birthday Dependency Property : DateTime - Дата рождения

        /// <summary>
        /// Дата рождения Property Register
        /// </summary>
        public static readonly DependencyProperty BirthdayProperty =
            DependencyProperty.Register(
                nameof(Birthday),
                typeof(DateTime),
                typeof(StudentEditorWindow),
                new PropertyMetadata(default(DateTime)));


        /// <summary>
        /// Дата рождения Property 
        /// </summary>
//[Category("")]
        [Description("Дата рождения")]
        public DateTime Birthday { get => (DateTime)GetValue(BirthdayProperty); set => SetValue(BirthdayProperty, value); }

        #endregion

        #region Description Dependency Property : string - Описание

        /// <summary>
        /// Описание Property Register
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                nameof(Description),
                typeof(string),
                typeof(StudentEditorWindow),
                new PropertyMetadata(default(string),(e,d) => {}));


        /// <summary>
        /// Описание Property 
        /// </summary>
//[Category("")]
        [Description("Описание")]
        public string Description { get => (string)GetValue(DescriptionProperty); set => SetValue(DescriptionProperty, value); }

        #endregion

        public StudentEditorWindow()
        {
            InitializeComponent();
        }
    }
}
