using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    internal partial class WeekStatsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Weeks> WeekStats { get; set; }
        
        private bool _isBusy;
        
        readonly IWeekService weekService;
        public WeekStatsViewModel()
        {
            Title = "Week Statistics";
            WeekStats = new ObservableRangeCollection<Weeks>();
            weekService = new WeekService();
        }
        public new bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        [RelayCommand]
        async Task Refresh()
        {
            //Get Week Stats
            WeekStats.Clear();
            var weeks = await weekService.GetAllWeeksAsync();
            weeks = weeks.OrderByDescending(a => a.Id);
            WeekStats.Add((Weeks)weeks.FirstOrDefault());
            IsBusy = false;
        }
    }
}
