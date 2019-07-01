using SkiaSharp;
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
