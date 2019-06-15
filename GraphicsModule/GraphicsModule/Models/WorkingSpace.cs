using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Interfaces;
using GraphicsModule.Models.Painters;

namespace GraphicsModule.Models
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
        public IRestrictiveFramePainter FramePainter { get; set;}

        /// <summary>
        /// Отрисовщик кординат.
        /// </summary>
        public ICoordinatesPainter CoordinatesPainter { get; set;}

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
        public WorkingSpace(IPlotPainter plotPainter, IRestrictiveFramePainter framePainter, ICoordinatesPainter coordinatesPainter, SKCanvas canvas)
        {
            PlotPainter = plotPainter;
            FramePainter = framePainter;
            CoordinatesPainter = coordinatesPainter;
            _canvas = canvas;
        }

        /// <summary>
        /// Рисует график.
        /// </summary>
        /// <param name="plot">График.</param>
        public void PaintPlot(Plot plot, Parameters parameters)
        {
            PlotPainter.Paint(plot, parameters, _canvas);
        }

        /// <summary>
        /// Рисует рамку.
        /// </summary>
        /// <param name="frame">Рамка.</param>
        public void PaintFrame(RestrictiveFrame frame)
        {
            FramePainter.Paint(frame, _canvas);
        }

        /// <summary>
        /// Рисует координаты.
        /// </summary>
        /// <param name="coordinates">Координаты.</param>
        /// <param name="frame">Рамка.</param>
        public void PaintCoordinates(Coordinates coordinates, RestrictiveFrame frame)
        {
            CoordinatesPainter.Paint(coordinates, frame, _canvas);
        }
    }
}
