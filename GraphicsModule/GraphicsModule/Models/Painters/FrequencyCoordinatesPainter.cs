using System;
using GraphicsModule.Interfaces;
using SkiaSharp;

namespace GraphicsModule.Models.Painters
{   
    /// <summary>
    /// Отрисовщик координат для амплитудно-частотной характеристики.
    /// </summary>
    public class FrequencyCoordinatesPainter : ICoordinatesPainter
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
        /// Возвращает коэффициент масштабирования для Y-точек.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        private float GetYScalingFactor(Parameters parameters, RestrictiveFrame frame)
        {
            // Ненулевой
            float scalingFactor = 0.01f;

            float point = 0;
            while (Convert.ToInt32(point) != Convert.ToInt32(frame.GetSecondPointY()))
            {
                // Здесь макс значение по y
                float y = (float)(30);

                point = frame.GetFirstPointY() + y * scalingFactor;
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
            PaintsKeeper keeper = new PaintsKeeper();

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
                    canvas.DrawText($"{number}", x, frame.GetSecondPointY() + frame.GetSecondPointY() * _margin, keeper.paints["Text Paint"]);
                    quantityOfIterations = 0;
                }                

                i += 0.04f;
                j += 0.04f;
                quantityOfIterations += 1;
            }

            canvas.DrawText($"{coordinates.NameOfXAxis}", frame.GetCenterPointX(), frame.GetSecondPointY() + 2 * frame.GetSecondPointY() * _margin, keeper.paints["Text Paint"]);
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
            float yScalingFactor = GetYScalingFactor(parameters, frame);

            PaintsKeeper keeper = new PaintsKeeper();
            // Смещенная точка.
            float shiftPoint = frame.GetFirstPointX() - frame.GetSecondPointX() * _margin;

            // 0
            //canvas.DrawText("0", shiftPoint, frame.GetCenterPointY(), keeper.paints["Text Paint"]);
            //canvas.DrawLine(frame.GetFirstPointX(), frame.GetCenterPointY(), frame.GetSecondPointX(), frame.GetCenterPointY(), frame.Paint);

            byte quantityOfIterations = 0;
            float number = 0;
            float j = 0;

            double i = 0;
            i = frame.GetFirstPointY();
            while (i <= frame.GetSecondPointY())
            {
                if (Convert.ToInt32(j) == Convert.ToInt32(30))
                {
                    break;
                }

                float y = j * yScalingFactor;
                y += frame.GetFirstPointY();

                
                // Через каждую 125-ю итерацию - 5 целых чисел.
                if (quantityOfIterations == 125)
                {
                    number = Convert.ToInt32(j);
                    canvas.DrawText($"{-number}", shiftPoint, y, keeper.paints["Text Paint"]);
                    quantityOfIterations = 0;
                }

                i += 0.04f;
                j += 0.04f;
                quantityOfIterations += 1;
            }

            // Название оси
            shiftPoint -= frame.GetSecondPointX() * _margin;

            SKPath path = new SKPath();
            path.MoveTo(shiftPoint, frame.GetCenterPointY());
            path.LineTo(shiftPoint, frame.GetFirstPointY());
            canvas.DrawTextOnPath($"{coordinates.NameOfYAxis}", path, 0, 0, keeper.paints["Text Paint"]);
            path.Close();
        }
    }
}
