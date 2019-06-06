﻿using System;
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
        public PlotType Type
        {
            get; private set;
        }

        /// <summary>
        /// Рамка.
        /// </summary>
        public Frame Frame { get; private set; }

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
        public SKPaint Paint { get; private set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип графика.</param>
        /// <param name="paint">Краска для линий.</param>
        /// <param name="frame">Рамка.</param>
        /// <param name="lineThickness">Толщина линий.</param>
        public Plot(PlotType type, SKPaint paint, Frame frame, float lineThickness)
        {
            Type = type;
            Paint = paint;

            Frame = frame;
            FirstPointX = Frame.GetFirstPointX();
            FirstPointY = Frame.GetFirstPointY();
            // Странно работает здесь
            SecondPointX = Frame.GetFirstPointX() + Frame.GetSecondPointX();
            SecondPointY = Frame.GetFirstPointY() + Frame.GetSecondPointY();

            LineThickness = lineThickness;
        }
    }
}