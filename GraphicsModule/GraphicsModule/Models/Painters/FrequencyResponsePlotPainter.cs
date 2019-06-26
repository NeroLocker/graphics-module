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
            PaintsKeeper keeper = new PaintsKeeper();
            int counter = 0;
            foreach (ParameterType currentType in (ParameterType[])Enum.GetValues(typeof(ParameterType)))
            {
                SKPaint currentPaint = keeper.paintsListForPlots[counter];
                PaintSParameter(currentType, currentPaint, plot, parameters, canvas);
                counter += 1;
            }
                                         
        }

        /// <summary>
        /// Рисует АЧХ для параметра.
        /// </summary>
        /// <param name="plot"></param>
        /// <param name="parameters"></param>
        /// <param name="canvas"></param>
        private void PaintSParameter(ParameterType type, SKPaint paint, Plot plot, Parameters parameters, SKCanvas canvas)
        {
            // цикл для расчета коэффициента масштабирования координат X
            RestrictiveFrame frame = plot.Frame;
            float scalingFactor = GetXScalingFactor(parameters, frame);

            double i = plot.Frame.GetFirstPointX();
            float j = (float)parameters.Fmin;
            while (j * scalingFactor <= parameters.Fmax * scalingFactor)
            {
                float x = j;
                float y = plot.GetCenterPointOfYAxis();

                // Встречаем отрицательную бесконечность на 1 шаге.
                if (parameters.GetMagnitude(ParameterType.S12, x) == Double.NegativeInfinity)
                {
                    i += 0.04f;
                    j += 0.04f;
                    continue;
                }

                // Инвертирование значений.
                float currentMagnitude = (float)parameters.GetMagnitude(type, x);
                y += -(currentMagnitude);
                canvas.DrawPoint(plot.FirstPointX + x * scalingFactor, y, paint);

                i += 0.04f;
                j += 0.04f;
            }

            int b = 0;
        }

        /// <summary>
        /// Возвращает коэффициент масштабирования для X-точек.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        private float GetXScalingFactor(Parameters parameters, RestrictiveFrame frame)
        {
            // Ненулевой
            float scalingFactor = 0.01f;

            float point = 0;
            while (Convert.ToInt32(point) != Convert.ToInt32(frame.GetSecondPointX()))
            {
                float x = (float)(parameters.Fmax);
                point = frame.GetFirstPointX() + x * scalingFactor;
                scalingFactor += 0.01f;
            }

            return scalingFactor;
        }
    }
}
