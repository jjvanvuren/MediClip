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
        private ListView patientList;
        private ImageCell patientImage;

        public PatientListPage(ObservableCollection<Patient> patients)
        {
            InitializeComponent();

            this.patientList = this.FindByName<ListView>("Patients");
            this.patientImage = this.FindByName<ImageCell>("PatientsPicture");
            this.patientList.ItemsSource = patients;
            /*foreach(Patient patient in patients)
            {
                if (!patient.Picture.Equals(""))
                {
                    this.patientImage.ImageSource = patient.Picture;
                }else{
                    this.patientImage.ImageSource = "blankPersonMale.png";
                }
            }*/

        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

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