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
            //get => (float)(Math.Pow(10, -(S21 / 20)));
            // ??
            //get => (float)((Z1c - Z1pi)/(Z1c + Z1pi));
            get => 0.99f;
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
        public Complex Theta(float currentF)
        {
            Complex i = Complex.Sqrt(-1);
            
            Complex result = ((i * GetOmega(currentF) * Math.Sqrt(Er) * L) / C);
            return result;
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
            get { return Z0 * (1/N - K)/KHatch; }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2c.
        /// </summary>
        private float Z2c
        {
            get { return ((Z0 * KHatch)/(1/N - K)); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2π.
        /// </summary>
        private float Z2pi
        {
            get { return Z0 * (N - K)/KHatch; }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Zm.
        /// </summary>
        private float Zm
        {
            get { return ((Z0 * KHatch) / K); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z12.
        /// </summary>
        private float Z12
        {
            get { return ((Z0 * K) / KHatch); }
        }

        #endregion

        # region Параметры с тильдами (~)
        private float W11Tilda
        {
            get { return ((Z0 * KHatch) / N); }
        }

        private float W22Tilda
        {
            get { return (float)(Z0 * KHatch * N); }
        }

        private float Rho11Tilda
        {
            get { return (float)(Z0 / (N * KHatch)); }
        }
        private float Rho22Tilda
        {
            get { return (float)((Z0 * N) / KHatch); }
        }

        private float UTilda
        {
            //get { return (float)((Z0 * KHatch) / K); }
            //UTilda = Zm
            get => Zm;
        }

        private float RTilda
        {
            //get { return (float)((Z0 * K) / KHatch); }
            // RTilda = z12
            get => Z12;
        }
        
        # endregion

        private float Rho11
        {
            get { return (Rho11Tilda / Z01); }
        }

        private float Rho22
        {
            get { return (Rho22Tilda / Z02); }
        }

        private float R
        {
            get { return (float)(RTilda / Math.Sqrt(Z01 * Z02)); }
        }

        private float W11
        {
            get { return (W11Tilda / Z01); }
        }

        private float W22
        {
            get { return (W22Tilda / Z02); }
        }

        private float V
        {
            get { return (float)(Zm / Math.Sqrt(Z01 * Z02)); }
        }

        #endregion

        public float GetOmega(float currentF)
        {
            float result = (float)(2 * Math.PI * currentF * Math.Pow(10, 9));
            return result;
        }

        # region S-параметры

        public Complex GetZCT(float currentF)
        {
            Complex currentTheta = Theta(currentF);
            //гиперболический котангенс
            Complex cothOfTheta = 1/Complex.Tanh(currentTheta);

            Complex result = Z0/KHatch * cothOfTheta;

            return result;
        }

        public Complex GetZCS(float currentF)
        {
            Complex currentTheta = Theta(currentF);
            //гиперболический косеканс 
            Complex cschOfTheta = 1 / Complex.Sinh(currentTheta);

            Complex result = Z0 / KHatch * cschOfTheta;

            return result;
        }

        public Complex GetZ12(float currentF)
        {
            Complex result = GetZCT(currentF) * K;

            return result;
        }

        public Complex GetZ0(float currentF)
        {
            Complex sqrt = Math.Sqrt(Z01 * Z02);
            Complex result = GetZ12(currentF)/(sqrt);

            return result;
        }

        /// <summary>
        /// Возвращает общий знаменатель A для всех S-параметров.
        /// </summary>
        /// <param name="currentF">Текущая частота.</param>
        /// <returns>Параметр A.</returns>
        public Complex GetA(float currentF)
        {
            Complex i = Complex.Sqrt(-1);

            Complex currentTheta = Theta(currentF);

            Complex sinOfTheta = Complex.Sin(currentTheta);
            Complex cosOfTheta = Complex.Cos(currentTheta);

            // Обозначим комплексные части выражения А переменными
            // TODO: изменить тип данных alpha
            Complex alpha = Complex.Pow(((R - 1 / V) * sinOfTheta), 2);            

            Complex beta = 2 * cosOfTheta + i * (Rho11 + 1/W11) * sinOfTheta;

            Complex gamma = 2 * cosOfTheta + i * (Rho22 + 1/W22) * sinOfTheta;

            Complex multiplication = Complex.Multiply(beta, gamma);

            Complex result = Complex.Add(alpha, multiplication);

            //???
            //return alpha + multiplication;
            return result;
        }

        public Complex GetS21(float currentF)
        {
            Complex i = Complex.Sqrt(-1);

            Complex currentTheta = Theta(currentF);

            Complex sinThetaOfPowerOf2 = Complex.Pow(Complex.Sin(currentTheta), 2);
            Complex sin2Theta = Complex.Sin(2 * currentTheta);

            Complex numerator = -2 * (Rho11/V + R/W11) * sinThetaOfPowerOf2 + i * (R + 1/V) * sin2Theta;

            Complex A = GetA(currentF);

            Complex result = Complex.Divide(numerator, A);

            // S12 = S21
            // Значение S21 инициализируется в конструкторе

            return result;
        }

        public Complex GetS14(float currentF)
        {
            Complex i = Complex.Sqrt(-1);

            Complex currentTheta = Theta(currentF);

            Complex sinTheta = Complex.Sin(currentTheta);

            Complex A = GetA(currentF);

            Complex result = -i * ((2 * (R - 1/V) * sinTheta)/A);

            return result;
        }
        //public Complex S12(float currentF)
        //{
        //    // Числитель выражения
        //    Complex numerator = new Complex((-2 * (Rho11 / V + R / W11) * Math.Pow(Math.Sin(Theta(currentF)), 2)), ((R + 1 / V) * Math.Sin(2 * Theta(currentF))));

        //    // Считаем
        //    Complex res = (numerator / GetA(currentF));

        //    // S12 = S21
        //    // Значение S21 инициализируется в конструкторе

        //    return res;
        //}
        #endregion
    }
}
