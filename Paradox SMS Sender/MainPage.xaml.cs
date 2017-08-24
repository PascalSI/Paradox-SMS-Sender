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
using Windows.Devices.Sms;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Core;
using System.Windows;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Paradox_SMS_Sender
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SmsDevice2 device;

        public MainPage()
        {
            this.InitializeComponent();
            mapControl.Loaded += Map_Loaded;
        }
        
        private async void ConsoleCommand(string command)
        {
            // If this is the first request, get the default SMS device. If this
            // is the first SMS device access, the user will be prompted to grant
            // access permission for this application.
            if (device == null)
            {
                try
                {
                    //rootPage.NotifyUser("Getting default SMS device ...", NotifyType.StatusMessage);
                    device = SmsDevice2.GetDefault();
                    txtStatus.Text = "Device 1";
                }
                catch (Exception ex)
                {
                    //rootPage.NotifyUser(ex.Message, NotifyType.ErrorMessage);
                    txtStatus.Text = ex.ToString();
                    return;
                }
            }

            string msgStr = "";
            if (device != null)
            {
                try
                {
                    // Create a text message - set the entered destination number and message text.
                    SmsTextMessage2 msg = new SmsTextMessage2();
                    msg.To = "0734079735";
                    msg.Body = command;

                    // Send the message asynchronously
                    //rootPage.NotifyUser("Sending message ...", NotifyType.StatusMessage);
                    SmsSendMessageResult result = await device.SendMessageAndGetResultAsync(msg);

                    if (result.IsSuccessful)
                    {
                        msgStr = "";
                        msgStr += "Text message sent, cellularClass: " + result.CellularClass.ToString();
                        IReadOnlyList<Int32> messageReferenceNumbers = result.MessageReferenceNumbers;

                        for (int i = 0; i < messageReferenceNumbers.Count; i++)
                        {
                            msgStr += "\n\t\tMessageReferenceNumber[" + i.ToString() + "]: " + messageReferenceNumbers[i].ToString();
                        }
                        //rootPage.NotifyUser(msgStr, NotifyType.StatusMessage);
                        txtStatus.Text = "Command sent!";
                    }
                    else
                    {
                        msgStr = "";
                        msgStr += "ModemErrorCode: " + result.ModemErrorCode.ToString();
                        msgStr += "\nIsErrorTransient: " + result.IsErrorTransient.ToString();
                        if (result.ModemErrorCode == SmsModemErrorCode.MessagingNetworkError)
                        {
                            msgStr += "\n\tNetworkCauseCode: " + result.NetworkCauseCode.ToString();

                            if (result.CellularClass == CellularClass.Cdma)
                            {
                                msgStr += "\n\tTransportFailureCause: " + result.TransportFailureCause.ToString();
                            }
                            //rootPage.NotifyUser(msgStr, NotifyType.ErrorMessage);
                            txtStatus.Text = "Error!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtStatus.Text = "Device null";
                }
            }
            else
            {
                //rootPage.NotifyUser("Failed to find SMS device", NotifyType.ErrorMessage);
                txtStatus.Text = "Failed to find SMS device";
            }
        }

        private void cmdStay_Click(object sender, RoutedEventArgs e)
        {
            string area = "A0";

            if ((bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A3";
            }
            else if (!(bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A2";
            }
            else if((bool)ckbArea1.IsChecked.Value && !(bool)ckbArea2.IsChecked.Value)
            {
                area = "A1";
            }
            else
            {
                txtStatus.Text = "No area selected!";
                return;
            }

            string command = "C2153.STAY." + area + ".0734079735";
            ConsoleCommand(command);
        }

        private  void cmdSleep_Click(object sender, RoutedEventArgs e)
        {
            string area = "A0";

            if ((bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A3";
            }
            else if (!(bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A2";
            }
            else if ((bool)ckbArea1.IsChecked.Value && !(bool)ckbArea2.IsChecked.Value)
            {
                area = "A1";
            }
            else
            {
                txtStatus.Text = "No area selected!";
                return;
            }

            string command = "C2153.SLEEP." + area + ".0734079735";
            ConsoleCommand(command);
        }

        private void cmdArm_Click(object sender, RoutedEventArgs e)
        {
            string area = "A0";

            if ((bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A3";
            }
            else if (!(bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A2";
            }
            else if ((bool)ckbArea1.IsChecked.Value && !(bool)ckbArea2.IsChecked.Value)
            {
                area = "A1";
            }
            else
            {
                txtStatus.Text = "No area selected!";
                return;
            }

            string command = "C2153.ARM." + area + ".0734079735";
            ConsoleCommand(command);
        }

        private void cmdDisarm_Click(object sender, RoutedEventArgs e)
        {
            string area = "A0";

            if ((bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A3";
            }
            else if (!(bool)ckbArea1.IsChecked.Value && (bool)ckbArea2.IsChecked.Value)
            {
                area = "A2";
            }
            else if ((bool)ckbArea1.IsChecked.Value && !(bool)ckbArea2.IsChecked.Value)
            {
                area = "A1";
            }
            else
            {
                txtStatus.Text = "No area selected!";
                return;
            }

            string command = "C2153.OFF." + area + ".0734079735";
            ConsoleCommand(command);
        }

        private void ckbMap_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(MapPageSMS));

            if ((bool)ckbMap.IsChecked)
            {
                mapControl.Visibility = Visibility.Visible;
            }
            else
            {
                mapControl.Visibility = Visibility.Collapsed;
            }
        }

        async private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                //_rootPage.NotifyUser("Location updated.", NotifyType.StatusMessage);
                //UpdateLocationData(e.Position);
            });
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            //Show home icon on map
            BasicGeoposition geoPositionHome = new BasicGeoposition() { Latitude = 47.1310166666667, Longitude = 27.6005266666667 };
            Geopoint geoPointHome = new Geopoint(geoPositionHome);

            // Create a MapIcon.
            MapIcon mapIconHome = new MapIcon();
            mapIconHome.Location = geoPointHome;
            mapIconHome.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIconHome.Title = "Home";
            mapIconHome.ZIndex = 0;

            // Add the MapIcon to the map.
            mapControl.MapElements.Add(mapIconHome);

            //Request the user access to Localization services
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    
                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 10 };
                    //geolocator.PositionChanged += OnPositionChanged;
                    Geoposition position = await geolocator.GetGeopositionAsync();
                    Geopoint geoPointCurrent = position.Coordinate.Point;

                    // Set the map location
                    mapControl.Center = geoPointCurrent;
                    mapControl.ZoomLevel = 17;
                    mapControl.LandmarksVisible = true;

                    // Create a MapIcon
                    MapIcon mapIconCurrent = new MapIcon();
                    mapIconCurrent.Location = geoPointCurrent;
                    mapIconCurrent.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIconCurrent.Title = "Position";
                    mapIconCurrent.ZIndex = 0;

                    // Add the MapIcon to the map.
                    mapControl.MapElements.Add(mapIconCurrent);


                    //double latitude = position.Coordinate.Latitude;
                    //double longitude = position.Coordinate.Longitude;

                    //double home_lat = 47.1310166666667;
                    //double home_long = 27.6005266666667;
                    //double thld = 0;

                    //var center = new Geopoint(new BasicGeoposition()
                    //{
                    //    Latitude = latitude,
                    //    Longitude = longitude

                    //});

                    //await mapControl.TrySetSceneAsync(MapScene.CreateFromLocationAndRadius(center, 300));

                    //txtStatus.Text = latitude.ToString() + " " + longitude.ToString();

                    //if ((latitude < (home_lat + thld) || latitude > (home_lat - thld)) &&
                    //   (longitude < (home_long + thld) || longitude > (home_long - thld)))
                    //{
                    //    txtStatus.Text = "Am ajuns acasa!";
                    //}

                    //geofences = GeofenceMonitor.Current.Geofences;

                    //FillRegisteredGeofenceListBoxWithExistingGeofences();
                    //FillEventListBoxWithExistingEvents();

                    //// Register for state change events.
                    //GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
                    //GeofenceMonitor.Current.StatusChanged += OnGeofenceStatusChanged;
                    break;

                case GeolocationAccessStatus.Denied:
                    //_rootPage.NotifyUser("Access denied.", NotifyType.ErrorMessage);
                    break;

                case GeolocationAccessStatus.Unspecified:
                    //_rootPage.NotifyUser("Unspecified error.", NotifyType.ErrorMessage);
                    break;
            }
        }
    }
}
