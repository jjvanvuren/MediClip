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
	public partial class WardPage : ContentPage
	{
		public WardPage ()
		{
			InitializeComponent ();
		}

        private void WardSearch_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Search Button","It works!!","OK");
        }
    }
}