using CommunityToolkit.Maui;
using XWPLStats.Services;
using XWPLStats.ViewModels;
using XWPLStats.Views;

namespace XWPLStats;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>().UseMauiCommunityToolkit();

		builder.Services.AddSingleton<IRestService, RestService>();
		builder.Services.AddSingleton<PlayerHelpers, PlayerHelpers>();

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();


        builder.Services.AddSingleton<AddPlayer>();
		builder.Services.AddSingleton<AddUpdatePlayerViewModel>();
        builder.Services.AddSingleton<EditPlayer>();
        builder.Services.AddSingleton<EditPlayerViewModel>();
        builder.Services.AddSingleton<PlayerDetailPage>();
		builder.Services.AddSingleton<PlayerDetailPageViewModel>();
		builder.Services.AddSingleton<TeamStats>();
		builder.Services.AddSingleton<TeamStatsViewModel>();
		builder.Services.AddSingleton<WeekStatsPage>();
		builder.Services.AddSingleton<WeekStatsViewModel>();
		builder.Services.AddSingleton<WeekViewer>();
		builder.Services.AddSingleton<WeekViewerViewModel>();

        return builder.Build();
	}
}
