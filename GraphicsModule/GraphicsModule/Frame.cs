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
    }
}
