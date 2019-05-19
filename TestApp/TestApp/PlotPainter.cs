﻿using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace TestApp
{
    /// <summary>
    /// Класс, ответственный за построение графиков
    /// </summary>
    public class PlotPainter
    {
        // Все поля относящиеся к рабочему холсту
        private SKImageInfo _info;
        private SKSurface _surface;
        private SKCanvas _canvas;

        // Поля центров по ширине и высоте холста
        private float _widthCenterOfCanvas;
        private float _heightCenterOfCanvas;

        // Краски
        private PaintsKeeper _keeper;

        // Толщина линий для отрисовки
        private int _thicknessOfLines = 2;

        /// <summary>
        /// Инициализирует служебные поля холста и необходимые для работы данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public PlotPainter(object sender, SKPaintSurfaceEventArgs args)
        {
            _info = args.Info;
            _surface = args.Surface;
            _canvas = _surface.Canvas;

            _widthCenterOfCanvas = _info.Width / 2;
            _heightCenterOfCanvas = _info.Height / 2;

            _keeper = new PaintsKeeper();

            _canvas.Clear();
        }

        /// <summary>
        /// Рисует графики
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
        /// Рисует оси координатной системы и помечает их метками
        /// </summary>
        public void DrawAxis()
        {
            // Оси

            float percent = (float)0.05;

            _canvas.DrawRect((float)(_info.Width * percent), (float)(_info.Height * percent), (float)(_info.Width * (1 - 2 * percent)), (float)(_info.Height * (1 - 2 * percent)), _keeper.dictionary["Axes Paint"]);
          
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
                _canvas.DrawText($"{i}", (float)currentPixel, (float)(_info.Height), _keeper.dictionary["Text Paint"]);
                _canvas.DrawLine((float)currentPixel, (float)(_info.Height * (1 - percent)), (float)currentPixel, (float)(_info.Height * percent), _keeper.dictionary["Green Paint"]);
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
                _canvas.DrawText($"{i}", (float)(_info.Width * 0.02), (float)currentPixel, _keeper.dictionary["Text Paint"]);
                _canvas.DrawLine((float)(_info.Width * percent), (float)currentPixel, (float)(_info.Width * (1 - percent)), (float)currentPixel, _keeper.dictionary["Green Paint"]);
            }
        }

        /// <summary>
        /// Рисует график по входной строке
        /// </summary>
        /// <param name="nameOfPlot"></param>
        private void DrawPlotByName(string nameOfPlot)
        {
            // Необходимо для построения тригонометрических функций
            float PI = (float)(Math.PI);

            if (nameOfPlot == "sin")
            {
                _canvas.DrawText("sin(x)", 0, (float)(_info.Height * 0.25), _keeper.dictionary["Text Paint"]);

                for (int i = 0; i < _info.Width; i++)
                {
                    int x = i;
                    float y = (float)(_info.Height / 9 * (Math.Sin(i * 5 * PI / (_info.Width - 1))));
                    _canvas.DrawCircle(x, _info.Height / 2 + y, _thicknessOfLines, _keeper.dictionary["Black Paint"]);
                }
            }

            if (nameOfPlot == "cos")
            {
                // Рисуем красным цветом
                _keeper.dictionary["Text Paint"].Color = SKColors.Red;
                _canvas.DrawText("cos(x)", 0, (float)(_info.Height * 0.30), _keeper.dictionary["Text Paint"]);

                for (int i = 0; i < _info.Width; i++)
                {
                    int x = i;
                    float y = (float)(_info.Height / 9 * (Math.Cos(i * 5 * PI / (_info.Width - 1))));
                    _canvas.DrawCircle(x, _info.Height / 2 + y, _thicknessOfLines, _keeper.dictionary["Red Paint"]);
                }
            }

            if (nameOfPlot == "sin+cos")
            {
                // Рисуем синим цветом
                _keeper.dictionary["Text Paint"].Color = SKColors.Blue;
                _canvas.DrawText("sin(x) + cos(x)", 0, (float)(_info.Height * 0.35), _keeper.dictionary["Text Paint"]);

                for (int i = 0; i < _info.Width; i++)
                {
                    int x = i;
                    float sinY = (float)(_info.Height / 9 * (Math.Sin(i * 5 * PI / (_info.Width - 1))));
                    float cosY = (float)(_info.Height / 9 * (Math.Cos(i * 5 * PI / (_info.Width - 1))));
                    float y = sinY + cosY;
                    _canvas.DrawCircle(x, _info.Height / 2 + y, _thicknessOfLines, _keeper.dictionary["Blue Paint"]);
                }
            }

            if (nameOfPlot == "kx")
            {
                // y = kx + b

                // Метка графика
                _canvas.DrawText("kx", 0, (float)(_info.Height * 0.25), _keeper.dictionary["Text Paint"]);

                // Перемещаемся по x в начало координат
                float counter = (float)-(_info.Width * 0.35);

                // Проходим циклом 100% отрезка ширины экрана
                while(counter < _info.Width * 0.65)
                {
                    float x = counter;
                    float y = -x;
                    _canvas.DrawCircle((float)(_info.Width * 0.35 + x), _info.Height / 2 + y, _thicknessOfLines, _keeper.dictionary["Black Paint"]);

                    counter += 1;
                }
            }

            if (nameOfPlot == "x^2")
            {
                // y = x^2

                // Рисуем красным цветом
                _keeper.dictionary["Text Paint"].Color = SKColors.Red;

                // Метка графика
                _canvas.DrawText("x*x", 0, (float)(_info.Height * 0.30), _keeper.dictionary["Text Paint"]);

                // Перемещаемся по x в начало координат
                float counter = (float)-(_info.Width * 0.35);

                // Проходим циклом 100% отрезка ширины экрана
                while (counter < _info.Width * 0.65)
                {
                    float x = counter;

                    // Масштабируем график с помощью домножения на коэффициент
                    float y = (float)(-0.01 * (x * x));
                    _canvas.DrawCircle((float)(_info.Width * 0.35 + x), _info.Height / 2 + y, _thicknessOfLines, _keeper.dictionary["Red Paint"]);

                    counter += 1;
                }
            }

            if (nameOfPlot == "e^x")
            {
                // y = e^x

                // Рисуем синим цветом
                _keeper.dictionary["Text Paint"].Color = SKColors.Blue;

                // Метка графика
                _canvas.DrawText("e^x", 0, (float)(_info.Height * 0.35), _keeper.dictionary["Text Paint"]);

                // Перемещаемся по x в начало координат
                float counter = (float)-(_info.Width * 0.35);
                while (counter < 30)
                {
                    float x = counter;
                    float y = (float)(-Math.Exp(x));

                    _canvas.DrawCircle((float)(_info.Width * 0.35 + x), _info.Height / 2 + y, _thicknessOfLines, _keeper.dictionary["Blue Paint"]);

                    // Используем литерал типа float для правильного приведения типов
                    counter += 0.05f;
                }
            }
        }
    }
}
