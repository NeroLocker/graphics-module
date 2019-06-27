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

            // Заполняем поля стандартными значениями
            fMinEntry.Text = "0";
            fMaxEntry.Text = "20";
            lEntry.Text = "15";
            eREntry.Text = "1";
            z1Entry.Text = "57";
            z2Entry.Text = "29";
            z01Entry.Text = "50";
            z02Entry.Text = "25";
            s21Entry.Text = "3";
        }

        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                if (TryParse())
                {
                    var fMin = float.Parse(fMinEntry.Text);
                    var fMax = float.Parse(fMaxEntry.Text);
                    var l = float.Parse(lEntry.Text);
                    var eR = float.Parse(eREntry.Text);
                    var s21 = float.Parse(s21Entry.Text);
                    var z01 = float.Parse(z01Entry.Text);
                    var z02 = float.Parse(z02Entry.Text);
                    var z1 = float.Parse(z1Entry.Text);
                    var z2 = float.Parse(z2Entry.Text);

                    try
                    {
                        Parameters userParameters = new Parameters(fMin, fMax, l, eR, s21, z01, z02, z1, z2);
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
            fMinEntry.Text = "0";
            fMaxEntry.Text = "20";
            lEntry.Text = "15";
            eREntry.Text = "1";
            z1Entry.Text = "57";
            z2Entry.Text = "29";
            z01Entry.Text = "50";
            z02Entry.Text = "25";
            s21Entry.Text = "3";            
        }

        /// <summary>
        /// Проверяет поля на заполненность 
        /// </summary>
        /// <returns></returns>
        private bool CheckFields()
        {
            # region Проверка на null
            if (fMinEntry == null)
            {
                return false;
            }
            if (fMaxEntry.Text == null)
            {
                return false;
            }
            if (lEntry.Text == null)
            {
                return false;
            }
            if (eREntry.Text == null)
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
            if (z01Entry.Text == null)
            {
                return false;
            }
            if (z02Entry.Text == null)
            {
                return false;
            }
            if (s21Entry.Text == null)
            {
                return false;
            }            
            # endregion

            # region Проверка на длину строки
            if (fMinEntry.Text.Length == 0)
            {
                return false;
            }
            if (fMaxEntry.Text.Length == 0)
            {
                return false;
            }
            if (z01Entry.Text.Length == 0)
            {
                return false;
            }
            if (z02Entry.Text.Length == 0)
            {
                return false;
            }
            if (z1Entry.Text.Length == 0)
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
            if (eREntry.Text.Length == 0)
            {
                return false;
            }
            # endregion

            return true;
        }


        private bool TryParse()
        {
            try
            {
                var fMin = float.Parse(fMinEntry.Text);
                var fMax = float.Parse(fMaxEntry.Text);
                var l = float.Parse(lEntry.Text);
                var eR = float.Parse(eREntry.Text);
                var s21 = float.Parse(s21Entry.Text);
                var z01 = float.Parse(z01Entry.Text);
                var z02 = float.Parse(z02Entry.Text);
                var z1 = float.Parse(z1Entry.Text);
                var z2 = float.Parse(z2Entry.Text);
            }
            catch (FormatException)
            {
                return false;
            }
            
            return true;
        }
    }
}