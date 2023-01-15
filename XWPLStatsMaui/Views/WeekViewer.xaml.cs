using XWPLStats.ViewModels;

namespace XWPLStats.Views;

public partial class WeekViewer : ContentPage
{
	public WeekViewer(WeekViewerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (WeekViewerViewModel)BindingContext;
        await vm.RefreshCommand.ExecuteAsync(null);
    }
}