using RobofestApp.Pages.SpectatorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAuth : ContentPage
    {
        public SelectAuth()
        {
            InitializeComponent();
        }

        private void JudgeAuth(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
        private void StaffAuth(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
        private void SpectatorAuth(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SpectateHome());
        }
    }
}