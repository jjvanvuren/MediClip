using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MediClip
{
    // The Menu page used for returning to homepage and logout
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        // Returns user to the home page (ward list)
        private void ReturnHome_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        // Logout the user. Returns to the LoginPage
        private void Logout_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}
