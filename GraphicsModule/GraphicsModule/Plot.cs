using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;


namespace GraphicsModule
{
    /// <summary>
    /// График.
    /// </summary>
    public class Plot
    {
        /// <summary>
        /// Имя.
        /// </summary>
        private string _name;

        /// <summary>
        /// Имя.
        /// </summary>
        private string Name
        {
            get => _name;
            set
            {
                if ((value != "Frequency Response") || (value != "Phase Response"))
                {
                    throw new ArgumentException("This name is forbidden");
                }
                else
                {
                    _name = value;
                }

            }
        }

        /// <summary>
        /// Краска, используемая для отрисовки линий.
        /// </summary>
        public SKPaint Paint { get; private set;}

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paint"></param>
        public Plot(string name, SKPaint paint)
        {
            Name = name;
            Paint = paint;
        }
    }
}
