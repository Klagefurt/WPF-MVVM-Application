using OxyPlot;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp.Infrastructure.Commands;
using WpfApp.Models;
using WpfApp.Models.University;
using WpfApp.ViewModels.Base;
using DataPoint = WpfApp.Models.DataPoint;

namespace WpfApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        /*--------------------------------------------------------------*/

        #region Groups
        /// <summary>Группы студентов</summary>
        public ObservableCollection<Group> Groups { get; }
        #endregion

        #region CompositeCollection
        public object[] CompositeCollection { get; }
        #endregion

        #region SelectedCompositeValue
        private object _selectedCompositValue;

        public object SelectedCompositeValue
        {
            get => _selectedCompositValue;
            set => Set(ref _selectedCompositValue, value);
        }
        #endregion

        #region SelectedGroup
        /// <summary>Выбранная группа</summary>
        private Group _selectedGroup;
        /// <summary>Выбранная группа</summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (!Set(ref _selectedGroup, value)) return;
                _selectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }
        #endregion

        #region StudentFilterText : string - Student filter text

        private string _studentFilterText;
        public string StudentFilterText
        {
            get => _studentFilterText;
            set
            {
                if (!Set(ref _studentFilterText, value)) return;
                _selectedGroupStudents.View.Refresh();
            }
        }
        #endregion

        #region SelectedGroupStudents
        private readonly CollectionViewSource _selectedGroupStudents = new();
        private void OnStudentsFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }
            if (string.IsNullOrWhiteSpace(_studentFilterText))
                return;
            if (student.Name == null || student.Surname == null || student.Patronymic == null)
            {
                e.Accepted = false;
                return;
            }
            if (student.Name.Contains(_studentFilterText, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Surname.Contains(_studentFilterText, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(_studentFilterText, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }
        public ICollectionView SelectedGroupStudents => _selectedGroupStudents?.View;
        #endregion

        #region SelectedPageIndex
        /// <summary>Выбранный индекс вкладки</summary>
        private int _selectedPageIndex;
        /// <summary>Выбранный индекс вкладки</summary>
        public int SelectedPageIndex
        {
            get => _selectedPageIndex;
            set => Set(ref _selectedPageIndex, value);
        }
        #endregion

        #region TestDataPoints
        /// <summary>Тестовый набор данных для визулизации графиков</summary>
        private IEnumerable<DataPoint> _testDataPoints;
        /// <summary>Тестовый набор данных для визулизации графиков</summary>
        public IEnumerable<DataPoint> TestDatapoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }
        #endregion

        #region Title
        /// <summary>Заголовок окна</summary>
        private string _title;

		/// <summary>Заголовок окна</summary>
		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}
        #endregion

        #region Status
        /// <summary>Статус программмы</summary>
        private string _status;

        /// <summary>Статус программмы</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }
        #endregion

        /*--------------------------------------------------------------*/

        #region CreateGroupCommand
        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecuted(object p)
        {
            var max_count = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Group {max_count}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion

        #region DeleteGroupCommand
        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecute(object parameter) => parameter is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object parameter)
        {
            if (!(parameter is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index > 0) 
                SelectedGroup = Groups[group_index - 1];
        }
        #endregion

        #region CloseAppCommand
        public ICommand CloseAppCommand { get; }

        private bool CanCloseAppCommandExecute(object parameter) => true;

        private void OnAppCloseCommandExecuted(object parameter)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object parameter) => _selectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object parameter)
        {
            var mw = (MainWindow)Application.Current.MainWindow;
            var tabControlCount = mw.GetTabControlCount();

            if ( parameter is null || 
                (_selectedPageIndex == 0 && Convert.ToInt32(parameter) == -1) ||
                (_selectedPageIndex == tabControlCount - 1 && Convert.ToInt32(parameter) == 1)) return;
            SelectedPageIndex += Convert.ToInt32(parameter);
        }
        #endregion

        /*--------------------------------------------------------------*/

        public IEnumerable<Student> students_test => 
            Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
            .Select(i => new Student
            {
                Name = $"Name {i}",
                Surname = $"Surname {i}"
            });

        public MainWindowViewModel()
        {
            Title = "New Application";
            Status = "Ready!";

            CloseAppCommand = new LambdaCommand(OnAppCloseCommandExecuted, CanCloseAppCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);

            var dataPoints = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double toRad = Math.PI / 180;
                var y = Math.Sin(x * toRad);

                dataPoints.Add(new DataPoint { XValue = x, YValue = y });
            }
            TestDatapoints = dataPoints;

            var student_index = 1;
            var students = Enumerable.Range(1, 15).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group{
                Name = $"Group {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

            var data_list = new List<object>
            {
                "Some text",
                43,
                Groups[1],
                Groups[1].Students[1]
            };

            CompositeCollection = data_list.ToArray();

            _selectedGroupStudents.Filter += OnStudentsFiltered;

            //_selectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));

            //_selectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        }
    }
}
