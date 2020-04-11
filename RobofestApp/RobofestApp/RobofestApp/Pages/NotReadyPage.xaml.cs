using RobofestApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotReadyPage : ContentPage
    {
        private static int Score;
        public NotReadyPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CurrentScore.Text = "CurrentScore: 5";
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReadyPage(1));
        }
    }
}