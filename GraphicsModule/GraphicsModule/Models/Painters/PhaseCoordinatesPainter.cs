using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Interfaces;
using GraphicsModule.Models;
using SkiaSharp;

namespace GraphicsModule.Painters
{   
    /// <summary>
    /// Отрисовщик координат для фазово-частотной характеристики.
    /// </summary>
    public class PhaseCoordinatesPainter : ICoordinatesPainter
    {
        private SKCanvas _canvas;
        private Coordinates _coordinates;
        private RestrictiveFrame _frame;

        public void Paint(Coordinates coordinates, RestrictiveFrame frame, SKCanvas canvas)
        {
            _canvas = canvas;
            _coordinates = coordinates;
            _frame = frame;

            DrawYMarks();
        }

        private void DrawYMarks()
        {            
            var numberOfMarks = Math.Abs(_coordinates.StartPointOfYAxis) + Math.Abs(_coordinates.EndPointOfYAxis);
            var step = _frame.GetHeight() / numberOfMarks;
            float currentPoint = _frame.GetFirstPointY();
            float startPoint = _frame.GetFirstPointY();
            float endPoint = _frame.GetSecondPointY();
            float margin = 0.9f;
            float markPoint = startPoint * margin;

            for (float i = startPoint; i >= endPoint; i--)
            {
                if (i >= numberOfMarks)
                {
                    break;
                }
                currentPoint += step;
                _canvas.DrawText($"{i}", markPoint, currentPoint, _frame.Paint);
            }
        }
    }
}
