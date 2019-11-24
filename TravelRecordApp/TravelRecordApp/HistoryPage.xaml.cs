using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            /*
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();

                postListView.ItemsSource = posts;
            }
            */

            try
            {
                var posts = await Post.Read();

                postListView.ItemsSource = posts;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Failed to get data", "Ok");
            }
        }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = postListView.SelectedItem as Post;

            if(selectedItem != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedItem));
            }
        }
    }
}