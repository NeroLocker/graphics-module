using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    
    public partial class MainPage : ContentPage
    {        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCalculateButton(object sender, EventArgs e)
        {
            try
            {
                int numberOfWaves = Int32.Parse(numberOfWavesEntry.Text);

                if (numberOfWaves <= 30 && numberOfWaves >= 5)
                {
                    await Navigation.PushAsync(new OutputPage(numberOfWaves));

                    numberOfWavesEntry.Text = String.Empty;
                }
            }
            catch (FormatException)
            {

            }            
        }
    }
}