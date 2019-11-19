using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using TravelRecordApp.Common;
using XFLoadingPageService;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.London_stadium.jpg", assembly);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<ILoadingPageService>().InitLoadingPage(new LoadingIndicatorPage());
            bool isEmailEmpty    = string.IsNullOrEmpty(emailField.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordField.Text);

            if(isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Error", "Please fill in both fields", "Ok");
            }
            else
            {
                try
                {
                    LoginButton.IsEnabled = false;
                    registerUserButton.IsEnabled = false;
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    var user = (await App.mobileServiceClient.GetTable<Users>().Where(u => u.email == emailField.Text).ToListAsync()).FirstOrDefault();

                    if (user != null)
                    {
                        App.user = user;

                        if (user.password == passwordField.Text)
                        {
                            await Navigation.PushAsync(new HomePage());
                        }
                        else
                        {
                            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                            await DisplayAlert("Error", "Username or password incorrect", "Ok");
                        }
                    }
                    else
                    {
                        DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                        await DisplayAlert("Error", "There was problem logging you in", "Ok");
                    }

                    LoginButton.IsEnabled = true;
                    registerUserButton.IsEnabled = true;
                }
                catch(Exception ex)
                {
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                    await DisplayAlert("Error", ex.Message, "Ok");
                }
            }
        }

        private void registerUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}