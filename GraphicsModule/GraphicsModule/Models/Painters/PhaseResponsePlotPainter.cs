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

        private List<float> _phasePointListOfS21 = new List<float>();
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

            _phasePointListOfS21 = parameters.GetListOfPhasesOfS21();
            _phasePointListOfS31 = parameters.GetListOfPhasesOfS31();

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
                    float y = plot.GetCenterPointOfYAxis();
                    y += _phasePointListOfS21[Convert.ToInt32(counter)] * 1;
                    _canvas.DrawPoint(x, y, plot.BluePaint);

                    y = plot.GetCenterPointOfYAxis();
                    y += _phasePointListOfS31[Convert.ToInt32(counter)] * 1;
                    _canvas.DrawPoint(x, y, plot.RedPaint);
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
