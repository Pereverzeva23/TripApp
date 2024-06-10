using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TripApp.Models;
using TripApp.ViewModels;
namespace TripApp;

    public partial class SegmentPageKirovka : ContentPage
    {
       public SegmentPageKirovka() { InitializeComponent(); }
        private void InitializeComponent()
        {
            global::Microsoft.Maui.Controls.Xaml.Extensions.LoadFromXaml(this, typeof(SegmentPageKirovka));
        }
}
