using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }

        [MaxLength(250)]
        public string Experience { get; set; }

        public string VenueName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Distance { get; set; }

        public string UserId { get; set; }

        public static async void Insert(Post post)
        {
            await App.mobileServiceClient.GetTable<Post>().InsertAsync(post);
        }
    }
}