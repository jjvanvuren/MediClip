using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;

namespace MediClip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePage : ContentPage
    {
        private Editor enteryField;


        public AddNotePage()
        {
            InitializeComponent();

            CameraButton.Clicked += CameraButton_Clicked;
            this.enteryField = this.FindByName<Editor>("NoteArea");

            CrossDeviceMotion.Current.SensorValueChanged += Current_SensorValueChanged;
        }

        private void Handle_Activated(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
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
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
        }

        private void Submit_Clicked(object sender, System.EventArgs e)
        {
            //Navigation.PushAsync(new NoteListPage());
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
                this.enteryField.Text = "";
            }
        }
    }
}