using System;
using GraphicsModule.Interfaces;
using SkiaSharp;

namespace GraphicsModule.Models.Painters
{   
    /// <summary>
    /// Отрисовщик координат для фазово-частотной характеристики.
    /// </summary>
    public class PhaseCoordinatesPainter : BasePainter, ICoordinatesPainter
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
                    // 0
                    canvas.DrawText("0", shiftPoint, frame.GetCenterPointY(), PaintsKeeper.paints["Text Paint"]);
                    canvas.DrawLine(frame.GetFirstPointX(), frame.GetCenterPointY(), frame.GetSecondPointX(), frame.GetCenterPointY(), frame.Paint);

                    // положительная часть оси Y
                    float valueToShow = 0;
                    float currentPoint = frame.GetCenterPointY();
                    while (currentPoint >= frame.GetFirstPointY())
                    {
                        canvas.DrawText($"{valueToShow.ToString("#")}", shiftPoint, currentPoint, PaintsKeeper.paints["Text Paint"]);
                        // grid
                        canvas.DrawLine(frame.GetFirstPointX(), currentPoint, frame.GetSecondPointX(), currentPoint, frame.GridPaint);
                        valueToShow += 50;
                        currentPoint -= 50;
                    }

                    // отрицательная часть оси Y
                    valueToShow = 0;
                    currentPoint = frame.GetCenterPointY();
                    while (currentPoint <= frame.GetSecondPointY())
                    {
                        canvas.DrawText($"{valueToShow.ToString("#")}", shiftPoint, currentPoint, PaintsKeeper.paints["Text Paint"]);
                        // grid
                        canvas.DrawLine(frame.GetFirstPointX(), currentPoint, frame.GetSecondPointX(), currentPoint, frame.GridPaint);
                        valueToShow -= 50;
                        currentPoint += 50;
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
