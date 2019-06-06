using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModule
{
    /// <summary>
    /// Отрисовщик рамок.
    /// </summary>
    public interface IFramePainter
    {
        void Paint(Frame frame, SKCanvas canvas);
    }
}
