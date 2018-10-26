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
        private ListView listOfNotes;
        private ObservableCollection<Note> patient;

        public NoteListPage (ObservableCollection<Note> patients)
		{
			InitializeComponent ();
            this.listOfNotes = this.FindByName<ListView>("ListView");

            this.listOfNotes.ItemsSource = patients;
            patient = patients;
        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void AddNote_Clicked(object sender, System.EventArgs e)
        {
                int pPatientID = patient[1].PatientID;
                Navigation.PushAsync(new AddNotePage(pPatientID));
        }
        private void Note_Clicked(object sender, System.EventArgs e)
        {
            // 
            Task.Run(async () =>
            {

                // Get selected person id
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