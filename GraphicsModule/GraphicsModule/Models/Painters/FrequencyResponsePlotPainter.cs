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
        private SKCanvas _canvas;
        private Plot _plot;
        private Parameters _parameters;

        private List<float> _magnitudePointListOfS21 = new List<float>();

        private List<float> _magnitudePointListOfS31 = new List<float>();

        /// <summary>
        /// Рисует амплитудно-частотную характеристику.
        /// </summary>
        /// <param name="plot">График.</param>
        /// <param name="parameters">Параметры.</param>
        /// <param name="canvas">Холст.</param>
        public void Paint(Plot plot, Parameters parameters, SKCanvas canvas)
        {
            // ось X
            _plot = plot;
            _parameters = parameters;
            _canvas = canvas;

            //_magnitudePointListOfS21 = parameters.GetListOfMagnitudesOfS21();
            _magnitudePointListOfS31 = parameters.GetListOfMagnitudesOfS31();

            PaintsKeeper keeper = new PaintsKeeper();

            SKPaint redPaint = keeper.paints["Red Paint"];

            float coef = 1.62f;

            int b = 0;

            float counter = 1;
            while (counter <= _plot.SecondPointX)
            {
                float x = _plot.FirstPointX;
                x += counter * coef;

                try
                {
                    float y = _plot.GetCenterPointOfYAxis();
                    y+= _magnitudePointListOfS31[Convert.ToInt32(counter)] * 1;
                    _canvas.DrawPoint(x, y, _plot.RedPaint);
                    counter += 0.04f;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    break;
                }

            }

        }

    }
}
