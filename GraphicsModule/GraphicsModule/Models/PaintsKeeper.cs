using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace GraphicsModule.Models
{
    /// <summary>
    /// Класс, хранящий коллекцию красок.
    /// </summary>
    public class PaintsKeeper
    {
        public Dictionary<string, SKPaint> paints = new Dictionary<string, SKPaint>();

        /// <summary>
        /// Инициализирует словарь красок.
        /// </summary>
        public PaintsKeeper()
        {
            SKPaint blackPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black
            };

            SKPaint redPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red
            };

            SKPaint bluePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue
            };

            SKPaint greenPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Green
            };

            SKPaint axesPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 2
            };

            SKPaint textPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black,
                TextSize = 24
            };

            paints.Add("Black Paint", blackPaint);
            paints.Add("Red Paint", redPaint);
            paints.Add("Blue Paint", bluePaint);
            paints.Add("Green Paint", greenPaint);
            paints.Add("Axes Paint", axesPaint);
            paints.Add("Text Paint", textPaint);
        }
    }
}
