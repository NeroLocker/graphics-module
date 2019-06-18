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
        private SKCanvas _canvas;
        private Plot _plot;
        private Parameters _parameters;

        //private List<float> _phasePointListOfS21 = new List<float>();
        private List<float> _phasePointListOfS31 = new List<float>();

        /// <summary>
        /// Рисует график.
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

            _phasePointListOfS31 = parameters.GetListOfPhasesOfS31();

            List<float> sortedPointListOfS31 = _phasePointListOfS31;
            sortedPointListOfS31.Sort();
            float maxValue = sortedPointListOfS31[sortedPointListOfS31.Count - 1];
            float minValue = sortedPointListOfS31[0];

            PaintsKeeper keeper = new PaintsKeeper();

            SKPaint bluePaint = keeper.paints["Blue Paint"];
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

                    
                    y += _phasePointListOfS31[Convert.ToInt32(counter)];
                    _canvas.DrawPoint(x, y, _plot.RedPaint);

                    if (_phasePointListOfS31[Convert.ToInt32(counter)] == maxValue)
                    {
                        _canvas.DrawLine(x, y, _plot.FirstPointX, y, _plot.GrayPaint);
                    }

                    if (_phasePointListOfS31[Convert.ToInt32(counter)] == minValue)
                    {
                        _canvas.DrawLine(x, y, _plot.FirstPointX, y, _plot.GrayPaint);
                    }
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
