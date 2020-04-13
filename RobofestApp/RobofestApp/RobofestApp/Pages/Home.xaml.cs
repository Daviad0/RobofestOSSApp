using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RobofestApp.Models;
using Newtonsoft.Json;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        private static ObservableCollection<string> data = new ObservableCollection<string>();
        public Home()
        {
            InitializeComponent();
            /*data.Add("1. 1001-1");
            data.Add("2. 1002-1");
            data.Add("3. 1003-1");
            ThisWontWork.ItemsSource = data;*/
            BindingContext = new RankViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            BindingContext = new RankViewModel();
        }
        private void TeamDetails(object sender, EventArgs e)
        {
            var button = sender as Button;
            Navigation.PushAsync(new TeamDetails(button.ClassId));
        }
        private void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;
            if(tabbedPage.CurrentPage.ClassId != null)
            {
                if (tabbedPage.CurrentPage.ClassId == "rank")
                {
                    BindingContext = new RankViewModel();
                }
                else if (tabbedPage.CurrentPage.ClassId == "schedule")
                {
                    BindingContext = new ScheduleViewModel();
                }
            }
            
        }
    }
}