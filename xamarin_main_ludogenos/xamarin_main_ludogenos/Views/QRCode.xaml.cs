using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using BluetoothPrinter;
using Application = Xamarin.Forms.Application;

namespace xamarin_main_ludogenos.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCode : ContentPage
    {
        public QRCode()
        {
            InitializeComponent();
            
        }


        private async void selectPrinterButton_Clicked(object sender, EventArgs e)
        {
            var devices = DependencyService.Get<IBluetoothPrinterService>().GetAvailableDevices();
            if (devices != null && devices.Count > 0)
            {
                var choices = devices.Select(d => d.Title).ToArray();
                string action = await Application.Current.MainPage.DisplayActionSheet("Select printer device.", "Cancel", null, choices);
                if (choices.Contains(action))
                {
                    SelectDevice(action);
                }
            }
            else
            {
                await DisplayAlert("Select Printer", "No device.", "OK");
            }
        }


        void SelectDevice(string printerName)
        {
            if (DependencyService.Get<IBluetoothPrinterService>().SetCurrentDevice(printerName))
            {
                var current = DependencyService.Get<IBluetoothPrinterService>().GetCurrentDevice();
                if (current != null)
                {
                    btGerar.Text = current.Title;

                }
            }
        }

        private void BtGerar_Clicked(object sender, EventArgs e)
        {
            string value = etValor.Text;
            idQRCode.BarcodeValue = value;
            DependencyService.Get<IBluetoothPrinterService>().PrintQR(etValor.Text);
        }
    }
}