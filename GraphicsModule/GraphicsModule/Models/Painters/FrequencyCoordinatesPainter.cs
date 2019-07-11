using System;
using GraphicsModule.Interfaces;
using SkiaSharp;

namespace GraphicsModule.Models.Painters
{   
    /// <summary>
    /// Отрисовщик координат для амплитудно-частотной характеристики.
    /// </summary>
    public class FrequencyCoordinatesPainter : BasePainter, ICoordinatesPainter
    {
        private float _margin = 0.05f;

        /// <summary>
        /// Перечисление для типа координат.
        /// </summary>
        private enum TypeOfAxis
        {
            X,
            Y
        };

        /// <summary>
        /// Рисует координаты.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <param name="canvas"></param>
        public void Paint(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {         
            foreach (TypeOfAxis type in Enum.GetValues(typeof(TypeOfAxis)))
            {
                DrawMarks(type, coordinates, parameters, frame, canvas);
            }
        }

        /// <summary>
        /// Рисует определенные координаты.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <param name="canvas"></param>
        private void DrawMarks(TypeOfAxis type, Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas)
        {
            float xScalingFactor = base.GetXScalingFactor(parameters, frame);
            float yScalingFactor = base.GetYScalingFactor(parameters, frame);

            // Смещенная точка.
            float shiftPoint = frame.GetFirstPointX() - frame.GetSecondPointX() * _margin;

            byte quantityOfIterations = 0;
            float number = 0;
            float j = (float)parameters.Fmin;

            double i = 0;

            switch (type)
            {
                case TypeOfAxis.X:
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
                            // grid
                            canvas.DrawLine(x, frame.GetFirstPointY(), x, frame.GetSecondPointY(), frame.GridPaint);
                            quantityOfIterations = 0;
                        }

                        i += 0.04f;
                        j += 0.04f;
                        quantityOfIterations += 1;
                    }
                    canvas.DrawText($"{coordinates.NameOfXAxis}", frame.GetCenterPointX(), frame.GetSecondPointY() + 2 * frame.GetSecondPointY() * _margin, PaintsKeeper.paints["Text Paint"]);

                    break;
                case TypeOfAxis.Y:
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
                            canvas.DrawText($"{-number}", shiftPoint, y, PaintsKeeper.paints["Text Paint"]);
                            // grid
                            canvas.DrawLine(frame.GetFirstPointX(), y, frame.GetSecondPointX(), y, frame.GridPaint);
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
                    canvas.DrawTextOnPath($"{coordinates.NameOfYAxis}", path, 0, 0, PaintsKeeper.paints["Text Paint"]);
                    path.Close();

                    break;
            }
        }
    }
}
