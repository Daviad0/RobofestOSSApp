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
    public partial class FieldSelectionPage : ContentPage
    {
        private static int FieldNum = 0;
        private static int Round = 0;
        private static bool Rerun = false;
        private static bool Usable = true;
        public FieldSelectionPage()
        {
            InitializeComponent();
        }
        private void NextPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckSelectionPage());
        }
        private void ChangeField(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == f1select)
            {
                FieldNum = 1;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if(sender == f2select)
            {
                FieldNum = 2;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if(sender == f3select)
            {
                FieldNum = 3;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == f4select)
            {
                FieldNum = 4;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == f5select)
            {
                FieldNum = 5;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == f6select)
            {
                FieldNum = 6;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            CheckIfValid();
        }
        private void ChangeRound(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == r1select)
            {
                Round = 1;
                r1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                r2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r1select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else
            {
                Round = 2;
                r2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                r1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r2select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            CheckIfValid();
        }

        public void CheckIfValid()
        {
            if(Round != 0 && FieldNum != 0)
            {
                NextPageButton.IsEnabled = true;
            }
        }

        private void rerunCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Rerun = e.Value;
        }

        private void usableCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Usable = e.Value;
        }
    }
}