using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModule.Models
{
    /// <summary>
    /// Класс, хранящий информацию о координатах и названиях осей графика.
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// Тип графика.
        /// </summary>
        public PlotType Type { get; private set;}

        /// <summary>
        /// Начальная точка оси Y.
        /// </summary>
        public float StartPointOfYAxis { get; private set;}

        /// <summary>
        /// Конечная точка оси Y;
        /// </summary>
        public float EndPointOfYAxis { get; private set;}

        /// <summary>
        /// Начальная точка оси X.
        /// </summary>
        public float StartPointOfXAxis { get; private set;}

        /// <summary>
        /// Конечная точка оси X;
        /// </summary>
        public float EndPointOfXAxis { get; private set; }

        /// <summary>
        /// Название оси Y;
        /// </summary>
        public string NameOfYAxis { get; private set;}

        /// <summary>
        /// Название оси X.
        /// </summary>
        public string NameOfXAxis { get; private set;}

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type"></param>
        public Coordinates(PlotType type)
        {
            List<string> textData = new List<string>() { "S-параметры, дБ", "Фаза, град."};           
            NameOfXAxis = "Частота, ГГц";
            Type = type;

            // см. рис 7 статьи
            if (Type == PlotType.FrequencyResponse)
            {
                NameOfYAxis = textData[0];
                StartPointOfYAxis = 0;
                EndPointOfYAxis = -30;
            }
            else
            {
                NameOfYAxis = textData[1];
                StartPointOfYAxis = 90;
                EndPointOfYAxis = -180;
            }
            
            StartPointOfXAxis = 0;
            EndPointOfXAxis = 20;
        }

    }
}
