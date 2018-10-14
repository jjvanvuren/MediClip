using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediClip
{
	public partial class NoteListPage : ContentPage
	{
		public NoteListPage ()
		{
			InitializeComponent ();
		}

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void AddNote_Clicked(object sender, System.EventArgs e)
        {

            Navigation.PushAsync(new AddNotePage());
        }
    }
}