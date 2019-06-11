using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Models;
using SkiaSharp;

namespace GraphicsModule.Interfaces
{
    /// <summary>
    /// Интерфейс отрисовщика координат.
    /// </summary>
    public interface ICoordinatesPainter
    {
        void Paint(Coordinates coordinates, RestrictiveFrame frame, SKCanvas canvas);
    }
}
