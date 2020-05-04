using RobofestApp.Models;
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
    public partial class SelectCompetition : ContentPage
    {
        public SelectCompetition()
        {
            BindingContext = new CompetitionViewModel();
            InitializeComponent();
        }
    }
}