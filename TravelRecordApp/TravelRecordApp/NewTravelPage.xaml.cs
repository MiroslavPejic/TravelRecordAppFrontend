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
        Post post;

		public NewTravelPage ()
		{
			InitializeComponent ();

            post = new Post();
            containerStackLayout.BindingContext = post;
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

                post.CategoryId = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Address = selectedItem.location.address;
                post.Distance = selectedItem.location.distance.ToString();
                post.Latitude = selectedItem.location.lat;
                post.Longitude = selectedItem.location.lng;
                post.VenueName = selectedItem.name;
                post.UserId = App.user.Id;

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