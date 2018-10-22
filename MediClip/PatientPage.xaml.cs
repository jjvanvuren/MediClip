﻿using MediClip.Models;
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
            DataPatient patient = new DataPatient();
        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void Notes_Clicked(object sender, System.EventArgs e)
        {
            //var API_URL = "https://mediclipwebapi.azurewebsites.net/";
            //var sPatientID = ToString ()

            Navigation.PushAsync(new NoteListPage());
        }

        private void Dosage_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Dosage Information", "Dosage Information will be displayed here.", "OK");
        }
    }
}
