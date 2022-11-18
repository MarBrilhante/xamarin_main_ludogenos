using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace xamarin_main_ludogenos.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Barcode : ContentPage
    {

        List<string> list = new List<string>();
        public Barcode()
        {
            InitializeComponent();

            btGerar.Clicked += BtGerar_Clicked;


            list.Add("EAN_13");
            list.Add("EAN_8");
            picker.ItemsSource = list;
        }


        private void BtGerar_Clicked(object sender, EventArgs e)
        {
            string value = etValor.Text;

            ZXingBarcodeImageView imageView = new ZXingBarcodeImageView();
            imageView.BarcodeFormat = ZXing.BarcodeFormat.EAN_13;


            int selectedModels = picker.SelectedIndex;

            if (selectedModels == 0)
            {
                idBarCode.BarcodeFormat = imageView.BarcodeFormat;
            }

            if (selectedModels == 1)
            {
                etValor.Text = value;
                idBarCode.BarcodeFormat = ZXing.BarcodeFormat.EAN_8;
            }
        }
    }
}