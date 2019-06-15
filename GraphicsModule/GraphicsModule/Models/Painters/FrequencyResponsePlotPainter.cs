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
            List<float> pointList = new List<float>();
            for (float i = -200; i <= 200; i++)
            {
                Complex S21 = parameters.GetS21(i);
                float value = (float)(20 * Math.Log(S21.Magnitude));
                value = Math.Abs(value);
                pointList.Add(value);
            }

            int b = 0;
            int counter = 1;
            for (float i = plot.FirstPointX; i < plot.SecondPointX; i++)
            {                
                float x = i;
                try
                {
                    float y = pointList[counter];
                    canvas.DrawCircle(x, y, plot.LineThickness, plot.Paint);
                    counter += 1;
                }
                catch(ArgumentOutOfRangeException e)
                {
                    break;
                }
            }                    
        }
    }
}
