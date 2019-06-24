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
                if (TryParse())
                {
                    var zo = float.Parse(zoEntry.Text);
                    var z1 = float.Parse(z1Entry.Text);
                    var z2 = float.Parse(z2Entry.Text);
                    var z01 = z1;
                    var z02 = z2;
                    var s21 = float.Parse(s21Entry.Text);
                    var l = float.Parse(lEntry.Text);

                    try
                    {
                        Parameters userParameters = new Parameters(zo, z1, z2, z01, z02, s21, l);
                        await Navigation.PushAsync(new ResultsPage(userParameters));
                    }

                    catch (ArgumentException)
                    {
                        await DisplayAlert("Предупреждение", "Одно или несколько введенных параметров не в допустимом диапазоне", "Ок");
                    }
                }
                else
                {
                    await DisplayAlert("Предупреждение", "Один или несколько параметров введены некорректно", "Ок");
                }

                
            }
            else
            {
                await DisplayAlert("Предупреждение", "Один или несколько параметров не заполнены", "Ок");
            }
        }

        private void OnByDefaultButtonClicked(object sender, EventArgs e)
        {
            zoEntry.Text = "61";
            z1Entry.Text = "75";
            z2Entry.Text = "50";
            s21Entry.Text = "5";
            lEntry.Text = "15";
        }

        /// <summary>
        /// Проверяет поля на заполненность 
        /// </summary>
        /// <returns></returns>
        private bool CheckFields()
        {
            if (zoEntry == null)
            {
                return false;
            }
            if (z1Entry.Text == null)
            {
                return false;
            }
            if (z2Entry.Text == null)
            {
                return false;
            }
            if (s21Entry.Text == null)
            {
                return false;
            }
            if (lEntry.Text == null)
            {
                return false;
            }

            if (zoEntry.Text.Length == 0)
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

            return true;
        }


        private bool TryParse()
        {
            try
            {
                var z0 = float.Parse(zoEntry.Text);
                var z1 = float.Parse(z1Entry.Text);
                var z2 = float.Parse(z2Entry.Text);
                var s21 = float.Parse(s21Entry.Text);
                var l = float.Parse(lEntry.Text);
            }
            catch (FormatException)
            {
                return false;
            }
            
            return true;
        }


    }
}