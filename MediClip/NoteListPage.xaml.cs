using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MediClip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediClip
{
	public partial class NoteListPage : ContentPage
	{
        private ListView listOfNotes;

        public NoteListPage (ObservableCollection<Note> patients)
		{
			InitializeComponent ();
            this.listOfNotes = this.FindByName<ListView>("ListView");

            this.listOfNotes.ItemsSource = patients;
        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void AddNote_Clicked(object sender, System.EventArgs e)
        {

            Navigation.PushAsync(new AddNotePage());
        }
        private void Note_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Error", "Not Implimented", "Okay");
        }
    }
}