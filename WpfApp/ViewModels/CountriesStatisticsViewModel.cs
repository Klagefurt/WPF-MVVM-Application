using System.Windows;
using System.Windows.Input;
using WpfApp.Infrastructure.Commands;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
    internal class CountriesStatisticsViewModel : ViewModel
    {
        private MainWindowViewModel MainWindowViewModel { get; }
        private DataService _DataService;

        #region Countries : IEnumerable<CountryInfo> - country statistics

        private IEnumerable<CountryInfo> _Countries;
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries; 
            private set => Set(ref _Countries, value);
        }

        #endregion

        #region Commands

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object sender)
        {
            Countries = _DataService.GetData();
        }

        #endregion

        /// <summary>
        /// Constructor for debugging, do not start in release mode!
        /// </summary>
        public CountriesStatisticsViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("It is not debug mode!");

            _Countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
            {
                Name = $"Country {i}",
                ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                {
                    Name = $"Province {i}",
                    Location = new Point(i, j),
                    InfectedCounts = Enumerable.Range(1, 10).Select(k => new InfectedCount
                    {
                        Date = DateTime.Now,
                        Count = k
                    }).ToArray()
                }).ToArray()
            }).ToArray();
        }

        public CountriesStatisticsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            _DataService = new DataService();

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

        }
    }
}
