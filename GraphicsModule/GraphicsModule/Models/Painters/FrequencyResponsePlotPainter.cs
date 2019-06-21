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
            // ось X
            
            //_magnitudePointListOfS21 = parameters.GetListOfMagnitudesOfS21();
            List<float> magnitudePointListOfS31 = parameters.GetListOfMagnitudesOfS31();

            Parameters parametersClone = (Parameters)parameters.Clone();
            List<float> sortedPointListOfS31 = parametersClone.GetListOfMagnitudesOfS31();
            sortedPointListOfS31.Sort();

            float maxValue = sortedPointListOfS31[sortedPointListOfS31.Count - 1];
            float minValue = sortedPointListOfS31[0];

            float markpointX = plot.SecondPointX / 4;
            float markpointY = plot.SecondPointY / 4;

            canvas.DrawText("|S31|", markpointX, markpointY, plot.TextPaint);


            // Помечаем нулевую отметку на оси Y
            canvas.DrawLine(plot.FirstPointX, plot.GetCenterPointOfYAxis(), plot.SecondPointX, plot.GetCenterPointOfYAxis(), plot.TextPaint);

            //float coef = 1.62f;
            float coef = 1f;

            float counter = 0;
            while (counter <= plot.SecondPointX)
            {
                float x = plot.FirstPointX;
                x += counter;

                try
                {
                    float y = plot.GetCenterPointOfYAxis();
                    y += magnitudePointListOfS31[Convert.ToInt32(counter)] * 1;
                    canvas.DrawPoint(x, y, plot.RedPaint);

                    if (magnitudePointListOfS31[Convert.ToInt32(counter)] == maxValue)
                    {
                        canvas.DrawLine(x, y, plot.FirstPointX, y, plot.GrayPaint);
                    }

                    if (magnitudePointListOfS31[Convert.ToInt32(counter)] == minValue)
                    {
                        canvas.DrawLine(x, y, plot.FirstPointX, y, plot.GrayPaint);
                    }

                    counter += 0.04f;
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

        }
    }
}
