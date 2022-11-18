using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunmiDemo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_main_ludogenos.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        public Test()
        {
            InitializeComponent();
        }

        private void testCompleted(object sender, EventArgs e)
        {
            Xamarin.Forms.DependencyService.Register<INativePages>();
            DependencyService.Get<INativePages>().StartActivityInAndroid("Hello Sunmi,\nI am printing some text\n");
            DependencyService.Get<INativePages>().StartActivityInAndroid("LF");
        }
    }
}