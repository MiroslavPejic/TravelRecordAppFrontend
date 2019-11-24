using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get { return id; }
            set 
            {
                id = value;
                onPropertyChanged("Id");
            }
        }

        private string experience;

        public string Experience
        {
            get { return experience; }
            set { experience = value; onPropertyChanged("Experience"); }
        }


        private string venueName;

        public string VenueName
        {
            get { return venueName; }
            set { venueName = value; onPropertyChanged("VenueName"); }
        }

        private string categoryId;

        public string CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; onPropertyChanged("CategoryId"); }
        }

        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; onPropertyChanged("CategoryName"); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; onPropertyChanged("Address"); }
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; onPropertyChanged("Longitude"); }
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; onPropertyChanged("Latitude"); }
        }

        private string distance;

        public string Distance
        {
            get { return distance; }
            set { distance = value; onPropertyChanged("Distance"); }
        }

        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; onPropertyChanged("UserId"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static async void Insert(Post post)
        {
            await App.mobileServiceClient.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<List<Post>> Read()
        {
            var posts = await App.mobileServiceClient.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

            return posts;
        }

        public static Dictionary<string, int> PostCaegories(List<Post> posts)
        {
            var categories = (from p in posts
                              orderby p.CategoryId
                              select p.CategoryName).Distinct().ToList(); 

            Dictionary<string, int> categoryCount = new Dictionary<string, int>();

            foreach (var category in categories)
            {
                var count = posts.Where(p => p.CategoryName == category).ToList().Count;

                categoryCount.Add(category, count);
            }

            return categoryCount;
        }

        private void onPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}