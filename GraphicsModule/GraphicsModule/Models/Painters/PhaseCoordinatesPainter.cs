using System;
using GraphicsModule.Interfaces;
using SkiaSharp;

namespace GraphicsModule.Models.Painters
{   
    /// <summary>
    /// Отрисовщик координат для фазово-частотной характеристики.
    /// </summary>
    public class PhaseCoordinatesPainter : ICoordinatesPainter
    {
        private float _margin = 0.05f;

        /// <summary>
        /// Рисует координаты.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <param name="canvas"></param>
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

            float point = 0;
            while (Convert.ToInt32(point) != Convert.ToInt32(frame.GetSecondPointX()))
            {
                float x = (float)(parameters.Fmax);
                point = frame.GetFirstPointX() + x * scalingFactor;
                scalingFactor += 0.01f;
            }

            return scalingFactor;
        }

        /// <summary>
        /// Рисует координаты по X.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <param name="canvas"></param>
        private void DrawXMarks(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            float xScalingFactor = GetXScalingFactor(parameters, frame);

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
                    canvas.DrawText($"{number}", x, frame.GetSecondPointY() + frame.GetSecondPointY() * _margin, PaintsKeeper.paints["Text Paint"]);
                    quantityOfIterations = 0;
                }

                i += 0.04f;
                j += 0.04f;
                quantityOfIterations += 1;
            }

            canvas.DrawText($"{coordinates.NameOfXAxis}", frame.GetCenterPointX(), frame.GetSecondPointY() + 2 * frame.GetSecondPointY() * _margin, PaintsKeeper.paints["Text Paint"]);
        }

        /// <summary>
        /// Рисует координаты по Y.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <param name="canvas"></param>
        private void DrawYMarks(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            // отрисовка
            float shift = frame.GetFirstPointX() - frame.GetSecondPointX() * _margin;

            // 0
            canvas.DrawText("0", shift, frame.GetCenterPointY(), PaintsKeeper.paints["Text Paint"]);
            canvas.DrawLine(frame.GetFirstPointX(), frame.GetCenterPointY(), frame.GetSecondPointX(), frame.GetCenterPointY(), frame.Paint);

            // положительная часть оси Y
            float valueToShow = 0;
            float currentPoint = frame.GetCenterPointY();
            while (currentPoint >= frame.GetFirstPointY())
            {
                canvas.DrawText($"{valueToShow.ToString("#")}", shift, currentPoint, PaintsKeeper.paints["Text Paint"]);
                valueToShow += 50;
                currentPoint -= 50;
            }

            // отрицательная часть оси Y
            valueToShow = 0;
            currentPoint = frame.GetCenterPointY();
            while (currentPoint <= frame.GetSecondPointY())
            {
                canvas.DrawText($"{valueToShow.ToString("#")}", shift, currentPoint, PaintsKeeper.paints["Text Paint"]);
                valueToShow -= 50;
                currentPoint += 50;
            }

            // Название оси
            shift -= frame.GetSecondPointX() * _margin;

            SKPath path = new SKPath();
            path.MoveTo(shift, frame.GetCenterPointY());
            path.LineTo(shift, frame.GetFirstPointY());
            canvas.DrawTextOnPath($"{coordinates.NameOfYAxis}", path, 0, 0, PaintsKeeper.paints["Text Paint"]);
            path.Close();
        }
    }
}
