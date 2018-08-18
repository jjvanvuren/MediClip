using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MediClip
{
    public partial class PatientPage : ContentPage
    {
        public PatientPage()
        {
            InitializeComponent();
        }

        private void PatientSearch_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Search Button", "It works!!", "OK");
        }
    }
}
