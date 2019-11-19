using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFLoadingPageService
{
    public interface ILoadingPageService
    {
        void InitLoadingPage(ContentPage loadingIndicatorPage = null);

        void ShowLoadingPage();

        void HideLoadingPage();
    }
}

namespace TravelRecordApp.Common
{
    public class CommonFunctions
    {
        public string updateSql(object selectedItem)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                try
                {
                    conn.Table<Post>();
                    
                    int rows = conn.Update(selectedItem);

                    return rows.ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
