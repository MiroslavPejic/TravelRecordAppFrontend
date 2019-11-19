using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    public class LabeledLatLng
    {
        public string label { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public IList<LabeledLatLng> labeledLatLngs { get; set; }
        public int distance { get; set; }
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public string crossStreet { get; set; }

        // A location string in the format that we want
        public string locationLine
        {
            get
            {
                if (address != null && city != null)
                {
                    return string.Format("{0} - {1}", address, city);
                }
                else
                {
                    if (address == null && city == null)
                    {
                        return "";
                    }
                    else if(city == null)
                    {
                        return address;
                    }
                    else
                    {
                        return city;
                    }
                }
            }
        }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public bool primary { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Category> categories { get; set; }

        public static async Task<(List<Venue>, string)> GetVenues(double latitude, double longitude)
        {
            List<Venue> venues = new List<Venue>();

            var url = VenueRoot.GenerateURL(latitude, longitude);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();

                    var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                    venues = venueRoot.response.venues as List<Venue>;
                }
            }
            catch (Exception ex)
            {
                return (venues, ex.Message);
            }

            return (venues, "Success");
        }
    }

    public class Response
    {
        public IList<Venue> venues { get; set; }
    }

    public class VenueRoot
    {
        public Response response { get; set; }
        [JsonConstructor]
        public VenueRoot()
        {

        }

        public static string GenerateURL(double longitude, double latitude)
        {
            return string.Format(Constants.VENUE_SEARCH, longitude, latitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyymmdd"));
        }
    }
}
