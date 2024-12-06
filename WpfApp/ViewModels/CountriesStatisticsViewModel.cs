using System.Security.AccessControl;
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

        private IEnumerable<CountryInfo> _countries;
        public IEnumerable<CountryInfo> Countries
        {
            get => _countries; 
            private set => Set(ref _countries, value);
        }

        #endregion

        #region Commands

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object sender)
        {
            Countries = _DataService.GetData();
        }

        #endregion


        public CountriesStatisticsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            _DataService = new DataService();

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);
        }
    }
}
