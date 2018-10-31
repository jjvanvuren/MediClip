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
        //creating Xaml link variables
        private Editor entryField;
        private Entry title;
        private int pPatientID;
        private String pictureName;


        public AddNotePage(int patientID)
        {
            InitializeComponent();

            //linking Xaml link variables to Xaml objects
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

        //============================================= 
        //Reference A2: Externally Sourced algorithms
        //Purpose: Adds camera functionality to the form, the
        //ability to add a picture taken with the phone
        //Date: 15/10/2018
        //Source: Github Repository
        //Author: James Montemagno
        //URL: https://github.com/jamesmontemagno/MediaPlugin
        //Adaption Required: Had to change permission settings so that the application could access the device’s 
        // camera and other media. Changed where photos are stored and how their file names are generated.
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
                //============================================= 
                //Reference C3: Externally Sourced Datetime pull
                //Purpose: Accessing date and time that is on the device
                //Date: 21/10/2018
                //Source: Xamarin Forum
                //Author: Laser
                //URL: https://forums.xamarin.com/discussion/25375/datetime-tolocaltime
                //Adaption Required: Created a variable that calls the current date and time, to make 
                // each photo name unique.
                //=============================================
                DateTime dt = DateTime.Now.ToLocalTime();
                pictureName = Convert.ToString(pPatientID) +
                              Convert.ToString(dt.Hour) +
                              Convert.ToString(dt.Minute) +
                              Convert.ToString(dt.Second) +
                              Convert.ToString(dt.Day) +
                              Convert.ToString(dt.Month) +
                              Convert.ToString(dt.Year);
                //============================================= 
                //End reference C3
                //============================================= 
                // Store taken photo in the device's local album storage
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                SaveToAlbum = true,
                    Name = pictureName
                });
                var path = file.Path;
                pictureName = path;
                if (file != null)
                {
                    PhotoImage.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                }
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

        //============================================= 
        //End reference A2
        //============================================= 



        //Saving all information to the data base
        private void Submit_Clicked(object sender, System.EventArgs e)
        {
            String theText = this.entryField.Text;
            String theTitle = this.title.Text;
            String theImage = pictureName;
            //Creating a Note to pass to the NotePage once created.
            Note tempNote = new Note();
            tempNote.PatientID = pPatientID;
            tempNote.Text = theText;
            tempNote.Picture = pictureName;
            tempNote.Title = theTitle;

            try
            {
                // run the query
                MediClipClient client = new MediClipClient();
                //bool result =  
                client.PostNote( pPatientID, theTitle,  theText, theImage);

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new NotePage(tempNote));
                });
            }
            catch
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", "Error uploading patient note", "Okay");
                });
            }
        }
        //============================================= 
        //Reference C1: Sensor activation code
        //Purpose: Adds Sensor functionality to the form, the
        // ability to read the Accelerometer
        //Date: 11/10/2018
        //Source: Lab 5
        //Author: David Cornforth
        //URL: N/A 
        //Adaption Required: Small changes to suit our code, such as different called methods.
        //=============================================

        //All below code is for cleaning the notes section for text
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer);
        }

        // When this page dissapears, stop listening for changes
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossDeviceMotion.Current.Stop(MotionSensorType.Accelerometer);
        }


        void Current_SensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            // Differentiate between sensor readings
            switch (e.SensorType)
            {
                // When the accelerometer changes it will call the desired method
                case MotionSensorType.Accelerometer:
                    ClearTextField(e.Value as MotionVector);
                    break;
                default:
                    break;
            }
        }
        //============================================= 
        //End reference C1
        //============================================= 

        // This method checks to see if the device was shaken by calling WasTheDeviceShaken 
        // and if so it clears the text field.
        private void ClearTextField(MotionVector value)
        {
            if (WasTheDeviceShaken(value))
            {
                this.entryField.Text = "";
            }
        }

        //============================================= 
        //Reference C2: Externally sourced algorithms
        //Purpose: takes the accelerometer readings and checks to see if device was shaken
        //Date: 11/10/2018
        //Source: W3 school
        //Author: unknown
        //URL: https://www.w3.org/TR/accelerometer/
        //Adaption Required: Lowered the reading amounts (sensitivity) and changed the code to return a bool
        // depending on if the device was shaken or not.
        //=============================================

        // Check to see if the device was shaken
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
        //============================================= 
        //End reference C2
        //============================================= 

    }
}

