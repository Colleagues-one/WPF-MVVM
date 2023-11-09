using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM.Infrastructure.Commands;
using WPF_MVVM.Models;
using WPF_MVVM.Models.Decanat;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    
    internal class MainWindowViewModel : BaseViewModel
    {

        
        #region SelectedPageIndex :int - Номер выбранной вкладки
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

        #region TestDataPoints :IEnumerable<DataPoint> - Тестовый
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

        #region Title Заголовок окна
        private string _title = "MainWindow";

        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _title;
           /* set сеттер если бы бы базовый класс не имел виртуального метода Set
            {
                if (Equals(value, _title)) return;
                _title = value;
                OnPropertyChanged();
                Set(ref _title, value);
            }*/ 
            set => Set(ref _title, value);
        }
        #endregion

        #region Status Статус окна
        private string _status = "Status";
        /// <summary> статус программы </summary>
        public string Status { get => _status; set => Set(ref _status, value); }
        #endregion

        #region Commands 

        #region CloseAppCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object sender) => true;

        private void OnCloseApplicationCommandExecuted(object sender)
        {
            Application.Current.Shutdown();
        }
        #endregion

        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object sender)
        {
            int number = Convert.ToInt32(sender);
            if(number <= 0 && _selectedPageIndex >= 1 || number >= 0 && _selectedPageIndex < 1)
                return true;
            return false;
            
        } 

        private void OnChangeTabIndexCommandExecuted(object sender)
        {
            if((sender is null)) return;
            SelectedPageIndex += Convert.ToInt32(sender);
        }
        #endregion

        
        
        #region DecanatTEST
        /// <summary>
        /// 
        /// </summary>
        public object[] CompositeCollection { get; }

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
            set => Set(ref _selectedGroup, value);
        }
        #endregion


        #endregion





        #region SelectedCompositeValue : object - Выбранный непонятный элемент
        /// <summary>
        /// field Выбранный непонятный элемент
        /// </summary>
        private object _selectedCompositeValue;

        /// <summary>
        ///  attribute Выбранный непонятный элемент
        /// </summary>
        public object SelectedCompositeValue { get => _selectedCompositeValue; set =>Set(ref _selectedCompositeValue, value); }
        #endregion




        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand =
                new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            ChangeTabIndexCommand = new RelayCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

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
            
            
            Groups = new ObservableCollection<Group>(Enumerable.Range(1,20).Select(i=> new Group
                {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(Enumerable.Range(1,10).Select(s=>new Student
                {
                    Name = $"Name{i}.{s}",
                    Surname = $"Surname{i}.{s}",
                    Patronymic = $"Patronymic{i}.{s}",
                    Birthday = DateTime.Now,
                    Rating = 0,
                    Description = $"This is a Student from group{i}"
                })),
                Description = $"This is a group{i}"
                }));

            
            var datalist = new List<object>();
            datalist.Add("iffdk");
                datalist.Add(49);
            datalist.Add(Groups[1]);
            datalist.Add(Groups[1].Students[3]);

            CompositeCollection = datalist.ToArray();
            #endregion
        }
    }
}
