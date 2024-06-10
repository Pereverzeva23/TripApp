using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TripApp.Models;
using TripApp.ViewModels;
namespace TripApp;

public partial class ViewPage : ContentPage
{
	public ViewPage()
	{
		InitializeComponent();
	}

    private async void ImageButton_Clicked1(object sender, EventArgs e)
    {
       // Routing.RegisterRoute("SegmentPageKirovka", typeof(SegmentPageKirovka));
        //ViewPage = new AppShell();
        await Shell.Current.GoToAsync("//SegmentPageKirovka");
    }
}