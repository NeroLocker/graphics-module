using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using GraphicsModule.Interfaces;
using GraphicsModule.Models;
using SkiaSharp;

namespace GraphicsModule.Models.Painters
{
    /// <summary>
    /// Отрисовщик амплитудно-частотной характеристики.
    /// </summary>
    public class FrequencyResponsePlotPainter : IPlotPainter
    {
        /// <summary>
        /// Рисует амплитудно-частотную характеристику.
        /// </summary>
        /// <param name="plot">График.</param>
        /// <param name="parameters">Параметры.</param>
        /// <param name="canvas">Холст.</param>
        public void Paint(Plot plot, Parameters parameters, SKCanvas canvas)
        {
            // цикл для расчета коэффициента масштабирования координат X
            float coef = 0.01f;

            double i = 0;
            float point = 0;
            while (Convert.ToInt32(point) != Convert.ToInt32(plot.SecondPointX))
            {


                float x = (float)(parameters.Fmax);
                point = plot.FirstPointX + x * coef;

                coef += 0.01f;
            }



            i = plot.FirstPointX;
            float j = 0;
            while (i <= plot.SecondPointX)
            {                
                float x = j;
                float y = plot.GetCenterPointOfYAxis();
                y += -(-(x * x) + 2 * x + 3);
                canvas.DrawPoint(plot.FirstPointX + x * coef, y, plot.RedPaint);

                i += 0.04f;
                j += 0.04f;
            }                       

        }
    }
}
