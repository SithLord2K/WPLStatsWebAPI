using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    public partial class TeamStatsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<TeamStats> TeamStat { get; set; }

        readonly PlayerHelpers playerHelpers;
        
        public TeamStatsViewModel()
        {
            TeamStat = new ObservableRangeCollection<TeamStats>();
            playerHelpers= new PlayerHelpers();
        }

        [RelayCommand]
        async Task Refresh()
        {
            IsBusy = true;
            TeamStat.Clear();
            TeamStats newTeam = await playerHelpers.GetTeamStats();
            TeamStat.Add(newTeam);
            IsBusy = false;
        }
    }
}
