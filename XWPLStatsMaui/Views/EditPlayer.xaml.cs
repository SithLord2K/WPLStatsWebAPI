using MvvmHelpers;
using System.Collections.ObjectModel;
using XWPLStats.Models;
using XWPLStats.Services;
using XWPLStats.ViewModels;

namespace XWPLStats.Views;

public partial class EditPlayer : ContentPage
{
    public EditPlayer(EditPlayerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

	}

    protected override void OnAppearing()
    {
        EditPlayerViewModel viewModel = (EditPlayerViewModel)BindingContext;
        viewModel.RefreshCommand.Execute(null);
    }

}