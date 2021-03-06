﻿using MediClip.Client;
using MediClip.Models;
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
        //creating Xaml link variables
        private Entry UserName;
        private Entry Password;

        public LoginPage()
        {
            InitializeComponent();
            //linking Xaml link variables to Xaml objects
            this.UserName = this.FindByName<Entry>("entUserName");
            this.Password = this.FindByName<Entry>("entPassword");
        }

        // Disable going back to the log in screen with hardware back button
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        //Event Handler to handle Login submission
        private void LogIn_Clicked(object sender, System.EventArgs e)
        {


            Task.Run(async () =>
            {

                // convert entered text to strings
                String sNurseUserName = this.UserName.Text;
                String sNursePassword = this.Password.Text;
                try
                {
                    // Find the nurse by username
                    MediClipClient client = new MediClipClient();
                    Nurse currentNurse = await client.GetNurse(sNurseUserName);

                    //If password is correct send to homescreen else display login failed message
                    if (sNursePassword.Equals(currentNurse.Password))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PushAsync(new HomePage());
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            DisplayAlert("Login Failed", "Username or Password is incorrect", "Okay");
                        });
                    }

                }
                catch
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Error", "Error retrieving login details", "Okay");
                    });
                }
            });
        }
    }
}