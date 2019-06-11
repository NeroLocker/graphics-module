using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GraphicsModule.Models;

namespace GraphicsModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                var z0 = float.Parse(z0Entry.Text);
                var z1 = float.Parse(z1Entry.Text);
                var z2 = float.Parse(z2Entry.Text);
                var z01 = z1;
                var z02 = z2;
                var s21 = float.Parse(s21Entry.Text);
                var l = float.Parse(lEntry.Text);
                var fn = float.Parse(fnEntry.Text);

                Parameters userParameters = new Parameters(z0, z1, z2, z01, z02, s21, l, fn);
                await Navigation.PushAsync(new ResultsPage(userParameters));
            }
            
        }

        /// <summary>
        /// Проверяет поля на заполненность 
        /// </summary>
        /// <returns></returns>
        private bool CheckFields()
        {
            if(z0Entry.Text.Length == 0)
            {
                return false;
            }
            if(z1Entry.Text.Length == 0)
            {
                return false;
            }
            if(z2Entry.Text.Length == 0)
            {
                return false;
            }
            if(s21Entry.Text.Length == 0)
            {
                return false;
            }
            if(lEntry.Text.Length == 0)
            {
                return false;
            }
            if(fnEntry.Text.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}