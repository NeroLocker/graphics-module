﻿using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModule
{
    /// <summary>
    /// Рабочее пространство.
    /// </summary>
    public class WorkingSpace
    {
        /// <summary>
        /// Отрисовщик графиков.
        /// </summary>
        public IPlotPainter PlotPainter { get; set;}

        /// <summary>
        /// Отрисовщик рамок.
        /// </summary>
        public IFramePainter FramePainter { get; set;}

        /// <summary>
        /// Холст.
        /// </summary>
        private SKCanvas _canvas;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="plotPainter">Отрисовщик графика.</param>
        /// <param name="framePainter">Отрисовщик рамки.</param>
        /// <param name="canvas">Холст.</param>
        public WorkingSpace(IPlotPainter plotPainter, IFramePainter framePainter, SKCanvas canvas)
        {
            PlotPainter = plotPainter;
            FramePainter = framePainter;
            _canvas = canvas;
        }

        /// <summary>
        /// Рисует график.
        /// </summary>
        /// <param name="plot">График.</param>
        public void PaintPlot(Plot plot)
        {
            PlotPainter.Paint(plot, _canvas);
        }

        /// <summary>
        /// Рисует рамку.
        /// </summary>
        /// <param name="frame">Рамка.</param>
        public void PaintFrame(Frame frame)
        {
            FramePainter.Paint(frame, _canvas);
        }
    }
}
