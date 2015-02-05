using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Devices.Enumeration;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace PersonalTracking.WindowsPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void OnGeofenceStateChanged(GeofenceMonitor sender, object args)
        {

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DeviceAccessInformation accessInfo;
            accessInfo = DeviceAccessInformation.CreateFromDeviceClass(DeviceClass.Location);
            accessInfo.AccessChanged += OnAccessChanged;
            Initialize();
            CreateGeofence();
        }

        private void OnAccessChanged(DeviceAccessInformation sender, DeviceAccessChangedEventArgs args)
        {



        }

        Geolocator geolocator;
        CancellationTokenSource cts;
        async private void Initialize()
        {
            try
            {
                // Get a geolocator object 
                geolocator = new Geolocator();

                // Get cancellation token
                cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                await geolocator.GetGeopositionAsync().AsTask(token);

                // other initialization for your app could go here

            }

            catch (Exception)
            {
                if (geolocator.LocationStatus == PositionStatus.Disabled)
                {
                    // On Windows Phone, this exception will be thrown when you call 
                    // GetGeopositionAsync if the user has disabled locaton in Settings.
                    //otPage.NotifyUser("Location has been disabled in Settings.");
                }

            }
            finally
            {
                cts = null;
            }

        }

        private void CreateGeofence()
        {
            Geofence geofence = null;

            string fenceKey = "Test";

            BasicGeoposition position;
            position.Latitude = 35.246166;
            position.Longitude = 33.035971;
            position.Altitude = 0.0;
            //            35.246166, 33.035971


            // the geofence is a circular region
            Geocircle geocircle = new Geocircle(position, 50);


            // want to listen for enter geofence, exit geofence and remove geofence events
            // you can select a subset of these event states
            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;
            mask |= MonitoredGeofenceStates.Removed;





            geofence = new Geofence(fenceKey, geocircle, mask, false);
            GeofenceMonitor.Current.Geofences.Add(geofence);

        }





    }
}
