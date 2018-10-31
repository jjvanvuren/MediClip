using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Xamarin.Forms;
using MediClip.Models;
using System.Collections.ObjectModel;
using MediClip.Client;

namespace MediClip
{
    public partial class HomePage : ContentPage
    {
        //creating Xaml link variables
        private ListView wardList;
        private WardViewModel modWardViewModel = new WardViewModel();
        private ObservableCollection<Ward> wards = new ObservableCollection<Ward>();

        //============================================= 
        //Reference A3: Externally Sourced algorithm
        //Purpose: Disable the back button so user doesnt accidentally return to
        // login page.
        //Date: 26/10/2018
        //Source: stackoverflow
        //Author: Jesper Christensen
        //URL: https://stackoverflow.com/questions/35862657/disabling-back-button-c-sharp-android-xamarin-code-not-responding
        //Adaption Required: None
        //=============================================

        // Disable going back to the log in screen with hardware back button
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        //============================================= 
        //End reference A3
        //============================================= 

        public HomePage()
        {
            InitializeComponent();

            BindingContext = modWardViewModel;
            wards = modWardViewModel.Wards;
            //linking Xaml link variable to Xaml objects
            this.wardList = this.FindByName<ListView>("ListView");

            this.wardList.ItemsSource = wards;

        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void Ward_Clicked(object sender, System.EventArgs e)
        {
            ObservableCollection<Patient> patientList = new ObservableCollection<Patient>();

            Task.Run(async () =>
            {
                // Get selected ward id
                Ward selectedItem = this.wardList.SelectedItem as Ward;
                int wWardID = selectedItem.WardID;
                try
                {
                    // run the query
                    MediClipClient client = new MediClipClient();
                    List<Patient> result = await client.ListPatient(wWardID);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // create patientlist page
                        foreach (Patient patient in result)
                        {
                            patientList.Add(patient);
                        }
                        Navigation.PushAsync(new PatientListPage(patientList));
                    });
                }
                catch // If the query fails display an error message
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Error", "Error retreiving patient list", "Okay");
                    });
                }
            });
        }
    }
}
