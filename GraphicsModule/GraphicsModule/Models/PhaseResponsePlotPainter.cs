﻿using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModule
{
    /// <summary>
    /// Отрисовщик фазово-частотной характеристики.
    /// </summary>
    public class PhaseResponsePlotPainter : IPlotPainter
    {        
        /// <summary>
        /// Рисует график.
        /// </summary>
        /// <param name="plot">График.</param>
        /// <param name="canvas">Холст.</param>
        public void Paint(Plot plot, SKCanvas canvas)
        {
            canvas.Clear();

            var PI = (float)(Math.PI);
            var width = plot.Frame.GetWidth();
            var height = plot.Frame.GetHeight();
            var lineThickness = plot.LineThickness;
            SKPaint paint = plot.Paint;

            for (float i = plot.FirstPointX; i < plot.SecondPointX; i++)
            {
                float x = i;
                float y = (float)(height / 9 * (Math.Sin(i * 5 * PI / (width - 1))));
                canvas.DrawCircle(x, (height / 2 + y), lineThickness, paint);
            }
        }
    }
}
