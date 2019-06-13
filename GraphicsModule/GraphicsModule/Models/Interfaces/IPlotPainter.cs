using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using GraphicsModule.Models;

namespace GraphicsModule.Interfaces
{    
    /// <summary>
    /// Интерфейс отрисовщика графиков.
    /// </summary>
    public interface IPlotPainter
    {
        void Paint(Plot plot, Parameters parameters, SKCanvas canvas);
    }
}
