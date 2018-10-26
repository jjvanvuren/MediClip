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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LogIn_Clicked(object sender, System.EventArgs e)
        {


            //MainPage = new NavigationPage(new MainPage())
            //{
            //    BarBackgroundColor = Color.FromHex("#127c54"),
            //    BarTextColor = Color.White,
            //    BackgroundColor = Color.WhiteSmoke,
            //};
        }
    }
}