using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace GraphicsModule
{
    /// <summary>
    /// Класс, хранящий коллекцию красок
    /// </summary>
    public class PaintsKeeper
    {
        public Dictionary<string, SKPaint> dictionary = new Dictionary<string, SKPaint>();

        /// <summary>
        /// Инициализирует словарь красок
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

            dictionary.Add("Black Paint", blackPaint);
            dictionary.Add("Red Paint", redPaint);
            dictionary.Add("Blue Paint", bluePaint);
            dictionary.Add("Green Paint", greenPaint);
            dictionary.Add("Axes Paint", axesPaint);
            dictionary.Add("Text Paint", textPaint);
        }
    }
}
