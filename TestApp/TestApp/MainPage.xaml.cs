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

        /// <summary>
        /// Отрисовывает графику на плоскости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            float centerOfWidth = info.Width / 2;
            float centerOfHeight = info.Height / 2;

            int thicknessOfLines = 2;

            MyPaints paints = new MyPaints();

            canvas.Clear();

            // Чертим оси x и y
            // Ось x
            canvas.DrawLine(0, centerOfHeight, info.Width, centerOfHeight, paints.Dict["Black Paint"]);

            // Точка 0
            canvas.DrawText("0", 0, (float)(info.Height / 2), paints.Dict["Text Paint"]);

            // Пометка оси X
            canvas.DrawText("x", (float)(info.Width - 24), (float)(info.Height / 2), paints.Dict["Text Paint"]);

            // Ось y
            //canvas.DrawLine(centerOfWidth, 0, centerOfWidth, info.Height, blackPaint);

            if (TrigonometricSwitch.IsToggled)
            {
                // График не в нуле
                // sin(x)

                // Выводим надпись
                canvas.DrawText("sin(x)", 0, (float)(info.Height * 0.25), paints.Dict["Text Paint"]);

                for (int i = 0; i < info.Width; i++)
                {
                    float PI = (float)(Math.PI);
                    int x = i;
                    float y = (float)(info.Height / 9 * (Math.Sin(i * _numberOfWaves * PI / (info.Width - 1))));

                    canvas.DrawCircle(x, info.Height / 2 + y, thicknessOfLines, paints.Dict["Black Paint"]);
                }

                // cos(x)

                paints.Dict["Text Paint"].Color = SKColors.Red;
                canvas.DrawText("cos(x)", 0, (float)(info.Height * 0.30), paints.Dict["Text Paint"]);
                for (int i = 0; i < info.Width; i++)
                {
                    float PI = (float)(Math.PI);
                    int x = i;
                    float y = (float)(info.Height / 9 * (Math.Cos(i * _numberOfWaves * PI / (info.Width - 1))));

                    canvas.DrawCircle(x, info.Height / 2 + y, thicknessOfLines, paints.Dict["Red Paint"]);
                }

                // sin(x) + cos(x)
                paints.Dict["Text Paint"].Color = SKColors.Blue;
                canvas.DrawText("sin(x) + cos(x)", 0, (float)(info.Height * 0.35), paints.Dict["Text Paint"]);
                
                for (int i = 0; i < info.Width; i++)
                {
                    float PI = (float)(Math.PI);
                    int x = i;
                    float sinY = (float)(info.Height / 9 * (Math.Sin(i * _numberOfWaves * PI / (info.Width - 1))));
                    float cosY = (float)(info.Height / 9 * (Math.Cos(i * _numberOfWaves * PI / (info.Width - 1))));
                    float y = sinY + cosY;

                    canvas.DrawCircle(x, info.Height / 2 + y, thicknessOfLines, paints.Dict["Blue Paint"]);
                }
            }

            if (OtherSwitch.IsToggled)
            {
                // График не в нуле
                // y = kx + b

                canvas.DrawText("kx", 0, (float)(info.Height * 0.25), paints.Dict["Text Paint"]);

                for (int i = -300; i < 600; i++)
                {
                    int x = i;
                    float y = -x;

                    canvas.DrawCircle((float)(info.Width * 0.35 + x), info.Height / 2 + y, thicknessOfLines, paints.Dict["Black Paint"]);
                }

                // y = x^2

                paints.Dict["Text Paint"].Color = SKColors.Red;
                canvas.DrawText("x*x", 0, (float)(info.Height * 0.30), paints.Dict["Text Paint"]);
                for (int i = -300; i < 300; i++)
                {
                    int x = i;
                    float y = (float)(-0.01 * (x * x));                   

                    canvas.DrawCircle((float)(info.Width * 0.35 + x), info.Height / 2 + y, thicknessOfLines, paints.Dict["Red Paint"]);
                }

                // y = exp^x

                paints.Dict["Text Paint"].Color = SKColors.Blue;
                canvas.DrawText("e^x", 0, (float)(info.Height * 0.35), paints.Dict["Text Paint"]);

                double counter = -(info.Width * 0.35);
                while (counter < 30)
                {
                    double x = counter;
                    float y = (float)(-Math.Exp(x));

                    canvas.DrawCircle((float)(info.Width * 0.35 + x), info.Height / 2 + y, thicknessOfLines, paints.Dict["Blue Paint"]);

                    counter += 0.05;
                }
                //for (int i = -300; i < 300; i++)
                //{
                //    int x = i;
                //    float y = (float)(0.0005 * Math.Exp(x));

                //    canvas.DrawCircle((float)(info.Width * 0.25 + x), info.Height / 2 + y, thicknessOfLines, bluePaint);                   
                //}

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
