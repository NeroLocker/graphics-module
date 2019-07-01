using GraphicsModule.Models;
using SkiaSharp;

namespace GraphicsModule.Interfaces
{
    /// <summary>
    /// Интерфейс отрисовщика координат.
    /// </summary>
    public interface ICoordinatesPainter
    {
        void Paint(Coordinates coordinates, Parameters parameters, RestrictiveFrame frame, SKCanvas canvas);
    }
}
