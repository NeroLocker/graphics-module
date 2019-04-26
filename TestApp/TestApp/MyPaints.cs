using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace TestApp
{
    /// <summary>
    /// Класс, хранящий коллекцию красок
    /// </summary>
    class MyPaints
    {

        public Dictionary<string, SKPaint> Dict = new Dictionary<string, SKPaint>();

        /// <summary>
        /// Инициализирует словарь красок
        /// </summary>
        public MyPaints()
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

            Dict.Add("Black Paint", blackPaint);
            Dict.Add("Red Paint", redPaint);
            Dict.Add("Blue Paint", bluePaint);
            Dict.Add("Text Paint", textPaint);
        }
    }
}
