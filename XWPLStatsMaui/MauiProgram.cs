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

		builder.Services.AddScoped<IRestService, RestService>();
		builder.Services.AddScoped<PlayerHelpers, PlayerHelpers>();

		builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();


        builder.Services.AddTransient<AddPlayer>();
		builder.Services.AddTransient<AddUpdatePlayerViewModel>();
        builder.Services.AddTransient<EditPlayer>();
        builder.Services.AddTransient<EditPlayerViewModel>();
        builder.Services.AddTransient<PlayerDetailPage>();
		builder.Services.AddTransient<PlayerDetailPageViewModel>();
		builder.Services.AddTransient<TeamStats>();
		builder.Services.AddTransient<TeamStatsViewModel>();
		builder.Services.AddTransient<WeekStatsPage>();
		builder.Services.AddTransient<WeekStatsViewModel>();
		builder.Services.AddTransient<WeekViewer>();
		builder.Services.AddTransient<WeekViewerViewModel>();
        builder.Services.AddTransient<AddPlayer>();
		builder.Services.AddScoped<AddUpdatePlayerViewModel>();
        builder.Services.AddTransient<EditPlayer>();
        builder.Services.AddScoped<EditPlayerViewModel>();
        builder.Services.AddTransient<PlayerDetailPage>();
		builder.Services.AddScoped<PlayerDetailPageViewModel>();
		builder.Services.AddTransient<TeamStats>();
		builder.Services.AddScoped<TeamStatsViewModel>();
		builder.Services.AddTransient<WeekStatsPage>();
		builder.Services.AddScoped<WeekStatsViewModel>();
		builder.Services.AddTransient<WeekViewer>();
		builder.Services.AddScoped<WeekViewerViewModel>();

        return builder.Build();
	}
}
