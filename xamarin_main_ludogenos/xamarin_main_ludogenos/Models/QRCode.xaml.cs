using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace xamarin_main_ludogenos.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCode : ContentPage
    {
        public QRCode()
        {
            InitializeComponent();
            btGerar.Clicked += BtGerar_Clicked;
        }

        private void BtGerar_Clicked(object sender, EventArgs e)
        {
            string value = etValor.Text;
            idQRCode.BarcodeValue = value;
        }
    }
}