using System;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_main_ludogenos.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {

        private string[] Strings = { "API", "Cancelar" };




        private string MétodoOption;

        public Settings()
        {
            InitializeComponent();
            MétodoText.Text = "API";

        }

        async void OnClickMétodo(object sender, EventArgs e)

        {


            string option = await DisplayActionSheet("Método de conexão", "cancelar", null, "API");
            if (option != "cancelar" && option != null)
            {
                MétodoOption = option;
                MétodoText.Text = option;
            }
        }
        void BoldChanged(object sender, CheckedChangedEventArgs e)
        {

        }
        void UnderlineChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}