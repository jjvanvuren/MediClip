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
        public PatientListPage(ObservableCollection<Patient> patients)
        {
            InitializeComponent();
            BindingContext = patients;
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