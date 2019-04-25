using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace TestApp
{
    public partial class MainPage : ContentPage
    {
        private int _numberOfWaves = 5;
        public MainPage()
        {
            InitializeComponent();        
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            int centerOfWidth = info.Width / 2;
            int centerOfHeight = info.Height / 2;

            canvas.Clear();

            SKPaint blackPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black
            };

            // Чертим оси x и y
            // Ось x
            canvas.DrawLine(0, centerOfHeight, info.Width, centerOfHeight, blackPaint);

            // Ось y
            canvas.DrawLine(centerOfWidth, 0, centerOfWidth, info.Height, blackPaint);

            if (TrigonometricSwitch.IsToggled)
            {                
                // График не в нуле
                // sin(x)
                for (int i = 0; i < info.Width; i++)
                {
                    float PI = (float)(Math.PI);
                    int x = i;
                    float y = (float)(info.Height / 9 * (Math.Sin(i * _numberOfWaves * PI / (info.Width - 1))));

                    canvas.DrawCircle(x, info.Height / 2 + y, 2, blackPaint);
                }

                // cos(x)
                for (int i = 0; i < info.Width; i++)
                {
                    float PI = (float)(Math.PI);
                    int x = i;
                    float y = (float)(info.Height / 9 * (Math.Cos(i * _numberOfWaves * PI / (info.Width - 1))));

                    canvas.DrawCircle(x, info.Height / 2 + y, 2, blackPaint);
                }
            }

            if (OtherSwitch.IsToggled)
            {
                // График не в нуле
                // cos(x)
                for (int i = 0; i < info.Width; i++)
                {
                    float PI = (float)(Math.PI);
                    int x = i;
                    float y = (float)(info.Height / 9 * (Math.Cos(i * _numberOfWaves * PI / (info.Width - 1))));

                    canvas.DrawCircle(x, info.Height / 2 + y, 2, blackPaint);
                }
            }
            
        }

        private void OnTrigonometricSwitchToggled(object sender, EventArgs e)
        {
            if (TrigonometricSwitch.IsToggled)
            {
                TrigonometricSwitch.IsEnabled = false;
                OtherSwitch.IsEnabled = true;

                OtherSwitch.IsToggled = false;

                OutputCanvasView.InvalidateSurface();
            }
        }

        private void OnOtherSwitchToggled(object sender, EventArgs e)
        {
            if (OtherSwitch.IsToggled)
            {
                OtherSwitch.IsEnabled = false;
                TrigonometricSwitch.IsEnabled = true;

                TrigonometricSwitch.IsToggled = false;

                OutputCanvasView.InvalidateSurface();
            }
        }        
    }
}
