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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            var postTable = await App.mobileServiceClient.GetTable<Post>().Where(p => p.UserId == App.user.id).ToListAsync();

            var categories = (from p in postTable
                                  orderby p.CategoryId
                                  select p.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoryCount = new Dictionary<string, int>();

            foreach(var category in categories)
            {
                var count = postTable.Where(p => p.CategoryName == category).ToList().Count;

                categoryCount.Add(category, count);
            }

            categoriesListView.ItemsSource = categoryCount;

            postCountLabel.Text = postTable.Count.ToString();
            //}
        }
    }
}