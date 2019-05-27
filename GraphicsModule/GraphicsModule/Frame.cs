using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace GraphicsModule
{
    /// <summary>
    /// Представляет рамку
    /// </summary>
    class Frame
    {
        /// <summary>
        /// Содержит информацию о рабочем пространстве
        /// </summary>
        private SKImageInfo Info
        {
            get; set;
        }

        /// <summary>
        /// Отступ от всех краев рабочего пространства
        /// </summary>
        private float Margin
        {
            get; set;
        }

        /// <summary>
        /// Координата X левой верхней точки
        /// </summary>
        public float FirstPointX
        {
            get; set;
        }

        /// <summary>
        /// Координата Y левой верхней точки
        /// </summary>
        public float FirstPointY
        {
            get; set;
        }

        /// <summary>
        /// Координата X нижней правой точки
        /// </summary>
        public float SecondPointX
        {
            get; set;
        }

        /// <summary>
        /// Координата Y нижней правой точки
        /// </summary>
        public float SecondPointY
        {
            get; set;
        }

        /// <summary>
        /// Инициализирует свойства
        /// </summary>
        /// <param name="info"></param>
        /// <param name="margin"></param>
        public Frame(SKImageInfo info, float margin)
        {
            Info = info;
            Margin = margin;
        }

        /// <summary>
        /// Считает координату X верхней левой точки
        /// </summary>
        /// <returns></returns>
        private float CalculateFirstPointX()
        {
            return (float)Info.Width * Margin;
        }

        /// <summary>
        /// Считает координату Y верхней левой точки
        /// </summary>
        /// <returns></returns>
        private float CalculateFirstPointY()
        {
            return (float)Info.Height * Margin;
        }

        /// <summary>
        /// Считает координату X нижней правой точки
        /// </summary>
        /// <returns></returns>
        private float CalculateSecondPointX()
        {
            return (float)(Info.Width * (1 - 2 * Margin));
        }

        /// <summary>
        /// Считает координату Y нижней правой точки
        /// </summary>
        /// <returns></returns>
        private float CalculateSecondPointY()
        {
            return (float)(Info.Height * (1 - 2 * Margin));
        }
    }
}
