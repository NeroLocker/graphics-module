using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Interfaces;
using GraphicsModule.Models;
using SkiaSharp;

namespace GraphicsModule.Models.Painters
{   
    /// <summary>
    /// Отрисовщик координат для амплитудно-частотной характеристики.
    /// </summary>
    public class FrequencyCoordinatesPainter : ICoordinatesPainter
    {
        public void Paint(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {         
            DrawXMarks(coordinates, parameters, frame, canvas);
            DrawYMarks(coordinates, parameters, frame, canvas);
        }

        private void DrawXMarks(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            PaintsKeeper keeper = new PaintsKeeper();

            var numberOfMarks = Math.Abs(coordinates.StartPointOfXAxis) + Math.Abs(coordinates.EndPointOfXAxis);
            var secondPointX = frame.GetFirstPointX() + frame.GetSecondPointX();
            var secondPointY = frame.GetFirstPointY() + frame.GetSecondPointY();   
            var step = (secondPointX - frame.GetFirstPointX()) / numberOfMarks;

            float currentPoint = frame.GetFirstPointX();
            float startPoint = currentPoint;
            float endPoint = secondPointX;

            float margin = 0.05f;
            float markPoint = secondPointY;
            markPoint += markPoint * margin;

            for (float i = coordinates.StartPointOfXAxis; i <= coordinates.EndPointOfXAxis; i++)
            {
                if (i == (Math.Round(coordinates.EndPointOfXAxis / 2)))
                {
                    string nameOfXAxis = coordinates.NameOfXAxis;
                    float pointForNameOfAxis = markPoint + markPoint * margin;
                    canvas.DrawText($"{nameOfXAxis}", currentPoint, pointForNameOfAxis, keeper.paints["Text Paint"]);
                }

                canvas.DrawText($"{i}", currentPoint, markPoint, keeper.paints["Text Paint"]);
                currentPoint += step;
            }            
        }

        private void DrawYMarks(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            PaintsKeeper keeper = new PaintsKeeper();

            List<float> pointListOfS31 = parameters.GetListOfMagnitudesOfS31();

            pointListOfS31.Sort();
            float maximumValueOfS31 = pointListOfS31[pointListOfS31.Count - 1];
            float minimumValueOfS31 = pointListOfS31[0];

            // отрисовка
            float margin = 0.6f;

            float shift = frame.GetFirstPointX() - frame.GetFirstPointX() * margin;

            // 0
            float centerOfYAxis = (frame.GetFirstPointY() + frame.GetSecondPointY()) / 2f;
            canvas.DrawText("0", shift, centerOfYAxis, keeper.paints["Text Paint"]);

            float valueToShow = 0;
            float currentPoint = centerOfYAxis;
            while (currentPoint >= frame.GetFirstPointY())
            {
                canvas.DrawText($"{valueToShow.ToString("#")}", shift, currentPoint, keeper.paints["Text Paint"]);
                valueToShow -= 30;
                currentPoint -= 30;
            }

            valueToShow = 0;
            currentPoint = centerOfYAxis;
            while (currentPoint <= frame.GetFirstPointY() + frame.GetSecondPointY())
            {
                canvas.DrawText($"{valueToShow.ToString("#")}", shift, currentPoint, keeper.paints["Text Paint"]);
                valueToShow += 30;
                currentPoint += 30;
            }

            float shiftForMaxMinValues = frame.GetFirstPointX() + frame.GetFirstPointX() * 0.3f;
            // max
            canvas.DrawText($"{minimumValueOfS31.ToString("#")}", shiftForMaxMinValues, centerOfYAxis + minimumValueOfS31, keeper.paints["Text Paint"]);
            //canvas.DrawLine(frame.GetFirstPointX(), centerOfYAxis + minimumValueOfS31, frame.GetFirstPointY() + frame.GetSecondPointY(), centerOfYAxis + minimumValueOfS31, frame.Paint);


            // min
            canvas.DrawText($"{maximumValueOfS31.ToString("#")}", shiftForMaxMinValues, centerOfYAxis + maximumValueOfS31, keeper.paints["Text Paint"]);

            // Название
            shift -= frame.GetFirstPointX() * 0.15f;
            SKPath path = new SKPath();
            path.MoveTo(shift, centerOfYAxis);
            path.LineTo(shift, frame.GetFirstPointY());
            canvas.DrawTextOnPath($"{coordinates.NameOfYAxis}", path, 0, 0, keeper.paints["Text Paint"]);
            path.Close();
        }
    }
}
