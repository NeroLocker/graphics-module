using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Interfaces;
using GraphicsModule.Models;

namespace GraphicsModule
{
    /// <summary>
    /// Отрисовщик рамки.
    /// </summary>
    public class ConcreteFramePainter : IRestrictiveFramePainter
    {
        /// <summary>
        /// Рисует рамку.
        /// </summary>
        /// <param name="frame">Рамка.</param>
        /// <param name="canvas">Холст.</param>
        public void Paint(RestrictiveFrame frame, SKCanvas canvas)
        {
            var firstPointX = frame.GetFirstPointX();
            var firstPointY = frame.GetFirstPointY();
            var secondPointX = frame.GetSecondPointX();
            var secondPointY = frame.GetSecondPointY();

            var paints = new PaintsKeeper();

            canvas.DrawCircle(firstPointX + 35f, firstPointY + 35f, 10f, paints.paints["Red Paint"]);
            //canvas.DrawRect(firstPointX, firstPointY, secondPointX, secondPointY, frame.Paint);
        }
    }
}
