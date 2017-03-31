using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Sms;

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
        }

        private async void cmdStay_Click(object sender, RoutedEventArgs e)
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
                    msg.Body = "C2153.STAY.A2.0734079735";

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
                        txtStatus.Text = "Result success";
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
                            txtStatus.Text = "Result Error";
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

        private void cmdSleep_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdArm_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void cmdDisarm_Click(object sender, RoutedEventArgs e)
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
                }
                catch (Exception ex)
                {
                    //rootPage.NotifyUser(ex.Message, NotifyType.ErrorMessage);
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
                    msg.Body = "C2153.OFF.A2.0734079735";

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
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                //rootPage.NotifyUser("Failed to find SMS device", NotifyType.ErrorMessage);
            }
        }
    }
}
