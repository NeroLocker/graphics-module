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
        private Parameters _parameters;

        public void Paint(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            _canvas = canvas;
            _coordinates = coordinates;
            _frame = frame;
            _keeper = new PaintsKeeper();
            _parameters = parameters;
            DrawXMarks();
            DrawYMarks();
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

        private void DrawYMarks()
        {
            List<float> pointListOfS31 = _parameters.GetListOfPhasesOfS31();

            pointListOfS31.Sort();
            float maximumValueOfS31 = pointListOfS31[pointListOfS31.Count - 1];
            float minimumValueOfS31 = pointListOfS31[0];

            // отрисовка
            float margin = 0.4f;

            float shift = _frame.GetFirstPointX() - _frame.GetFirstPointX() * margin;

            // 0
            float centerOfYAxis = (_frame.GetFirstPointY() + _frame.GetSecondPointY()) / 2f;
            _canvas.DrawText("0", shift, centerOfYAxis, _keeper.paints["Text Paint"]);

            // max
            _canvas.DrawText($"{minimumValueOfS31.ToString("#")}", shift, centerOfYAxis + minimumValueOfS31, _keeper.paints["Text Paint"]);
            //_canvas.DrawLine(_frame.GetFirstPointX(), centerOfYAxis + minimumValueOfS31, _frame.GetFirstPointY() + _frame.GetSecondPointY(), centerOfYAxis + minimumValueOfS31, _frame.Paint);


            // min
            _canvas.DrawText($"{maximumValueOfS31.ToString("#")}", shift, centerOfYAxis + maximumValueOfS31, _keeper.paints["Text Paint"]);

            // Название
            shift -= _frame.GetFirstPointX() * margin;
            SKPath path = new SKPath();
            path.MoveTo(shift, centerOfYAxis);
            path.LineTo(shift, _frame.GetFirstPointY());
            _canvas.DrawTextOnPath($"{_coordinates.NameOfYAxis}", path, 0, 0, _keeper.paints["Text Paint"]);
            path.Close();
        }
    }
}
