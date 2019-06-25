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
            List<float> magnitudePointListOfS12 = new List<float>();
            List<float> sortedPointListOfS12 = new List<float>();


            float counter1 = 0;
            while(counter1 < parameters.Fmax)
            {
                magnitudePointListOfS12.Add(parameters.GetMagnitude(ParameterType.S12, counter1));
                counter1 += 0.04f;
            }
            for (float i = (float)parameters.Fmin; i < (float)parameters.Fmax; i++)
            {
                
            }

            Parameters parametersClone = (Parameters)parameters.Clone();
            for (float i = (float)parameters.Fmin; i < (float)parameters.Fmax; i++)
            {
                sortedPointListOfS12.Add(parametersClone.GetMagnitude(ParameterType.S12, i));
            }
            sortedPointListOfS12.Sort();

            float maxValue = sortedPointListOfS12[sortedPointListOfS12.Count - 1];
            float minValue = sortedPointListOfS12[0];

            float markpointX = plot.SecondPointX / 4;
            float markpointY = plot.SecondPointY / 4;

            canvas.DrawText("|S12|", markpointX, markpointY, plot.TextPaint);


            // Помечаем нулевую отметку на оси Y
            canvas.DrawLine(plot.FirstPointX, plot.GetCenterPointOfYAxis(), plot.SecondPointX, plot.GetCenterPointOfYAxis(), plot.TextPaint);

            float counter = 0;
            while (counter <= plot.SecondPointX)
            {
                float x = plot.FirstPointX;
                x += counter;

                try
                {
                    float y = plot.GetCenterPointOfYAxis();
                    y += magnitudePointListOfS12[Convert.ToInt32(counter)];
                    canvas.DrawPoint(x, y, plot.RedPaint);

                    //if (magnitudePointListOfS12[Convert.ToInt32(counter)] == maxValue)
                    //{
                    //    canvas.DrawLine(x, y, plot.FirstPointX, y, plot.GrayPaint);
                    //}

                    //if (magnitudePointListOfS12[Convert.ToInt32(counter)] == minValue)
                    //{
                    //    canvas.DrawLine(x, y, plot.FirstPointX, y, plot.GrayPaint);
                    //}

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
