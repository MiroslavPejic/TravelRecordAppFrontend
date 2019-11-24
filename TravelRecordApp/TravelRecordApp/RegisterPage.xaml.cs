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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if(passwordField.Text == confirmPasswordField.Text)
            {
                // Register a user
                Users user = new Users()
                {
                    email = emailField.Text,
                    password = passwordField.Text
                };

                await App.mobileServiceClient.GetTable<Users>().InsertAsync(user);
            }
            {
                await DisplayAlert("Error", "Passwords don't match", "Cancel");
            }
        }
    }
}