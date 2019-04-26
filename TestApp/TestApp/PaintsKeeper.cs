using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace TestApp
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

            SKPaint textPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black,
                TextSize = 32
            };

            dictionary.Add("Black Paint", blackPaint);
            dictionary.Add("Red Paint", redPaint);
            dictionary.Add("Blue Paint", bluePaint);
            dictionary.Add("Text Paint", textPaint);
        }
    }
}
