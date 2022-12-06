using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    
    public partial class UpdatePlayerViewModel : BaseViewModel
    {
        IPlayerService playerService;
        public ObservableRangeCollection<Players> PlayerNames { get; set; }
        public UpdatePlayerViewModel()
        {
            playerService = DependencyService.Get<IPlayerService>();
            
            var playerList = playerService.GetAllPlayersAsync();
            
    
        }
    }
}
