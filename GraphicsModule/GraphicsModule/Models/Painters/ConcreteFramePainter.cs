using SkiaSharp;
using GraphicsModule.Interfaces;

namespace GraphicsModule.Models.Painters
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
            canvas.DrawLine(frame.GetFirstPointX(), frame.GetFirstPointY(), frame.GetSecondPointX(), frame.GetFirstPointY(), frame.Paint);
            canvas.DrawLine(frame.GetSecondPointX(), frame.GetFirstPointY(), frame.GetSecondPointX(), frame.GetSecondPointY(), frame.Paint);
            canvas.DrawLine(frame.GetSecondPointX(), frame.GetSecondPointY(), frame.GetFirstPointX(), frame.GetSecondPointY(), frame.Paint);
            canvas.DrawLine(frame.GetFirstPointX(), frame.GetSecondPointY(), frame.GetFirstPointX(), frame.GetFirstPointY(), frame.Paint);            
        }
    }
}
