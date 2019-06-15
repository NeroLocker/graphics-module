using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GraphicsModule.Models
{
    /// <summary>
    /// Класс параметров, который содержит все данные, касающиеся расчета.
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Характеристический импеданс.
        /// </summary>
        private float _z0;

        /// <summary>
        /// Характеристический импеданс первой линии.
        /// </summary>
        private float _z1;

        /// <summary>
        /// Характеристический импеданс второй линии.
        /// </summary>
        private float _z2;

        /// <summary>
        /// Номинал нагрузочного резистора Z01.
        /// </summary>
        private float _z01;

        /// <summary>
        /// Номинал нагрузочного резистора Z02.
        /// </summary>
        private float _z02;

        /// <summary>
        /// Коэффициент связи 1-ой и 2-ой линии.
        /// </summary>
        private float _s21;

        /// <summary>
        /// Геометрическая длина схемы отрезка СЛ.
        /// </summary>
        private float _l;

        /// <summary>
        /// Начальная частота fi.
        /// </summary>
        private float _fStart = 0;

        /// <summary>
        /// Конечная частота fi
        /// </summary>
        private float _fEnd;

        // Может быть больше единицы
        /// <summary>
        /// Диэлектрическая проницаемость среды.
        /// </summary>
        private float Er = 2.8f;       

        /// <summary>
        /// Скорость света.
        /// </summary>
        private float _c = 299792458f;

        /// <summary>
        /// Коэффициент импедансной связи.
        /// </summary>
        private float _k;

        /// <summary>
        /// Характеристический импеданс.
        /// </summary>
        public float Z0
        {
            get => _z0;
            private set => _z0 = value;
        }

        /// <summary>
        /// Характеристический импеданс первой линии.
        /// </summary>
        public float Z1
        {
            get => _z1;
            private set => _z1 = value;
        }

        /// <summary>
        /// Характеристический импеданс второй линии.
        /// </summary>
        public float Z2
        {
            get => _z2;
            private set => _z2 = value;
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z01.
        /// </summary>
        public float Z01
        {
            get => _z01;
            private set => _z01 = value;
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z02.
        /// </summary>
        public float Z02
        {
            get => _z02;
            private set => _z02 = value;
        }

        /// <summary>
        /// Коэффициент связи 1-ой и 2-ой линии.
        /// </summary>
        public float S21
        {
            get => _s21;
            private set => _s21 = Math.Abs(value);
        }

        /// <summary>
        /// Геометрическая длина схемы отрезка СЛ.
        /// </summary>
        public float L
        {
            get => _l;
            private set => _l = value;
        }

        /// <summary>
        /// Начальная частота Fn.
        /// </summary>
        public float FStart
        {
            get { return _fStart;}
        }

        /// <summary>
        /// Конечная частота Fn.
        /// </summary>
        public float FEnd
        {
            get => _fEnd;
            private set => _fEnd = value;
        }

        /// <summary>
        /// Скорость света.
        /// </summary>
        public float C
        {
            get => _c;
            private set => _c = value;
        }

        /// <summary>
        /// Конструктор, инициализирующий поля класса входными данными пользователя.
        /// </summary>
        /// <param name="z0"></param>
        /// <param name="z1"></param>
        /// <param name="z2"></param>
        /// <param name="z01"></param>
        /// <param name="z02"></param>
        /// <param name="s21"></param>
        /// <param name="l"></param>
        /// <param name="fEnd"></param>
        public Parameters(float z0, float z1, float z2, float z01, float z02, float s21, float l, float fEnd)
        {
            Z0 = z0;
            Z1 = z1;
            Z2 = z2;
            Z01 = z01;
            Z02 = z02;
            S21 = s21;
            L = l;
            FEnd = fEnd;
        }

        # region Параметры, вычисляемые формулами

        /// <summary>
        /// Коэффициент импедансной связи.
        /// </summary>
        private float K
        {
            // k = 10^(-S21/20)
            get => (float)(Math.Pow(10, -(S21 / 20)));
            set
            {
                if (!(value >= 0 && value < 1))
                {
                    throw new ArgumentException($"{value} should be in range of 0 to 1");
                }

                _k = value;
            }
        }

        /// <summary>
        /// Характеристический коэффициент k'.
        /// </summary>
        private float KHatch
        {           
            get { return (float)(Math.Sqrt(1 - K * K)); }            
        }

        /// <summary>
        /// Коэффициент трансформации(симметрии).
        /// </summary>
        private float N
        {
            // n
            get { return (float)(Math.Sqrt(Z2 / Z1)); }
        }

        /// <summary>
        /// Электрическая длина отрезка СЛ.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        private float Theta(float currentF)
        {
            
            return (float)((360 * currentF * Math.Sqrt(Er)*L)/C);
        }

        # region Нагрузочные резисторы

        /// <summary>
        /// Номинал нагрузочного резистора Z1c.
        /// </summary>
        private float Z1c
        {
            get { return (float)((Z0 * KHatch) / (N - K)); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z1π.
        /// </summary>
        private float Z1pi
        {
            get { return (float)(Z0 * ((1 / N - K) / KHatch)); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2c.
        /// </summary>
        private float Z2c
        {
            get { return (float)((Z0 * KHatch) / (1 / N - K)); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2π.
        /// </summary>
        private float Z2pi
        {
            get { return (float)(Z0 * ((N - K) / KHatch)); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Zm.
        /// </summary>
        private float Zm
        {
            get { return (float)((Z0 * KHatch) / K); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z12.
        /// </summary>
        private float Z12
        {
            get { return (float)((Z0 * K) / KHatch); }
        }

        #endregion

        # region Параметры с тильдами (~)
        private float W11tilda
        {
            get { return (float)((Z0 * KHatch) / N); }
        }

        private float W22tilda
        {
            get { return (float)(Z0 * KHatch * N); }
        }

        private float Rho11tilda
        {
            get { return (float)(Z0 / (N * KHatch)); }
        }
        private float Rho22tilda
        {
            get { return (float)((Z0 * N) / KHatch); }
        }

        private float Utilda
        {
            get { return (float)((Z0 * KHatch) / K); }
        }

        private float Rtilda
        {
            get { return (float)((Z0 * K) / KHatch); }
        }
        
        # endregion

        private float Rho11
        {
            get { return (float)(Rho11tilda / Z01); }
        }

        private float Rho22
        {
            get { return (float)(Rho22tilda / Z02); }
        }

        private float R
        {
            get { return (float)(Rtilda / Math.Sqrt(Z01 * Z02)); }
        }

        private float W11
        {
            get { return (float)(W11tilda / Z01); }
        }

        private float W22
        {
            get { return (float)(W22tilda / Z02); }
        }

        private float V
        {
            get { return (float)(Zm / Math.Sqrt(Z01 * Z02)); }
        }

        #endregion

        # region S-параметры
        /// <summary>
        /// Возвращает общий знаменатель A для всех S-параметров.
        /// </summary>
        /// <param name="currentF">Текущая частота.</param>
        /// <returns>Параметр A.</returns>
        public Complex GetA(float currentF)
        {
            // Обозначим комплексные части выражения А переменными
            // TODO: изменить тип данных alpha
            Complex alpha = Math.Pow(((R - 1/V) * Math.Sin(Theta(currentF))), 2);
            

            Complex beta = new Complex(2 * Math.Cos(Theta(currentF)), (Rho11 + 1/W11) * Math.Sin(Theta(currentF)));
            Complex gamma = new Complex(2 * Math.Cos(Theta(currentF)), (Rho22 + 1/W22) * Math.Sin(Theta(currentF)));

            Complex multiplication = Complex.Multiply(beta, gamma);

            Complex result = Complex.Add(alpha, multiplication);

            //???
            //return alpha + multiplication;
            return result;
        }

        public Complex GetS21(float currentF)
        {            
            double realPart = (-2 * (Rho11/V + R/W11) * Math.Pow(Math.Sin(Theta(currentF)), 2));
            double imaginaryPart = ((R + 1/V) * Math.Sin(2 * Theta(currentF)));
            // Числитель выражения
            Complex numerator = new Complex(realPart, imaginaryPart);

            Complex result = (numerator / GetA(currentF));

            // S12 = S21
            // Значение S21 инициализируется в конструкторе

            return result;
        }        
        public Complex S12(float currentF)
        {
            // Числитель выражения
            Complex numerator = new Complex((-2*(Rho11/V + R/W11)*Math.Pow(Math.Sin(Theta(currentF)),2)), ((R + 1/V) * Math.Sin(2 * Theta(currentF))));

            // Считаем
            Complex res = (numerator / GetA(currentF));

            // S12 = S21
            // Значение S21 инициализируется в конструкторе

            return res;
        }
        #endregion
    }
}
