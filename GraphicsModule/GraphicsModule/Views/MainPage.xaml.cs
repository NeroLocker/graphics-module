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
                    var z0 = float.Parse(z0Entry.Text);
                    var z1 = float.Parse(z1Entry.Text);
                    var z2 = float.Parse(z2Entry.Text);
                    var z01 = z1;
                    var z02 = z2;
                    var s21 = float.Parse(s21Entry.Text);
                    var l = float.Parse(lEntry.Text);

                    try
                    {
                        Parameters userParameters = new Parameters(z0, z1, z2, z01, z02, s21, l);
                        await Navigation.PushAsync(new ResultsPage(userParameters));
                    }

                    catch (ArgumentException)
                    {
                        await DisplayAlert("Предупреждение", "Одно из введенных чисел не в допустимом диапазоне", "Ок");
                    }
                }
                else
                {
                    await DisplayAlert("Предупреждение", "Вы заполнили не все поля", "Ок");
                }

                
            }
            else
            {
                await DisplayAlert("Предупреждение", "Один из параметров не число", "Ок");
            }
        }

        private void OnByDefaultButtonClicked(object sender, EventArgs e)
        {
            z0Entry.Text = "50";
            z1Entry.Text = "75";
            z2Entry.Text = "50";
            s21Entry.Text = "10";
            lEntry.Text = "75";
        }

        /// <summary>
        /// Проверяет поля на заполненность 
        /// </summary>
        /// <returns></returns>
        private bool CheckFields()
        {
            if (z0Entry == null)
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

            if (z0Entry.Text.Length == 0)
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
                var z0 = float.Parse(z0Entry.Text);
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