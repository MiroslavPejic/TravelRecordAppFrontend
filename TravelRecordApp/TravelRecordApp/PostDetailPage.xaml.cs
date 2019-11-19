using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostDetailPage : ContentPage
	{
        Post selectedItem;

		public PostDetailPage (Post selectedPost)
		{
			InitializeComponent ();

            this.selectedItem = selectedPost;

            experienceEntry.Text = selectedItem.Experience;
            venueLabel.Text = selectedItem.VenueName;
            categoryLabel.Text = selectedItem.CategoryName;
            addressLabel.Text = selectedItem.Address;
            coordinatesLabel.Text = $"{selectedItem.Latitude}, {selectedItem.Longitude}";
            distanceLabel.Text = $"{selectedItem.Distance} m";
		}

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            selectedItem.Experience = experienceEntry.Text;

            await App.mobileServiceClient.GetTable<Post>().UpdateAsync(selectedItem);

            experienceEntry.Text = string.Empty;
            await Navigation.PopAsync();
            await DisplayAlert("Success", "Successfully updated experience", "Ok");

        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            selectedItem.Experience = experienceEntry.Text;

            await App.mobileServiceClient.GetTable<Post>().DeleteAsync(selectedItem);

            experienceEntry.Text = string.Empty;
            await Navigation.PopAsync();
            await DisplayAlert("Success", "Successfully deleted experience", "Ok");
        }
    }
}