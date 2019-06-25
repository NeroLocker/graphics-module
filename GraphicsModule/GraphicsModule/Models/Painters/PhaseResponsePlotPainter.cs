using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Interfaces;
using GraphicsModule.Models;
using System.Numerics;

namespace GraphicsModule.Models.Painters
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
        /// <param name="parameters">Параметры.</param>
        /// <param name="canvas">Холст.</param>
        public void Paint(Plot plot, Parameters parameters, SKCanvas canvas)
        {
            //// ось X
            //List<float> phasePointListOfS31 = parameters.GetListOfPhasesOfS31();

            //Parameters parametersClone = (Parameters)parameters.Clone();
            //List<float> sortedPointListOfS31 = parametersClone.GetListOfPhasesOfS31();
            //sortedPointListOfS31.Sort();

            //float maxValue = sortedPointListOfS31[sortedPointListOfS31.Count - 1];
            //float minValue = sortedPointListOfS31[0];

            //float coef = 1.62f;

            //float markpointX = plot.SecondPointX / 4;
            //float markpointY = plot.SecondPointY / 4;

            //canvas.DrawText("|S31|", markpointX, markpointY, plot.TextPaint);

            //float counter = 1;
            //while (counter <= plot.SecondPointX)
            //{
            //    float x = plot.FirstPointX;
            //    x += counter * coef;

            //    try
            //    {
            //        float y = plot.GetCenterPointOfYAxis();

            //        y += phasePointListOfS31[Convert.ToInt32(counter)];
            //        canvas.DrawPoint(x, y, plot.RedPaint);

            //        if (phasePointListOfS31[Convert.ToInt32(counter)] == maxValue)
            //        {
            //            canvas.DrawLine(x, y, plot.FirstPointX, y, plot.GrayPaint);
            //        }

            //        if (phasePointListOfS31[Convert.ToInt32(counter)] == minValue)
            //        {
            //            canvas.DrawLine(x, y, plot.FirstPointX, y, plot.GrayPaint);
            //        }
            //        counter += 0.04f;
            //    }
            //    catch (ArgumentOutOfRangeException e)
            //    {
            //        break;
            //    }
            //}
        }
    }
}
