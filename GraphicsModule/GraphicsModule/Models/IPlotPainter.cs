using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModule
{    
    /// <summary>
    /// Интерфейс отрисовщика графиков.
    /// </summary>
    public interface IPlotPainter
    {
        void Paint(Plot plot, SKCanvas canvas);
    }
}
