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
        public List<SKPaint> paintsListForPlots = new List<SKPaint>();

        /// <summary>
        /// Инициализирует словарь красок.
        /// </summary>
        public PaintsKeeper()
        {
            SKPaint indianRedPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.IndianRed,
                StrokeWidth = 2,
                TextSize = 24
            };            

            SKPaint blackPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black,
                StrokeWidth = 2,
                TextSize = 24
            };

            SKPaint bluePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue,
                StrokeWidth = 2,
                TextSize = 24
            };

            SKPaint redPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red,
                StrokeWidth = 2,
                TextSize = 24
            };

            SKPaint deepSkyBluePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.DeepSkyBlue,
                StrokeWidth = 2,
                TextSize = 24
            };

            SKPaint purplePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Purple,
                StrokeWidth = 2,
                TextSize = 24
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

            SKPaint grayPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Gray
            };

            paints.Add("Indian Red Paint", indianRedPaint);            
            paints.Add("Black Paint", blackPaint);            
            paints.Add("Blue Paint", bluePaint);
            paints.Add("Red Paint", redPaint);
            paints.Add("Deep Sky Blue Paint", deepSkyBluePaint);
            paints.Add("Purple Paint", purplePaint);
            paints.Add("Axes Paint", axesPaint);
            paints.Add("Text Paint", textPaint);
            paints.Add("Gray Paint", grayPaint);


            foreach (KeyValuePair<string, SKPaint> keyValue in paints)
            {
                if (keyValue.Key == "Axes Paint")
                {
                    break;
                }

                paintsListForPlots.Add(keyValue.Value);
            } 
        }
    }
}
