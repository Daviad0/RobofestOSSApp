using RobofestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp.Pages.SpectatorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class SpectateHome : TabbedPage
    {
        private RankViewModel rankViewModel = new RankViewModel();
        public SpectateHome()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            await rankViewModel.Update();
            BindingContext = rankViewModel;
            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}