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
        Users user;
        public RegisterPage()
        {
            InitializeComponent();

            user = new Users();
            containerStackLayout.BindingContext = user;
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if(passwordField.Text == confirmPasswordField.Text)
            {
                Users.Register(user);
            }
            {
                await DisplayAlert("Error", "Passwords don't match", "Cancel");
            }
        }
    }
}