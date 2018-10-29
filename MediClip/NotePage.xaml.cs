using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediClip.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediClip
{
	public partial class NotePage : ContentPage
	{
        //creating Xaml link variables
        private Label title;
        private Label text;
        private Image noteImage;

        public NotePage (Note incomingNote)
		{
			InitializeComponent ();

            //linking Xaml link variables to Xaml objects
            this.title = this.FindByName<Label>("Title");
            this.text = this.FindByName<Label>("NoteText");
            this.noteImage = this.FindByName<Image>("PhotoImage");

            title.Text = incomingNote.Title;
            text.Text = incomingNote.Text;
            noteImage.Source = ImageSource.FromFile(incomingNote.Picture);
        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }
    }
}