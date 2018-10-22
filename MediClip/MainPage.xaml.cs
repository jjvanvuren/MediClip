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
    public partial class MainPage : ContentPage
    {
        private ListView wardList;
        private WardViewModel modWardViewModel = new WardViewModel();
        private ObservableCollection<Ward> wards = new ObservableCollection<Ward>();

        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = modWardViewModel;
            wards = modWardViewModel.Wards;

            this.wardList = this.FindByName<ListView>("ListView");

            this.wardList.ItemsSource = wards;

        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void Ward_Clicked(object sender, System.EventArgs e)
        {


            Task.Run(async () =>
            {

                // Get selected ward id
                Ward selectedItem = this.wardList.SelectedItem as Ward;
                int wWardID = selectedItem.WardID;
                try
                {
                    // run the query
                    MediClipClient client = new MediClipClient();
                    List<Patient> patientList = await client.ListPatient(wWardID);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // create patientlist page
                        this.Navigation.PushAsync(new PatientListPage(patientList));
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
