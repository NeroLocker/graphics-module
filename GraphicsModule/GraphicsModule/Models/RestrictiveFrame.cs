using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace GraphicsModule.Models
{
    /// <summary>
    /// Представляет рамку.
    /// </summary>
    public class RestrictiveFrame
    {
        /// <summary>
        /// Содержит информацию о служебном рабочем пространстве.
        /// </summary>
        private SKImageInfo _info;

        /// <summary>
        /// Краска для линий.
        /// </summary>
        public SKPaint Paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            // Толщина линий
            StrokeWidth = 2
        };


        /// <summary>
        /// Отступ от всех краев служебного рабочего пространства.
        /// </summary>
        private float _margin;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="info">Информация о служебном рабочем пространстве.</param>
        /// <param name="margin">Отступ от всех краев служебного рабочего пространства.</param>
        public RestrictiveFrame(SKImageInfo info, float margin)
        {
            _info = info;
            _margin = margin;
        }

        /// <summary>
        /// Возвращает ширину рамки.
        /// </summary>
        /// <returns>Ширина рамки</returns>
        public float GetWidth()
        {
            float xEnd = GetSecondPointX();
            float xStart = GetFirstPointX();

            return (xEnd - xStart);
        }

        /// <summary>
        /// Возвращает длину рамки.
        /// </summary>
        /// <returns>Длина рамки</returns>
        public float GetHeight()
        {
            float yEnd = GetSecondPointY();
            float yStart = GetFirstPointY();

            return (yEnd - yStart);
        }

        /// <summary>
        /// Считает координату X левой верхней точки.
        /// </summary>
        /// <returns></returns>
        public float GetFirstPointX()
        {
            return (_info.Width * _margin);
        }

        /// <summary>
        /// Считает координату Y левой верхней точки.
        /// </summary>
        /// <returns></returns>
        public float GetFirstPointY()
        {
            return (_info.Height * _margin);
        }

        /// <summary>
        /// Считает координату X правой нижней точки.
        /// </summary>
        /// <returns></returns>
        public float GetSecondPointX()
        {
            var value = (_info.Width * (1 - 2 * _margin));

            return value;
        }

        /// <summary>
        /// Считает координату Y правой нижней точки.
        /// </summary>
        /// <returns></returns>
        public float GetSecondPointY()
        {
            var value = (_info.Height * (1 - 2 * _margin));
            return value;
        }
    }
}
