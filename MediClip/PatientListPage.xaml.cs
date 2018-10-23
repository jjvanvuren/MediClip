using MediClip.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediClip
{
    public partial class PatientListPage : ContentPage
    {
        private ListView patientList;

        public PatientListPage(ObservableCollection<Patient> patients)
        {
            InitializeComponent();

            this.patientList = this.FindByName<ListView>("Patients");
            this.patientList.ItemsSource = patients;

        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void Patient_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PatientPage());
        }
    }
}