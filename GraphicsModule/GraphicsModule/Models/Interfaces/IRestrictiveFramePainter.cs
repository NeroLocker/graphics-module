using SkiaSharp;
using GraphicsModule.Models;

namespace GraphicsModule.Interfaces
{
    /// <summary>
    /// Интерфейс отрисовщика рамок.
    /// </summary>
    public interface IRestrictiveFramePainter
    {
        void Paint(RestrictiveFrame frame, SKCanvas canvas);
    }
}
