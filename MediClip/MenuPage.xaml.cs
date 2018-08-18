using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MediClip
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void SearchPatient_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SearchPatientPage());
        }

        private void SearchWard_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Success", "The button works!", "OK");
        }
    }
}
