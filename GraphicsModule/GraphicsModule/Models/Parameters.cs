using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GraphicsModule.Models
{
    /// <summary>
    /// Класс параметров, который содержит все данные, касающиеся расчета.
    /// </summary>
    public class Parameters : ICloneable
    {
        /// <summary>
        /// Шаг расчета параметров.
        /// </summary>
        private float _step = 0.04f;

        // Может быть больше единицы
        /// <summary>
        /// Диэлектрическая проницаемость среды.
        /// </summary>
        private double Er = 2.8f;

        /// <summary>
        /// Коэффициент импедансной связи.
        /// </summary>
        private Complex _k;

        private Complex _z0;

        private Complex _z1;

        private Complex _z2;

        private Complex _s21;

        private Complex _l;

        /// <summary>
        /// Характеристический импеданс.
        /// </summary>
        public Complex Z0
        {
            get => _z0;
            private set
            {
                if (!(value.Real >= 40 && value.Real <= 70))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Value can not be null");
                }

                _z0 = value;
            }
        }

        /// <summary>
        /// Характеристический импеданс первой линии.
        /// </summary>
        public Complex Z1
        {
            get => _z1;
            private set
            {
                if (!(value.Real >= 30 && value.Real <= 80))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Value can not be null");
                }

                _z1 = value;
            }
        }

        /// <summary>
        /// Характеристический импеданс второй линии.
        /// </summary>
        public Complex Z2
        {
            get => _z2; 
            private set
            {
                if (!(value.Real >= 30 && value.Real <= 80))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Value can not be null");
                }

                _z2 = value;
            }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z01.
        /// </summary>
        public Complex Z01 { get; private set; }

        /// <summary>
        /// Номинал нагрузочного резистора Z02.
        /// </summary>
        public Complex Z02 { get; private set; }

        /// <summary>
        /// Коэффициент связи 1-ой и 2-ой линии.
        /// </summary>
        public Complex S21
        {
            get => _s21; private set
            {
                if (!(value.Real >= 3 && value.Real <= 10))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Value can not be null");
                }

                _s21 = value;
            }
        }

        /// <summary>
        /// Геометрическая длина схемы отрезка СЛ.
        /// </summary>
        public Complex L { get => _l; private set {
                if (!(value.Real >= 45 && value.Real <= 75))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Value can not be null");
                }

                _l = value * Math.Pow(10, -4); } }

        /// <summary>
        /// Начальная частота Fn.
        /// </summary>
        private double FStart { get; set; }

        /// <summary>
        /// Конечная частота Fn.
        /// </summary>
        private double FEnd { get; set; }

        /// <summary>
        /// Скорость света.
        /// </summary>
        private Complex C { get; set; }

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
        public Parameters(double z0, double z1, double z2, double z01, double z02, double s21, double l)
        {
            Z0 = z0;
            Z1 = z1;
            Z2 = z2;
            Z01 = z01;
            Z02 = z02;
            S21 = s21;
            L = l;

            FStart = 0.001;
            FEnd = 20;

            C = 299792458;
        }

        # region Параметры, вычисляемые формулами

        /// <summary>
        /// Коэффициент импедансной связи.
        /// </summary>
        private Complex GetK()
        {
            // k = 10^(-S21/20)
            Complex result = (Complex.Pow(10, -(S21 / 20)));

            return result;
        }

        /// <summary>
        /// Характеристический коэффициент k'.
        /// </summary>
        private Complex KHatch
        {
            get { return (Complex.Sqrt(1 - GetK() * GetK())); }
        }

        /// <summary>
        /// Коэффициент трансформации(симметрии).
        /// </summary>
        private Complex N
        {
            // n
            get { return (Complex.Sqrt(Z2 / Z1)); }
        }

        /// <summary>
        /// Электрическая длина отрезка СЛ.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        private Complex GetTheta(double currentF)
        {
            
            Complex result = ((GetOmega(currentF) * Math.Sqrt(Er) * L) / C);

            return result;
        }

        # region Нагрузочные резисторы

        /// <summary>
        /// Номинал нагрузочного резистора Z1c.
        /// </summary>
        private Complex Z1c
        {
            get { return ((Z0 * KHatch) / (N - GetK())); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z1π.
        /// </summary>
        private Complex Z1pi
        {
            get { return Z0 * (1 / N - GetK()) / KHatch; }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2c.
        /// </summary>
        private Complex Z2c
        {
            get { return ((Z0 * KHatch) / (1 / N - GetK())); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2π.
        /// </summary>
        private Complex Z2pi
        {
            get { return Z0 * (N - GetK()) / KHatch; }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Zm.
        /// </summary>
        private Complex Zm
        {
            get { return ((Z0 * KHatch) / GetK()); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z12.
        /// </summary>
        private Complex Z12
        {
            get { return ((Z0 * GetK()) / KHatch); }
        }

        #endregion

        # region Параметры с тильдами (~)
        private Complex W11Tilda
        {
            get { return ((Z0 * KHatch) / N); }
        }

        private Complex W22Tilda
        {
            get { return (Z0 * KHatch * N); }
        }

        private Complex Rho11Tilda
        {
            get { return (Z0 / (N * KHatch)); }
        }
        private Complex Rho22Tilda
        {
            get { return ((Z0 * N) / KHatch); }
        }

        private Complex UTilda
        {
            //get { return ((Z0 * KHatch) / K); }
            //UTilda = Zm
            get => Zm;
        }

        private Complex RTilda
        {
            get { return ((Z0 * GetK()) / KHatch); }
            // RTilda = z12
        }

        #endregion

        private Complex Rho11
        {
            get { return (Rho11Tilda / Z01); }
        }

        private Complex Rho22
        {
            get { return (Rho22Tilda / Z02); }
        }

        private Complex R
        {
            get { return (RTilda / Complex.Sqrt(Z01 * Z02)); }
        }

        private Complex W11
        {
            get { return (W11Tilda / Z01); }
        }

        private Complex W22
        {
            get { return (W22Tilda / Z02); }
        }

        private Complex V
        {
            get { return (Zm / Complex.Sqrt(Z01 * Z02)); }
        }

        #endregion

        private Complex GetOmega(Complex currentF)
        {
            // Точно правильно!
            Complex result = (2 * Math.PI * currentF * Complex.Pow(10, 9));

            return result;
        }

        # region S-параметры

        /// <summary>
        /// Возвращает общий знаменатель A для всех S-параметров.
        /// </summary>
        /// <param name="currentF">Текущая частота.</param>
        /// <returns>Параметр A.</returns>
        private Complex GetA(double currentF)
        {
            // Точно правильно
            Complex i = Complex.Sqrt(-1);
            Complex currentTheta = GetTheta(currentF);

            Complex sinTheta = Complex.Sin(currentTheta);
            Complex cosTheta = Complex.Cos(currentTheta);

            Complex alpha = Complex.Pow((R - 1 / V) * sinTheta, 2);

            Complex beta = 2 * cosTheta + i * (Rho11 + 1 / W11) * sinTheta;

            Complex gamma = 2 * cosTheta + i * (Rho22 + 1 / W22) * sinTheta;

            Complex multiplication = Complex.Multiply(beta, gamma);

            Complex result = Complex.Add(alpha, multiplication);

            return result;
        }

        private Complex GetS21(double currentF)
        {
            Complex i = Complex.Sqrt(-1);

            Complex currentTheta = GetTheta(currentF);

            Complex sinThetaOfPowerOf2 = Complex.Pow(Complex.Sin(currentTheta), 2);
            Complex sin2Theta = Complex.Sin(2 * currentTheta);

            Complex numerator = -2 * (Rho11 / V + R / W11) * sinThetaOfPowerOf2 + i * (R + 1 / V) * sin2Theta;

            Complex A = GetA(currentF);

            Complex result = Complex.Divide(numerator, A);

            // S12 = S21
            // Значение S21 инициализируется в конструкторе

            return result;
        }

        //public Complex GetS14(double currentF)
        //{
        //    // Правильно
        //    Complex i = Complex.Sqrt(-1);

        //    Complex currentTheta = GetTheta(currentF);

        //    Complex sinTheta = Complex.Sin(currentTheta);

        //    Complex A = GetA(currentF);

        //    Complex result = -i * ((2 * (R - 1/V) * sinTheta)/A);

        //    return result;
        //}

        private Complex GetS31(double currentF)
        {
            Complex i = Complex.Sqrt(-1);
            Complex currentTheta = GetTheta(currentF);

            Complex sinTheta = Complex.Sin(currentTheta);
            Complex cosTheta = Complex.Cos(currentTheta);

            Complex numerator = 2 * (2 * cosTheta + i * (Rho22 + 1 / W22)) * sinTheta;
            Complex A = GetA(currentF);

            Complex result = Complex.Divide(numerator, A);

            return result;
        }

        public List<float> GetListOfMagnitudesOfS21()
        {
            List<float> magnitudesList = new List<float>();

            float counter = (float)FStart;
            while (counter <= (float)FEnd)
            {
                Complex currentS21 = GetS21(counter);

                // Модуль
                float currentMagnitude = (float)(20 * Math.Log10(currentS21.Magnitude));
                magnitudesList.Add(currentMagnitude);
                counter += _step;
            }

            return magnitudesList;
        }

        public List<float> GetListOfMagnitudesOfS31()
        {
            List<float> magnitudesList = new List<float>();

            float counter = (float)FStart;
            while (counter <= (float)FEnd)
            {
                Complex currentS31 = GetS31(counter);

                // Модуль
                float currentMagnitude = (float)(20 * Math.Log10(currentS31.Magnitude));
                magnitudesList.Add(currentMagnitude);
                counter += _step;
            }

            return magnitudesList;
        }

        public List<float> GetListOfPhasesOfS21()
        {
            List<float> phasesList = new List<float>();

            float previousPhase = 0;

            float counter = (float)FStart;
            while (counter <= (float)FEnd)
            {
                Complex currentS21 = GetS21(counter);

                // Фаза
                float currentPhase = (float)(Math.Atan(currentS21.Imaginary / currentS21.Real) * 180 / Math.PI);

                //currentPhase *= (float)Math.Pow(10, 11);

                if (counter != 0)
                {
                    previousPhase = currentPhase;
                }

                if ((currentPhase - previousPhase) > 195)
                {
                    currentPhase -= 360;
                }

                if ((currentPhase - previousPhase) > 195)
                {
                    currentPhase -= 360;
                }

                phasesList.Add(currentPhase);
                counter += _step;
            }

            return phasesList;
        }


        public List<float> GetListOfPhasesOfS31()
        {
            List<float> phasesList = new List<float>();

            float previousPhase = 0;

            float counter = (float)FStart;
            while (counter <= (float)FEnd)
            {
                Complex currentS31 = GetS31(counter);

                // Фаза
                // Для перевода в градусы домножить на 180/Pi
                float currentPhase = (float)(Math.Atan(currentS31.Imaginary / currentS31.Real) * 180 / Math.PI);

                if (counter != 0)
                {
                    previousPhase = currentPhase;
                }

                if ((currentPhase - previousPhase) > 195)
                {
                    currentPhase -= 360;
                }

                if ((currentPhase - previousPhase) > 195)
                {
                    currentPhase -= 360;
                }

                phasesList.Add(currentPhase);
                counter += _step;
            }

            return phasesList;
        }
        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
