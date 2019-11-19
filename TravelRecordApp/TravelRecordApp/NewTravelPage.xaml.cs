using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFLoadingPageService;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            DependencyService.Get<ILoadingPageService>().InitLoadingPage(new LoadingIndicatorPage2());
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            
            var (venues, message) = await Venue.GetVenues(position.Latitude, position.Longitude);

            DependencyService.Get<ILoadingPageService>().HideLoadingPage();

            if (venues.Count < 1)
            {
                await DisplayAlert("Error", message, "Ok");
            }

            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = venueListView.SelectedItem as Venue;

                var firstCategory = selectedItem.categories.FirstOrDefault();

                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedItem.location.address,
                    Distance = selectedItem.location.distance.ToString(),
                    Latitude = selectedItem.location.lat,
                    Longitude = selectedItem.location.lng,
                    VenueName = selectedItem.name,
                    UserId = App.user.id,
                };

                /*
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.Table<Post>();
                    int rows = conn.Insert(post);

                    if (rows > 0)
                    {
                        DisplayAlert("Success", "Experience successfully inserted", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Failed", "Failed to add experience", "Ok");
                    }

                    // Clear the input field
                    experienceEntry.Text = string.Empty;

                    // Navigate back to the history page
                    Navigation.PopAsync();
                }
                */

                Post.Insert(post);

                experienceEntry.Text = string.Empty;
                await Navigation.PopAsync();
                await DisplayAlert("Success", "Inserted experience into database", "Ok");

            }
            catch(NullReferenceException nre)
            {
                await DisplayAlert("Error", nre.Message, "Ok");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}