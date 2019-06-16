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
            List<float> phasePointList = new List<float>();
            List<Complex> complexList = new List<Complex>();
            Dictionary<float, float> xandydic = new Dictionary<float, float>();

            float counter = 0;
            while (counter <= 20)
            {
                Complex S14 = parameters.GetS14(counter);
                complexList.Add(S14);
                float currentMagnitude = (float)(20 * Math.Log10(S14.Magnitude));
                float currentPhase = (float)(Math.Atan2(S14.Imaginary, S14.Real) * 180/Math.PI);
                xandydic.Add(counter, currentMagnitude);
                phasePointList.Add(currentPhase);
                counter += 0.04f;
            }



            //float counter = 0;
            //while (counter <= 20)
            //{
            //    Complex S21 = parameters.GetS21(counter);
            //    complexList.Add(S21);
            //    float value = (float)(20 * Math.Log10(S21.Magnitude));
            //    value += 0;
            //    pointList.Add(value);
            //    counter += 0.04f;
            //}

            int b = 0;
            //float counter2 = 0.8f;

            //while (counter2 <= plot.SecondPointX)
            //{
            //    float x = counter2 * 8;
            //    try
            //    {
            //        float y = pointList[Convert.ToInt32(counter2)] * 8;
            //        canvas.DrawCircle(x, y, 1f, plot.Paint);
            //        counter2 += 0.04f;
            //    }
            //    catch (ArgumentOutOfRangeException e)
            //    {
            //        break;
            //    }
            //}
        }
    }
}
