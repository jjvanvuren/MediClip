using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediClip
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNotePage : ContentPage
	{
		public AddNotePage ()
		{
			InitializeComponent ();
		}

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void AddPhoto_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddPhotoPage());
        }

        private void Submit_Clicked(object sender, System.EventArgs e)
        {
            //Navigation.PushAsync(new NoteListPage());
        }
    }
}