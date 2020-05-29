using Rg.Plugins.Popup.Extensions;
using RobofestApp.Pages.PopUpPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp.Pages.JudgePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitionManagerPage : ContentPage
    {
        public CompetitionManagerPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new FieldManagementPopup());
        }
    }
}