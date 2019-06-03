using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace GraphicsModule
{
    /// <summary>
    /// Класс, ответственный за построение графиков.
    /// </summary>
    public class Painter
    {
        // Все поля относящиеся к рабочему холсту
        private SKImageInfo _info;
        private SKSurface _surface;
        private SKCanvas _canvas;


        /// <summary>
        /// Коллекция красок.
        /// </summary>
        private PaintsKeeper _paints;

        /// <summary>
        /// Коллекция графиков.
        /// </summary>
        private List<Plot> Plots;

        /// <summary>
        /// Рабочее пространство или рамка.
        /// </summary>
        private Frame DrawingSpace;

        // Толщина линий для отрисовки
        private int _thicknessOfLines = 2;

        private float _indent;

        private float _startWidthPoint;

        private float StartWidthPoint
        {
            get { return _startWidthPoint;}
            set
            {
                _startWidthPoint = (float)(_info.Width * value);
            }
        }

        private float _endWidthPoint;

        private float EndWidthPoint
        {
            get { return _endWidthPoint; }
            set
            {
                _endWidthPoint = (float)(_info.Width * (1 - value));
            }
        }

        /// <summary>
        /// Отступ надписей графиков от левой стороны экрана в процентах в десятичной форме.
        /// </summary>
        private float Indent
        {
            get { return _indent;}
            set
            {
                // Считаем процент от ширины холста
                _indent = (float)(_info.Width * value);
            }
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public Painter(List<Plot> plots, float margin, object sender, SKPaintSurfaceEventArgs args)
        {
            Plots = plots;
            DrawingSpace = new Frame(args.Info, margin);

            _info = args.Info;
            _surface = args.Surface;
            _canvas = _surface.Canvas;

            _paints = new PaintsKeeper();

            Indent = (float)0.10;

            StartWidthPoint = (float)0.05;
            EndWidthPoint = (float)0.05;

            _canvas.Clear();
        }

        /// <summary>
        /// Рисует графики.
        /// </summary>
        /// <param name="typeOfPlot"></param>
        public void Paint(string typeOfPlot)
        {
            DrawAxis();

            if (typeOfPlot == "Trigonometric")
            {               
                // sin(x)
                DrawPlotByName("sin");

                // cos(x)
                DrawPlotByName("cos");

                // sin(x) + cos(x)
                DrawPlotByName("sin+cos");
            }

            if (typeOfPlot == "Other")
            {
                DrawPlotByName("kx");
                DrawPlotByName("x^2");
                DrawPlotByName("e^x");
            }
        }

        /// <summary>
        /// Рисует оси координатной системы и помечает их метками.
        /// </summary>
        public void DrawAxis()
        {
            // Оси

            float percent = (float)0.05;

            _canvas.DrawRect((float)(StartWidthPoint), (float)(_info.Height * percent), (float)(_info.Width * (1 - 2 * percent)), (float)(_info.Height * (1 - 2 * percent)), _paints.dictionary["Axes Paint"]);
          
            // Точка 0
            //_keeper.dictionary["Text Paint"].Color = SKColors.Black;
            //_canvas.DrawText("0", 0, (float)(_info.Height / 2), _keeper.dictionary["Text Paint"]);

            // Пометка оси X
            //_canvas.DrawText("x", (float)(_info.Width - 24), (float)(_info.Height / 2), _keeper.dictionary["Text Paint"]);

            // Число отметок
            float numberOfMarks = 20;

            float step = (float)((_info.Width * (1 - 2 * percent)) / numberOfMarks);

            float currentPixel = (float)(_info.Width * percent);

            float temp = (float)(_info.Width * (1 - percent));

            for (int i = 1; i <= temp; i++)
            {
                if (i >= numberOfMarks)
                {
                    break;
                }
                currentPixel += step;
                _canvas.DrawText($"{i}", (float)currentPixel, (float)(_info.Height), _paints.dictionary["Text Paint"]);
                _canvas.DrawLine((float)currentPixel, (float)(_info.Height * (1 - percent)), (float)currentPixel, (float)(_info.Height * percent), _paints.dictionary["Green Paint"]);
            }

            // Для оси Y
            step = (float)((_info.Height * (1 - 2 * percent)) / numberOfMarks);

            currentPixel = (float)(_info.Height * percent);

            temp = (float)(_info.Height * (1 - percent));

            for (int i = 1; i <= temp; i++)
            {
                if (i >= numberOfMarks)
                {
                    break;
                }
                currentPixel += step;
                _canvas.DrawText($"{i}", (float)(_info.Width * 0.02), (float)currentPixel, _paints.dictionary["Text Paint"]);
                _canvas.DrawLine((float)(_info.Width * percent), (float)currentPixel, (float)(_info.Width * (1 - percent)), (float)currentPixel, _paints.dictionary["Green Paint"]);
            }
        }

        /// <summary>
        /// Рисует график по входной строке.
        /// </summary>
        /// <param name="nameOfPlot"></param>
        private void DrawPlotByName(string nameOfPlot)
        {
            // Необходимо для построения тригонометрических функций
            float PI = (float)(Math.PI);

            //float firstPoint = percent

            if (nameOfPlot == "sin")
            {
                _canvas.DrawText("sin(x)", Indent, (float)(_info.Height * 0.25), _paints.dictionary["Text Paint"]);

                for (float i = StartWidthPoint; i < EndWidthPoint; i++)
                {
                    float x = i;
                    float y = (float)(_info.Height / 9 * (Math.Sin(i * 5 * PI / (_info.Width - 1))));
                    _canvas.DrawCircle(x, _info.Height / 2 + y, _thicknessOfLines, _paints.dictionary["Black Paint"]);
                }
            }

            if (nameOfPlot == "cos")
            {
                // Рисуем красным цветом
                _paints.dictionary["Text Paint"].Color = SKColors.Red;
                _canvas.DrawText("cos(x)", Indent, (float)(_info.Height * 0.30), _paints.dictionary["Text Paint"]);

                for (float i = StartWidthPoint; i < EndWidthPoint; i++)
                {
                    float x = i;
                    float y = (float)(_info.Height / 9 * (Math.Cos(i * 5 * PI / (_info.Width - 1))));
                    _canvas.DrawCircle(x, _info.Height / 2 + y, _thicknessOfLines, _paints.dictionary["Red Paint"]);
                }
            }

            if (nameOfPlot == "sin+cos")
            {
                // Рисуем синим цветом
                _paints.dictionary["Text Paint"].Color = SKColors.Blue;
                _canvas.DrawText("sin(x) + cos(x)", Indent, (float)(_info.Height * 0.35), _paints.dictionary["Text Paint"]);

                for (float i = StartWidthPoint; i < EndWidthPoint; i++)
                {
                    float x = i;
                    float sinY = (float)(_info.Height / 9 * (Math.Sin(i * 5 * PI / (_info.Width - 1))));
                    float cosY = (float)(_info.Height / 9 * (Math.Cos(i * 5 * PI / (_info.Width - 1))));
                    float y = sinY + cosY;
                    _canvas.DrawCircle(x, _info.Height / 2 + y, _thicknessOfLines, _paints.dictionary["Blue Paint"]);
                }
            }

            if (nameOfPlot == "kx")
            {
                // y = kx + b

                // Метка графика
                _canvas.DrawText("kx", Indent, (float)(_info.Height * 0.25), _paints.dictionary["Text Paint"]);

                // Перемещаемся по x в начало координат
                float counter = StartWidthPoint;

                // Проходим циклом 100% отрезка ширины экрана
                while(counter < EndWidthPoint)
                {
                    float x = counter;
                    float y = -x;
                    _canvas.DrawCircle((float)(_info.Width * 0.35 + x), _info.Height / 2 + y, _thicknessOfLines, _paints.dictionary["Black Paint"]);

                    counter += 1;
                }
            }

            if (nameOfPlot == "x^2")
            {
                // y = x^2

                // Рисуем красным цветом
                _paints.dictionary["Text Paint"].Color = SKColors.Red;

                // Метка графика
                _canvas.DrawText("x*x", Indent, (float)(_info.Height * 0.30), _paints.dictionary["Text Paint"]);

                // Перемещаемся по x в начало координат
                float counter = (float)-(_info.Width * 0.35);

                // Проходим циклом 100% отрезка ширины экрана
                while (counter < _info.Width * 0.65)
                {
                    float x = counter;

                    // Масштабируем график с помощью домножения на коэффициент
                    float y = (float)(-0.01 * (x * x));
                    _canvas.DrawCircle((float)(_info.Width * 0.35 + x), _info.Height / 2 + y, _thicknessOfLines, _paints.dictionary["Red Paint"]);

                    counter += 1;
                }
            }

            if (nameOfPlot == "e^x")
            {
                // y = e^x

                // Рисуем синим цветом
                _paints.dictionary["Text Paint"].Color = SKColors.Blue;

                // Метка графика
                _canvas.DrawText("e^x", Indent, (float)(_info.Height * 0.35), _paints.dictionary["Text Paint"]);

                // Перемещаемся по x в начало координат
                float counter = (float)-(_info.Width * 0.35);
                while (counter < 30)
                {
                    float x = counter;
                    float y = (float)(-Math.Exp(x));

                    _canvas.DrawCircle((float)(_info.Width * 0.35 + x), _info.Height / 2 + y, _thicknessOfLines, _paints.dictionary["Blue Paint"]);

                    // Используем литерал типа float для правильного приведения типов
                    counter += 0.05f;
                }
            }
        }
    }
}
