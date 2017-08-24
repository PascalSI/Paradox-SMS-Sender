using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Paradox_SMS_Sender
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPageSMS : Page
    {
        public MapPageSMS()
        {
            this.InitializeComponent();
            mapWorld.Loaded += MapWorld_Loaded;
        }

        private async void MapWorld_Loaded(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 10 };
            Geoposition position = await geolocator.GetGeopositionAsync();

            double latitude = position.Coordinate.Latitude;
            double longitude = position.Coordinate.Longitude;

            var center = new Geopoint(new BasicGeoposition()
            {
                Latitude = latitude,
                Longitude = longitude

            });
            await mapWorld.TrySetSceneAsync(MapScene.CreateFromLocationAndRadius(center, 3000));

        }
    }
}
