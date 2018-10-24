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
	public partial class NotePage : ContentPage
	{
		public NotePage ()
		{
			InitializeComponent ();
		}

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }
    }
}