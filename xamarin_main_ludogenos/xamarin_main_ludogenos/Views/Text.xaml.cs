using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluetoothPrinter;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_main_ludogenos.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Text : ContentPage
    {

        private string[] Strings = { "CP437", "CP850", "CP860", "CP863" };
        private string WelcomeText = "欢迎光临(Simplified Chinese)" +
            "歡迎光臨（traditional chinese)" +
            "Welcome(English) " +
            "어서 오세요.(Korean)" +
            " いらっしゃいませ(Japanese)" +
            " Willkommen in  der(Germany) " +
            "Souhaits  de bienvenue(France)" +
            " ยินดีต้อนรับสู่(Thai) " +
            "Добро пожаловать(Russian)" +
            "Benvenuti a(Italian)" +
            " vítejte v(Czech)" +
            "BEM - vindo Ao Brasil(Portutuese)" +
                                           " (Arabic)مرحبا بكم في";


        private string TextSize;
        private string CharSetOption;
        private bool IsBold;
        private bool IsUnderline;
        public Text()
        {
            InitializeComponent();
            CharSetText.Text = "GB18030";
            TextSizeLabel.Text = "24";
            Editor_text.Text = WelcomeText;
        
        }


        private void Bt_imprimir_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IBluetoothPrinterService>().PrintText(Editor_text.Text);
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
                    bt_imprimir.Text = current.Title;

                }
            }
        }

        async void OnClickCharSet(object sender, EventArgs e)
        {
            string option = await DisplayActionSheet("char set", "cancelar", null, Strings);

            if (option != "cancelar" && option != null)
            {
                CharSetOption = option;
                CharSetText.Text = option;
            }
        }
        void SliderChanged(object sender, EventArgs e)
        {
            TextSize = Math.Round(Slider.Value).ToString();
            TextSizeLabel.Text = Math.Round(Slider.Value).ToString();
        }
        void BoldChanged(object sender, CheckedChangedEventArgs e)
        {
            IsBold = e.Value;
        }
        void UnderlineChanged(object sender, CheckedChangedEventArgs e)
        {
            IsUnderline = e.Value;
        }
    }
}