using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MediClip
{
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void SearchPatient_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Success","The button works!","OK");
        }

        private void SearchWard_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Success", "The button works!", "OK");
        }
    }
}
