    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFLoadingPageService;

namespace TravelRecordApp.Model
{
    public class Users : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static async Task<bool> Login(string email, string password)
        {
            DependencyService.Get<ILoadingPageService>().InitLoadingPage(new LoadingIndicatorPage());
            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                return false;
            }
            else
            {
                try
                {
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    var user = (await App.mobileServiceClient.GetTable<Users>().Where(u => u.email == email).ToListAsync()).FirstOrDefault();

                    if (user != null)
                    {
                        App.user = user;

                        if (user.password == password)
                        {
                            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                            return true;
                        }
                        else
                        {
                            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                            return false;
                        }
                    }
                    else
                    {
                        DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                    return false;
                }
            }
        }

        public static async void Register(Users user)
        {
            await App.mobileServiceClient.GetTable<Users>().InsertAsync(user);
        }
    }
}
