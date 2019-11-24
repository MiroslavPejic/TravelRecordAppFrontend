using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class HomeVM
    {
        public NavigationCommands NavCommand { get; set; }

        public HomeVM()
        {
            NavCommand = new NavigationCommands(this);
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
}
