﻿using SkiaSharp;


namespace GraphicsModule.Models
{
    /// <summary>
    /// График.
    /// </summary>
    public class Plot
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public PlotType Type
        {
            get; private set;
        }

        /// <summary>
        /// Рамка.
        /// </summary>
        public RestrictiveFrame Frame { get; private set; }

        /// <summary>
        /// Первая точка оси X из диапазона точек, где определен график.
        /// </summary>
        public float FirstPointX { get; private set; }

        /// <summary>
        /// Первая точка оси Y из диапазона точек, где определен график.
        /// </summary>
        public float FirstPointY { get; private set; }

        /// <summary>
        /// Последняя точка оси X из диапазона точек, где определен график.
        /// </summary>
        public float SecondPointX { get; private set; }

        /// <summary>
        /// Последняя точка оси Y из диапазона точек, где определен график.
        /// </summary>
        public float SecondPointY { get; private set; }

        /// <summary>
        /// Толщина линий.
        /// </summary>
        public float LineThickness { get; private set; }

        /// <summary>
        /// Краска, используемая для отрисовки линий.
        /// </summary>
        public SKPaint RedPaint { get; private set; }

        /// <summary>
        /// Краска, используемая для отрисовки линий.
        /// </summary>
        public SKPaint BluePaint { get; private set; }

        /// <summary>
        /// Краска, используемая для отрисовки линий.
        /// </summary>
        public SKPaint GrayPaint { get; private set; }

        /// <summary>
        /// Краска для текста.
        /// </summary>
        public SKPaint TextPaint { get; private set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип графика.</param>
        /// <param name="paint">Краска для линий.</param>
        /// <param name="frame">Рамка.</param>
        /// <param name="lineThickness">Толщина линий.</param>
        public Plot(PlotType type, RestrictiveFrame frame, float lineThickness)
        {
            Type = type;
            Frame = frame;
            FirstPointX = Frame.GetFirstPointX();
            FirstPointY = Frame.GetFirstPointY();
            SecondPointX = Frame.GetSecondPointX();
            SecondPointY = Frame.GetSecondPointY();

            RedPaint = PaintsKeeper.paints["Red Paint"];
            BluePaint = PaintsKeeper.paints["Blue Paint"];
            GrayPaint = PaintsKeeper.paints["Gray Paint"];
            TextPaint = PaintsKeeper.paints["Text Paint"];
        }

        /// <summary>
        /// Возвращает центральную точку по оси Y.
        /// </summary>
        /// <returns></returns>
        public float GetCenterPointOfYAxis()
        {
            return (FirstPointY + SecondPointY) / 2;
        }
    }
}
