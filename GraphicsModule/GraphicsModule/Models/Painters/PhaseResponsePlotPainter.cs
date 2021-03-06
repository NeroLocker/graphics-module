﻿using SkiaSharp;
using System;
using GraphicsModule.Interfaces;

namespace GraphicsModule.Models.Painters
{
    /// <summary>
    /// Отрисовщик фазово-частотной характеристики.
    /// </summary>
    public class PhaseResponsePlotPainter : BasePainter, IPlotPainter
    {
        /// <summary>
        /// Рисует график.
        /// </summary>
        /// <param name="plot">График.</param>
        /// <param name="parameters">Параметры.</param>
        /// <param name="canvas">Холст.</param>
        public void Paint(Plot plot, Parameters parameters, SKCanvas canvas)
        {
            int counter = 0;
            float shift = 0.1f;
            foreach (ParameterType currentType in (ParameterType[])Enum.GetValues(typeof(ParameterType)))
            {
                SKPaint currentPaint = PaintsKeeper.paintsListForPlots[counter];
                PaintSParameter(currentType, currentPaint, plot, parameters, canvas);
                canvas.DrawText($"{currentType}", plot.Frame.GetFirstPointX() + plot.Frame.GetSecondPointX() * shift, plot.Frame.GetFirstPointY() + plot.Frame.GetSecondPointY() * 0.05f, currentPaint);
                counter += 1;
                shift += 0.1f;
            }
        }

        /// <summary>
        /// Рисует ФЧХ для параметра.
        /// </summary>
        /// <param name="plot"></param>
        /// <param name="parameters"></param>
        /// <param name="canvas"></param>
        private void PaintSParameter(ParameterType type, SKPaint paint, Plot plot, Parameters parameters, SKCanvas canvas)
        {
            // цикл для расчета коэффициента масштабирования координат X
            RestrictiveFrame frame = plot.Frame;
            float scalingFactor = base.GetXScalingFactor(parameters, frame);

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
                float currentPhase = (float)parameters.GetPhase(type, x);
                y += -(currentPhase);

                if (plot.FirstPointY + y >= plot.SecondPointY)
                {
                    i += 0.04f;
                    j += 0.04f;
                    continue;
                }
                canvas.DrawPoint(plot.FirstPointX + x * scalingFactor, y, paint);

                i += 0.04f;
                j += 0.04f;
            }
        }
    }
}
