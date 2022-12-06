
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

		builder.Services.AddSingleton<IPlayerService, PlayerService>();
		builder.Services.AddSingleton<IWeekService, WeekService>();
		builder.Services.AddSingleton<PlayerHelpers, PlayerHelpers>();

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddTransient<AddUpdatePlayer>();
		builder.Services.AddTransient<AddUpdatePlayerViewModel>();
		builder.Services.AddTransient<PlayerDetailPage>();
		builder.Services.AddTransient<PlayerDetailPageViewModel>();
		builder.Services.AddTransient<TeamStats>();
		builder.Services.AddTransient<TeamStatsViewModel>();
		builder.Services.AddTransient<WeekStatsPage>();
		builder.Services.AddTransient<WeekStatsViewModel>();

        return builder.Build();
	}
}
