using MediClip.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediClip.Client;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediClip
{
    public partial class PatientListPage : ContentPage
    {
        //creating Xaml link variables
        private ListView patientList;
        private ImageCell patientImage;

        public PatientListPage(ObservableCollection<Patient> patients)
        {
            InitializeComponent();
            //linking Xaml link variables to Xaml objects
            this.patientList = this.FindByName<ListView>("Patients");
            this.patientImage = this.FindByName<ImageCell>("PatientPicture");
            this.patientList.ItemsSource = patients;


        }

        // Displays the menu when the user clicks the hamburger menu
        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        // Displays the patients details
        private void Patient_Clicked(object sender, System.EventArgs e)
        {
            Task.Run(async () =>
            {

                // Get selected person id
                Patient selectedItem = this.patientList.SelectedItem as Patient;
                int pPatientID = selectedItem.PatientID;
                int wardID = selectedItem.WardID;
                try
                {
                    // run the query
                    MediClipClient client = new MediClipClient();
                    Patient result = await client.PatientByID(wardID, pPatientID);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PushAsync(new PatientPage(result));
                    });
                }
                catch
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