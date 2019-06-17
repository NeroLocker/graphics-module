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

        private float _centerPointOfYAxis;

        private List<float> _magnitudePointListOfS21 = new List<float>();
        private List<float> _phasePointListOfS21 = new List<float>();

        private List<float> _magnitudePointListOfS31 = new List<float>();
        private List<float> _phasePointListOfS31 = new List<float>();

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
            _centerPointOfYAxis = (_plot.FirstPointY + _plot.SecondPointY) / 2;

            float coef = 1.62f;

            DrawXAxis();

            float counter = 0;
            while (counter <= 20)
            {
                Complex currentS21 = _parameters.GetS21(counter);
                Complex currentS31 = _parameters.GetS31(counter);

                // Модуль
                float currentMagnitudeOfS21 = (float)(20 * Math.Log10(currentS21.Magnitude));
                float currentMagnitudeOfS31 = (float)(20 * Math.Log10(currentS31.Magnitude));

                // Фаза (аргумент)
                // Для перевода в градусы домножить на 180/Pi
                float currentPhaseOfS21 = (float)(Math.Atan2(currentS21.Imaginary, currentS21.Real) * 180 / Math.PI);
                float currentPhaseOfS31 = (float)(Math.Atan2(currentS31.Imaginary, currentS31.Real) * 180 / Math.PI);

                _magnitudePointListOfS21.Add(currentMagnitudeOfS21);
                _magnitudePointListOfS31.Add(currentMagnitudeOfS31);

                _phasePointListOfS21.Add(currentPhaseOfS21);
                _phasePointListOfS31.Add(currentPhaseOfS31);

                counter += 0.04f;
            }

            // Инвертируем знаки точек относительно оси Y
            List<float> magnitudeInvertedPointListOfS21 = new List<float>();
            foreach (float currentValue in _magnitudePointListOfS21)
            {
                magnitudeInvertedPointListOfS21.Add(-1 * currentValue);
            }

            List<float> magnitudeInvertedPointListOfS31 = new List<float>();
            foreach (float currentValue in _magnitudePointListOfS31)
            {
                magnitudeInvertedPointListOfS31.Add(-1 * currentValue);
            }

            int b = 0;

            float counter2 = 1;
            while (counter2 <= _plot.SecondPointX)
            {
                float x = _plot.FirstPointX;
                x += counter2 * coef;

                try
                {
                    float y = _centerPointOfYAxis;
                    y += magnitudeInvertedPointListOfS31[Convert.ToInt32(counter2)] * 1;
                    _canvas.DrawPoint(x, y, _plot.Paint);

                    y = _centerPointOfYAxis;
                    y+= magnitudeInvertedPointListOfS21[Convert.ToInt32(counter2)] * 1;
                    _canvas.DrawPoint(x, y, _plot.Paint);
                    //_canvas.DrawCircle(x, y, 1f, _plot.Paint);
                    counter2 += 0.04f;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    break;
                }

            }

        }

        private void DrawXAxis()
        {            
            _canvas.DrawLine(_plot.FirstPointX, _centerPointOfYAxis, _plot.SecondPointX, _centerPointOfYAxis, _plot.Paint);
        }
    }
}
