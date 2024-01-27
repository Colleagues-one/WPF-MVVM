using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;
using OxyPlot;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.Models;
using WPF_MVVM.Models.Decanat;
using WPF_MVVM.Services.Interfaces;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel : BaseViewModel
    {

        
        public CountriesStatisticViewModel CountriesStatisticViewModel { get; }

        public readonly IAsyncDataService AsyncData;

        public WebServerViewModel WebServer { get; }

        //------------------------------------------------------------------------------------------------------------

        #region SelectedPageIndex :int - Номер выбранной вкладки в статус баре
        /// <summary>
        /// SelectedPageIndex :int - Номер выбранной вкладки
        /// </summary>
        private int _selectedPageIndex = 0;
        /// <summary>
        /// SelectedPageIndex :int - Номер выбранной вкладки
        /// </summary>
        public int SelectedPageIndex
        {
            get => _selectedPageIndex; set => Set(ref _selectedPageIndex, value);
        }

        #endregion

        #region TestDataPoints :IEnumerable<DataPoint> - Тестовый график
        /// <summary>
        /// Тестовый набор дданных визуализации данных
        /// </summary>
        private IEnumerable<DatePoint> _testDataPoints;
        /// <summary>
        /// Тестовый набор дданных визуализации данных
        /// </summary>
        public IEnumerable<DatePoint> TestDatePoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        } 
        #endregion

        #region Status Статус окна
        private string _status = "Status";
        /// <summary> статус программы </summary>
        public string Status { get => _status; set => Set(ref _status, value); }
        #endregion

        #region Coefficient : double - коэффициент
        /// <summary>
        /// field коэффициент
        /// </summary>
        private double _Coefficient = 1;

        /// <summary>
        ///  attribute коэффициент
        /// </summary>
        public double Coefficient { get => _Coefficient; set => Set(ref _Coefficient, value); }
        #endregion

        #region FuelCount : double - Test of Gauge Indicator Value

        /// <summary>
        /// field Test of Gauge Indicator Value
        /// </summary>
        private double _FuelCount;

        /// <summary>
        ///  attribute Test of Gauge Indicator Value
        /// </summary>
        public double FuelCount
        {
            get => _FuelCount;
            set => Set(ref _FuelCount, value);
        }

        #endregion

        #region DataValue : string - Результат длительной ассинхронной операции

        /// <summary>
        /// field Результат длительной ассинхронной операции
        /// </summary>
        private string _DataValue;

        /// <summary>
        ///  attribute Результат длительной ассинхронной операции
        /// </summary>
        public string DataValue
        {
            get => _DataValue;
            private set => Set(ref _DataValue,value);
        }

        #endregion

        #region DecanatTEST


        public ObservableCollection<Group> Groups { get; }

        #region SelectedGroup Выбранная группа Тест
        /// <summary>
        /// выбранная группа
        /// </summary>
        private Group _selectedGroup;

        /// <summary>
        /// выбранная группа
        /// </summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (!Set(ref _selectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }
        #endregion


        #region StudentFilterText : string - Текст фильтра студентов

        /// <summary>
        /// field Текст фильтра студентов
        /// </summary>
        private string _StudentFilterText;

        /// <summary>
        ///  attribute Текст фильтра студентов
        /// </summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View?.Refresh();
            }
        }

        #endregion

        #region GroupFilterText : string - Текст фильтра групп

        /// <summary>
        /// field Текст фильтра групп
        /// </summary>
        private string _GroupFilterText;

        /// <summary>
        ///  attribute Текст фильтра групп
        /// </summary>
        public string GroupFilterText
        {
            get => _GroupFilterText;
            set
            {
                if (!Set(ref _GroupFilterText, value)) return;
                _CollectionViewGroups.View?.Refresh();
            }
        }

        #endregion

        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        private readonly CollectionViewSource _CollectionViewGroups = new CollectionViewSource();



        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;

        public ICollectionView ViewGroups => _CollectionViewGroups?.View;

        private void OnStudentFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }

            var filter = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter)) return;


            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Surname.Contains(filter, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(filter, StringComparison.OrdinalIgnoreCase)) return;
            e.Accepted = false;
        }

        private void OnGroupsFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group))
            {
                e.Accepted = false;
                return;
            }
            var filter = _GroupFilterText;
            if (string.IsNullOrWhiteSpace(filter)) return;

            if (group.Name is null)
            {
                e.Accepted = false;
                return;
            }
            if (group.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)) return;
            OnPropertyChanged(nameof(ViewGroups));
            e.Accepted = false;

        }

        #endregion

        #region Commands 

        #region CloseAppCommand
        public CloseApplicationCommand CloseApplicationCommand { get; }

        #endregion

        #region ICommand CreateNewGroup - Creating new group test
        /// <summary>
        /// Creating new group test
        /// </summary>
        public ICommand CreateNewGroupCommand { get; }

        private bool CanCreateNewGroupCommandExecute(object parameter) => true;

        private void OnCreateNewGroupCommandExecuted(object parameter)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion       

        #region ICommand DeleteGroup - Deleting group
        /// <summary>
        /// Deleting group
        /// </summary>
        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecute(object parameter) => parameter is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object parameter)
        {
            if (!(parameter is Group group)) return;
            int group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
            {
                SelectedGroup = Groups[group_index];
            }
        }
        #endregion

        #region ICommand StartProcess (object) - Запуск процесса

        /// <summary>
        /// Запуск процесса
        /// </summary>
        public ICommand StartProcessCommand { get; }

        private bool CanStartProcessCommandExecute(object parameter) => true;

        private void OnStartProcessCommandExecuted(object parameter)
        {
            new Thread(ComputeValue).Start();
        }

        private void ComputeValue()
        {
            DataValue = AsyncData.GetResult(DateTime.Now);
        }

        #endregion

        #region ICommand StopProcess (object) - Остановка процесса

        /// <summary>
        /// Остановка процесса
        /// </summary>
        public ICommand StopProcessCommand { get; }

        private bool CanStopProcessCommandExecute(object parameter) => true;

        private void OnStopProcessCommandExecuted(object parameter)
        {

        }

        #endregion

        #endregion


        public MainWindowViewModel(CountriesStatisticViewModel Statistic, IAsyncDataService asyncData, WebServerViewModel webServer)
        {
            this.WebServer = webServer;
            AsyncData = asyncData;

            CountriesStatisticViewModel = Statistic;//инверсированная зависимость
            Statistic.MainWindowViewModel = this;

            /*CountriesStatisticViewModel = App.Host.Services.GetRequiredService<CountriesStatisticViewModel>(); антипаттерн */
          //  CountriesStatisticViewModel = new CountriesStatisticViewModel(this); обычная зависимость

            #region Commands
            CloseApplicationCommand = new CloseApplicationCommand();
            CreateNewGroupCommand = new RelayCommand(OnCreateNewGroupCommandExecuted, CanCreateNewGroupCommandExecute);
            DeleteGroupCommand = new RelayCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);
            StartProcessCommand = new RelayCommand(OnStartProcessCommandExecuted, CanStartProcessCommandExecute);
            StopProcessCommand = new RelayCommand(OnStopProcessCommandExecuted, CanStopProcessCommandExecute);

            #endregion

            #region Graf
            var data_Points = new List<DatePoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                data_Points.Add((new DatePoint { XValue = x, YValue = y }));
            }

            TestDatePoints = data_Points;
            #endregion

            #region Decanat

            int student_index = 1;
            Groups = new ObservableCollection<Group>(Enumerable.Range(1,20).Select(i=> new Group
                {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(Enumerable.Range(1,10).Select(s=>new Student
                {
                    Name = $"Name {student_index}",
                    Surname = $"Surname {student_index}",
                    Patronymic = $"Patronymic {student_index++}",
                    Birthday = DateTime.Now,
                    Rating = 0,
                    Description = $"This is a Student from group{i}"
                    
                })),
                Description = $"This is a group{i}"
                }));

            _CollectionViewGroups.Source = Groups;
            _SelectedGroupStudents.Filter += OnStudentFiltered;
            _CollectionViewGroups.Filter += OnGroupsFiltered;

            #endregion
        }

        
    }
}
