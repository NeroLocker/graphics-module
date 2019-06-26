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

            double i = 0;
            float point = 0;
            while (Convert.ToInt32(point) != Convert.ToInt32(frame.GetSecondPointX()))
            {
                float x = (float)(parameters.Fmax);
                point = frame.GetFirstPointX() + x * scalingFactor;
                scalingFactor += 0.01f;
            }

            return scalingFactor;
        }

        private void DrawXMarks(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            PaintsKeeper keeper = new PaintsKeeper();

            float xScalingFactor = GetXScalingFactor(parameters, frame);

            float margin = 0.05f;
            byte quantityOfIterations = 0;
            float number = 0;
            float j = (float)parameters.Fmin;

            double i = 0;
            i = frame.GetFirstPointX();
            while (i <= frame.GetSecondPointX())
            {
                if (Convert.ToInt32(j) == Convert.ToInt32(parameters.Fmax))
                {
                    break;
                }

                float x = j * xScalingFactor;
                x += frame.GetFirstPointX();
         
                // Через каждую 25-ю итерацию - целое число.
                if (quantityOfIterations == 25)
                {
                    number = Convert.ToInt32(j);
                    canvas.DrawText($"{number}", x, frame.GetSecondPointY() + frame.GetSecondPointY() * margin, keeper.paints["Text Paint"]);
                    quantityOfIterations = 0;
                }                

                i += 0.04f;
                j += 0.04f;
                quantityOfIterations += 1;
            }
        }

        private void DrawYMarks(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            PaintsKeeper keeper = new PaintsKeeper();

            // отрисовка
            float margin = 0.6f;

            float shift = frame.GetFirstPointX() - frame.GetFirstPointX() * margin;

            // 0
            float centerOfYAxis = frame.GetFirstPointY() + frame.GetHeight() / 2f;
            canvas.DrawText("0", shift, frame.GetCenterPointY(), keeper.paints["Text Paint"]);
            canvas.DrawLine(frame.GetFirstPointX(), centerOfYAxis, frame.GetSecondPointX(), centerOfYAxis, frame.Paint);

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
