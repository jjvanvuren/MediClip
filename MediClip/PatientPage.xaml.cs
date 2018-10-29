using MediClip.Models;
using System;
using System.Collections.Generic;
using MediClip.Client;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace MediClip
{
    public partial class PatientPage : ContentPage
    {
        //Creating private xaml display variables
        private Label name;
        private Label gender;
        private Label assignDateFrom;
        private Label assignDateTo;
        private int patientID;
        private String dosageInformation;
        private Image patientImage;
        private Label dob;

        public PatientPage(Patient incomingPatient)
        {
            InitializeComponent();
            //linking Xaml link variables to Xaml objects
            this.name = this.FindByName<Label>("FullName");
            this.gender = this.FindByName<Label>("Gender");
            this.assignDateFrom = this.FindByName<Label>("AssignDateFrom");
            this.assignDateTo = this.FindByName<Label>("AssignDateTo");
            this.patientImage = this.FindByName<Image>("PatientPicture");
            this.dob = this.FindByName<Label>("DoB");

            //Outputting all vaibles to correct locations in xaml
            name.Text = "Name: " + incomingPatient.FullName;
            dob.Text = "Date of Birth: " + incomingPatient.Dob;
            gender.Text = "Gender: " + incomingPatient.Sex;
            assignDateFrom.Text = "Assigned Date: " + incomingPatient.AssignDateFrom;
            assignDateTo.Text = "Discharge Date: " + incomingPatient.AssignDateTo;
            patientID = incomingPatient.PatientID;
            dosageInformation = incomingPatient.Dosage;
            if(incomingPatient.Picture != "")
            {
                patientImage.Source = incomingPatient.Picture;
            }else{
                patientImage.Source = "blankPersonMale.png";
            }
        }

        //Event Handler when menu button is pressed
        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        //Event Handler when note button is clicket to see patients notes.
        private void Notes_Clicked(object sender, System.EventArgs e)
        {
            ObservableCollection<Note> patientNotes = new ObservableCollection<Note>();

            Task.Run(async () =>
            {
                try
                {
                    // run the query
                    MediClipClient client = new MediClipClient();
                    List<Note> result = await client.PatientByID(patientID);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        foreach (Note notes in result)
                        {
                            patientNotes.Add(notes);
                        }
                        Navigation.PushAsync(new NoteListPage(patientNotes, patientID));
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

        //Event Handler to view Patients dosageInformation
        private void Dosage_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Dosage Information", dosageInformation, "OK");
        }
    }
}
