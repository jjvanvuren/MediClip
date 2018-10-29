using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MediClip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MediClip.Client;

namespace MediClip
{
	public partial class NoteListPage : ContentPage
	{
        //creating Xaml link variables
        private ListView listOfNotes;
        private int patient;

        public NoteListPage (ObservableCollection<Note> patients, int PatientId)
		{
			InitializeComponent ();
            //linking Xaml link variables to Xaml objects
            this.listOfNotes = this.FindByName<ListView>("ListView");

            this.listOfNotes.ItemsSource = patients;
            patient = PatientId;
        }

        //Event Handler when menu button is pressed
        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        //Event Handler when addNote is pressed to go to addNotePage
        private void AddNote_Clicked(object sender, System.EventArgs e)
        {
                Navigation.PushAsync(new AddNotePage(patient));
        }

        //Event Handler when a note from the list is clicked to view selected note
        private void Note_Clicked(object sender, System.EventArgs e)
        {
             
            Task.Run(async () =>
            {

                // Get selected person id and note Id to get a Note
                Note selectedItem = this.listOfNotes.SelectedItem as Note;
                int pPatientID = selectedItem.PatientID;
                int noteID = selectedItem.NoteID;
                try
                {
                    // run the query
                    MediClipClient client = new MediClipClient();
                    Note result = await client.GetNote(noteID, pPatientID);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PushAsync(new NotePage(result));
                    });
                }
                catch
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Error", "Error retreiving patient note", "Okay");
                    });
                }
            });
        }
    }
}