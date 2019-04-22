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
    public partial class OutputPage : ContentPage
    {
        private int _numberOfWaves;
        public OutputPage(int numberOfWaves)
        {
            InitializeComponent();

            _numberOfWaves = numberOfWaves;            

            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Red.ToSKColor()
            };

            paint.Style = SKPaintStyle.Fill;
            paint.Color = SKColors.Black;
            //canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);

            //for (int i = 1; i < 30; i++)
            //{
            //    int x = i;
            //    int y = 3 * x;

            //    canvas.DrawCircle(info.Width / 2 + x, info.Height / 2 + y, 3, paint);
            //}

            for (int i = 1; i < info.Width; i++)
            {
                float PI = (float)(Math.PI);
                int x = i;
                float y = (float)(info.Height/9 * (Math.Sin(i * _numberOfWaves * PI / (info.Width - 1))) );

                canvas.DrawCircle(x, info.Height / 2 + y, 2, paint);
            }

        }
    }
}
