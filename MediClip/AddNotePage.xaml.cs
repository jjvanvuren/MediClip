using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediClip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using MediClip.Client;

namespace MediClip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePage : ContentPage
    {
        private Editor entryField;
        private int pPatientID;
        private Entry title;

        public AddNotePage(int patientID)
        {
            InitializeComponent();

            CameraButton.Clicked += CameraButton_Clicked;
            this.entryField = this.FindByName<Editor>("NoteArea");
            this.title = this.FindByName<Entry>("Title");

            CrossDeviceMotion.Current.SensorValueChanged += Current_SensorValueChanged;
            pPatientID = patientID;
        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        //============================================= 
        //Reference A1: Externally Sourced algorithm
        //Purpose: Adds camera functionality to the form, the
        //ability to add a picture taken with the phone
        //Date: 15/10/2018
        //Source: Xamarin help website
        //Author: Adam Pedley
        //URL: https://xamarinhelp.com/use-camera-take-photo-xamarin-forms/
        //Adaption Required: Had to manually set permissions for camera use and install
        //further nuget packages for the algorithm to  work correctly
        //=============================================
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            // check if device has a camera or take photo function is supported
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            // Check if camera permission is granted
            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            // Check if storage status is granted
            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                // Store taken photo in the phones album
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    //Directory = "Sample",
                    //Name = "test.jpg"
                });
            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //If no permissions are granted send user to app settings to give them the chance to change permissions
                CrossPermissions.Current.OpenAppSettings();
            }
        }
        //============================================= 
        //End reference A1
        //============================================= 


        //Saving all information to the data base
        private void Submit_Clicked(object sender, System.EventArgs e)
        {
            String theText = this.entryField.Text;
            String theTitle = this.title.Text;
            String theImage = this.title.Text;
                try
                {
                    // run the query
                    MediClipClient client = new MediClipClient();
                    //bool result =  
                    client.PostNote( pPatientID, theTitle,  theText, theImage);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PushAsync(new HomePage());
                    });
                }
                catch
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Error", "Error retreiving patient note", "Okay");
                    });
                }



        }

        //All below code is for cleaning the notes section for text
        //Below code was borrowed from week 5 lab to enable the Accelerometer
        //and to disable to the accelerometer

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer);
        }

        // When this page dissapears, stop listenting for changes
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossDeviceMotion.Current.Stop(MotionSensorType.Accelerometer);
        }


        void Current_SensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            // As the motion plugin supports multiple types of sensors (eg compass, magnetometer, etc) 
            // we need to differentiate between sensor readings
            switch (e.SensorType)
            {
                // When the accelerometer changes, call a method to handle this event
                case MotionSensorType.Accelerometer:
                    ClearTextField(e.Value as MotionVector);
                    break;
                default:
                    break;
            }
        }

        // This methord is ussed to check if there was a shake
        private Boolean WasTheDeviceShaken(MotionVector value)
        {

            if (value.X > 15 || value.Y > 15 || value.Z > 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // This methord checks to see if the device was shaken by calling another methord and if so this methord clears the text Feild.
        private void ClearTextField(MotionVector value)
        {
            if (WasTheDeviceShaken(value))
            {
                this.entryField.Text = "";
            }
        }
    }
}

