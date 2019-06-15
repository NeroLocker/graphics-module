using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Interfaces;
using GraphicsModule.Models;
using SkiaSharp;

namespace GraphicsModule.Models.Painters
{   
    /// <summary>
    /// Отрисовщик координат для фазово-частотной характеристики.
    /// </summary>
    public class PhaseCoordinatesPainter : ICoordinatesPainter
    {
        private SKCanvas _canvas;
        private Coordinates _coordinates;
        private RestrictiveFrame _frame;
        private PaintsKeeper _keeper;

        public void Paint(Coordinates coordinates, RestrictiveFrame frame, SKCanvas canvas)
        {
            _canvas = canvas;
            _coordinates = coordinates;
            _frame = frame;
            _keeper = new PaintsKeeper();
            DrawXMarks();
        }

        private void DrawXMarks()
        {
            var numberOfMarks = Math.Abs(_coordinates.StartPointOfXAxis) + Math.Abs(_coordinates.EndPointOfXAxis);
            var secondPointX = _frame.GetFirstPointX() + _frame.GetSecondPointX();
            var secondPointY = _frame.GetFirstPointY() + _frame.GetSecondPointY();   
            var step = (secondPointX - _frame.GetFirstPointX()) / numberOfMarks;

            float currentPoint = _frame.GetFirstPointX();
            float startPoint = currentPoint;
            float endPoint = secondPointX;

            float margin = 0.05f;
            float markPoint = secondPointY;
            markPoint += markPoint * margin;

            for (float i = _coordinates.StartPointOfXAxis; i <= _coordinates.EndPointOfXAxis; i++)
            {
                if (i == (Math.Round(_coordinates.EndPointOfXAxis / 2)))
                {
                    string nameOfXAxis = _coordinates.NameOfXAxis;
                    float pointForNameOfAxis = markPoint + markPoint * margin;
                    _canvas.DrawText($"{nameOfXAxis}", currentPoint, pointForNameOfAxis, _keeper.paints["Text Paint"]);
                }

                _canvas.DrawText($"{i}", currentPoint, markPoint, _keeper.paints["Text Paint"]);
                currentPoint += step;
            }            
        }
    }
}
