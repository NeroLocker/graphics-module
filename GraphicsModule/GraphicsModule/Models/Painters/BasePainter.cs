using System;

namespace GraphicsModule.Models.Painters
{
    /// <summary>
    /// Базовый отрисовщик графиков.
    /// </summary>
    public class BasePainter
    {
        /// <summary>
        /// Возвращает коэффициент масштабирования для Y-точек.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        protected float GetYScalingFactor(Parameters parameters, RestrictiveFrame frame)
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
        /// Возвращает коэффициент масштабирования для X-точек.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        protected float GetXScalingFactor(Parameters parameters, RestrictiveFrame frame)
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
    }
}
