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
        private double Er { get; set;}

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
        /// Характеристический импеданс первой линии.
        /// </summary>
        public Complex Z1
        {
            get => _z1;
            private set
            {
                if (!(value.Real >= 1 && value.Real <= 100))
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
                if (!(value.Real >= 1 && value.Real <= 100))
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
                if (!(value.Real >= 2 && value.Real <= 10))
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
                if (!(value.Real >= 10 && value.Real <= 100))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Value can not be null");
                }

                _l = value * Math.Pow(10, -4); } }

        /// <summary>
        /// Начальная частота Fi.
        /// </summary>
        public double Fmin { get; private set; }

        /// <summary>
        /// Конечная частота Fi.
        /// </summary>
        public double Fmax { get; private set; }

        /// <summary>
        /// Скорость света.
        /// </summary>
        private Complex C { get; set; }

        /// <summary>
        /// Конструктор, инициализирующий поля класса входными данными пользователя.
        /// </summary>
        /// <param name="zo"></param>
        /// <param name="z1"></param>
        /// <param name="z2"></param>
        /// <param name="z01"></param>
        /// <param name="z02"></param>
        /// <param name="s21"></param>
        /// <param name="l"></param>
        /// <param name="fEnd"></param>
        public Parameters(double fmin, double fmax, double l, double er, double s21, double z01, double z02, double z1, double z2)
        {
            Fmin = fmin;
            Fmax = fmax;
            L = l;
            Er = er;
            S21 = s21;
            Z01 = z01;
            Z02 = z02;
            Z1 = z1;
            Z2 = z2;

            C = 299792458;  
        }

        # region Параметры, вычисляемые формулами

        /// <summary>
        /// Возвращает коэффициент симметрии n.
        /// </summary>
        /// <returns></returns>
        private Complex GetN()
        {
            Complex z2 = Z2;
            Complex z1 = Z1;

            Complex n = Complex.Sqrt(z2/z1);
            return n;
        }

        /// <summary>
        /// Возващает характеристическое сопротивление СЛ Zo.
        /// </summary>
        /// <returns></returns>
        private Complex GetZo()
        {
            Complex z2 = Z2;
            Complex z1 = Z1;

            Complex zo = Complex.Sqrt(z1 * z2);
            return zo;
        }

        /// <summary>
        /// Возвращает среднее геометрическое сопротивление нагрузок Z0.
        /// </summary>
        /// <returns></returns>
        private Complex GetZ0()
        {
            Complex z01 = Z01;
            Complex z02 = Z02;

            Complex z0 = Complex.Sqrt(z01 * z02);
            return z0;
        }

        /// <summary>
        /// Возвращает характеристический коэффициент k'.
        /// </summary>
        private Complex GetKHatch()
        {
            Complex k = GetK();
            Complex kHatch = Complex.Sqrt(1 - Complex.Pow(k, 2));

            return kHatch;
        }

        /// <summary>
        /// Возвращает ρ11.
        /// </summary>
        /// <returns></returns>
        private Complex GetRho11()
        {
            Complex zo = GetZo();
            Complex z01 = Z01;
            Complex n = GetN();
            Complex kHatch = GetKHatch();

            Complex rho11 = zo/(z01 * n * kHatch);

            return rho11;
        }

        /// <summary>
        /// Возвращает ρ22.
        /// </summary>
        /// <returns></returns>
        private Complex GetRho22()
        {
            Complex zo = GetZo();
            Complex z02 = Z02;
            Complex n = GetN();
            Complex kHatch = GetKHatch();

            Complex rho22 = (zo * n)/(z02 * kHatch);

            return rho22;
        }

        /// <summary>
        /// Возвращает r.
        /// </summary>
        /// <returns></returns>
        private Complex GetR()
        {
            Complex zo = GetZo();
            Complex z0 = GetZ0();
            Complex k = GetK();
            Complex kHatch = GetKHatch();

            Complex r = (zo * k)/(z0 * kHatch);

            return r;
        }

        /// <summary>
        /// Возвращает W11.
        /// </summary>
        /// <returns></returns>
        private Complex GetW11()
        {
            Complex zo = GetZo();
            Complex kHatch = GetKHatch();
            Complex z01 = Z01;
            Complex n = GetN();
            
            Complex w11 = (zo * kHatch)/(z01 * n);

            return w11;
        }

        /// <summary>
        /// Возвращает W22.
        /// </summary>
        /// <returns></returns>
        private Complex GetW22()
        {
            Complex zo = GetZo();
            Complex kHatch = GetKHatch();
            Complex z02 = Z02;
            Complex n = GetN();

            Complex w22 = (zo * kHatch * n)/(z02);

            return w22;
        }

        /// <summary>
        /// Возвращает v.
        /// </summary>
        /// <returns></returns>
        private Complex GetV()
        {
            Complex zo = GetZo();
            Complex kHatch = GetKHatch();
            Complex z0 = GetZ0();
            Complex k = GetK();

            Complex v = (zo * kHatch)/(z0 * k);

            return v;
        }

        #endregion



        /// <summary>
        /// Возвращает значение ω для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        private Complex GetOmega(Complex currentF)
        {
            Complex omega = (2 * Math.PI * currentF * Complex.Pow(10, 9));

            return omega;
        }

        /// <summary>
        /// Возвращает электрическую длину отрезка СЛ (рад).
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        private Complex GetTheta(double currentF)
        {
            Complex omega = GetOmega(currentF);

            Complex theta = ((omega * Math.Sqrt(Er) * L) / C);

            return theta;
        }

        /// <summary>
        /// Возвращает коэффициент импедансной связи.
        /// </summary>
        private Complex GetK()
        {
            // k = 10^(-S21/20)
            Complex k = Complex.Pow(10, -S21/20);

            return k;
        }

        # region S-параметры

        /// <summary>
        /// Возвращает общий знаменатель A для всех S-параметров.
        /// </summary>
        /// <param name="currentF">Текущая частота.</param>
        /// <returns>Параметр A.</returns>
        public Complex GetA(double currentF)
        {
            Complex r = GetR();
            Complex v = GetV();
            Complex rho11 = GetRho11();
            Complex w11 = GetW11();

            Complex rho22 = GetRho22();
            Complex w22 = GetW22();

            Complex theta = GetTheta(currentF);
            Complex sinOfTheta = Complex.Sin(theta);
            Complex cosOfTheta = Complex.Cos(theta);

            Complex i = Complex.Sqrt(-1);

            Complex alpha = Complex.Pow((r - 1/v) * sinOfTheta, 2);
            Complex beta = 2 * cosOfTheta + i * ((rho11 + 1/w11) * sinOfTheta);
            Complex gamma = 2 * cosOfTheta + i * ((rho22 + 1/w22) * sinOfTheta);

            Complex multiplication = Complex.Multiply(beta, gamma);
            Complex a = Complex.Add(alpha, multiplication);

            return a;
        }

        /// <summary>
        /// Возвращает значение коэффициента передачи S11 для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex GetS11(double currentF)
        {
            Complex r = GetR();
            Complex v = GetV();
            Complex rho11 = GetRho11();
            Complex w11 = GetW11();
            Complex rho22 = GetRho22();
            Complex w22 = GetW22();

            Complex theta = GetTheta(currentF);
            Complex sinOfTheta = Complex.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            Complex alpha = Complex.Pow(r, 2) - 1/Complex.Pow(v, 2);
            Complex beta = rho11 - 1/w11;
            Complex gamma = rho22 + 1/w22;

            Complex numerator = (alpha - beta * gamma) * Complex.Pow(sinOfTheta, 2) + i * beta * Complex.Sin(2 * theta);
            Complex a = GetA(currentF);

            Complex s11 = Complex.Divide(numerator, a);

            return s11;
        }

        /// <summary>
        /// Возвращает значение коэффициента передачи S22 для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex GetS22(double currentF)
        {
            Complex r = GetR();
            Complex v = GetV();
            Complex rho11 = GetRho11();
            Complex w11 = GetW11();
            Complex rho22 = GetRho22();
            Complex w22 = GetW22();

            Complex theta = GetTheta(currentF);
            Complex sinOfTheta = Complex.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            Complex alpha = Complex.Pow(r, 2) - 1 / Complex.Pow(v, 2);
            Complex beta = rho11 - 1 / w11;
            Complex gamma = rho22 + 1 / w22;

            Complex numerator = (alpha - beta * gamma) * Complex.Pow(sinOfTheta, 2) + i * gamma * Complex.Sin(2 * theta);
            Complex a = GetA(currentF);

            Complex s22 = Complex.Divide(numerator, a);

            return s22;
        }

        /// <summary>
        /// Возвращает значение коэффициента передачи S12 для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex GetS12(double currentF)
        {
            Complex r = GetR();
            Complex v = GetV();
            Complex rho11 = GetRho11();
            Complex w11 = GetW11();

            Complex theta = GetTheta(currentF);
            Complex sinOfTheta = Complex.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            Complex numerator = -2 * (rho11/v + r/w11) * Complex.Pow(sinOfTheta, 2) + i * (r + 1/v) * Complex.Sin(2 * theta);
            Complex a = GetA(currentF);

            Complex s12 = Complex.Divide(numerator, a);

            return s12;
        }

        /// <summary>
        /// Возвращает значение коэффициента передачи S13 для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex GetS13(double currentF)
        {

            Complex rho22 = GetRho22();
            Complex w22 = GetW22();

            Complex theta = GetTheta(currentF);
            Complex sinOfTheta = Complex.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            Complex gamma = rho22 + 1 / w22;

            Complex cosOfTheta = Complex.Cos(theta);

            Complex numerator = 2 * (2 * cosOfTheta + i * gamma * sinOfTheta);
            Complex a = GetA(currentF);

            Complex s13 = Complex.Divide(numerator, a);

            return s13;
        }

        /// <summary>
        /// Возвращает значение коэффициента передачи S24 для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex GetS24(double currentF)
        {

            Complex rho11 = GetRho11();
            Complex w11 = GetW11();

            Complex theta = GetTheta(currentF);
            Complex sinOfTheta = Complex.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            Complex beta = rho11 - 1 / w11;

            Complex cosOfTheta = Complex.Cos(theta);

            Complex numerator = 2 * (2 * cosOfTheta + i * beta * sinOfTheta);
            Complex a = GetA(currentF);

            Complex s24 = Complex.Divide(numerator, a);

            return s24;
        }

        /// <summary>
        /// Возвращает значение коэффициента передачи S14 для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex GetS14(double currentF)
        {
            Complex r = GetR();
            Complex v = GetV();
            Complex i = Complex.Sqrt(-1);
            Complex theta = GetTheta(currentF);
            Complex sinOfTheta = Complex.Sin(theta);

            
            Complex numerator = -i * 2 * (r - 1/v) * sinOfTheta;
            Complex a = GetA(currentF);

            Complex s14 = Complex.Divide(numerator, a);

            return s14;
        }

        /// <summary>
        /// Возвращает модуль определенного S-параметра для текущего Fi.
        /// </summary>
        /// <param name="type">Тип S-параметра.</param>
        /// <param name="currentF">Текущая частота.</param>
        /// <returns></returns>
        public float GetMagnitude(ParameterType type, double currentF)
        {
            float currentMagnitude;

            switch (type)
            {
                case ParameterType.S11:
                    Complex s11 = GetS11(currentF);
                    currentMagnitude = (float)(20 * Math.Log10(s11.Magnitude));
                    break;
                case ParameterType.S22:
                    Complex s22 = GetS22(currentF);
                    currentMagnitude = (float)(20 * Math.Log10(s22.Magnitude));
                    break;
                case ParameterType.S12:
                    Complex s12 = GetS12(currentF);
                    currentMagnitude = (float)(20 * Math.Log10(s12.Magnitude));
                    break;
                case ParameterType.S13:
                    Complex s13 = GetS13(currentF);
                    currentMagnitude = (float)(20 * Math.Log10(s13.Magnitude));
                    break;
                case ParameterType.S24:
                    Complex s24 = GetS24(currentF);
                    currentMagnitude = (float)(20 * Math.Log10(s24.Magnitude));
                    break;
                case ParameterType.S14:
                    Complex s14 = GetS14(currentF);
                    currentMagnitude = (float)(20 * Math.Log10(s14.Magnitude));
                    break;
                default:
                    throw new InvalidOperationException("Type is not valid.");
            }

            return currentMagnitude;          
        }

        /// <summary>
        /// Возвращает фазу определенного S-параметра для текущего Fi.
        /// </summary>
        /// <param name="type">Тип S-параметра.</param>
        /// <param name="currentF">Текущая частота.</param>
        /// <returns></returns>
        public float GetPhase(ParameterType type, double currentF)
        {
            float currentPhase;

            switch (type)
            {
                case ParameterType.S11:
                    Complex s11 = GetS11(currentF);
                    currentPhase = (float)(s11.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S22:
                    Complex s22 = GetS22(currentF);
                    currentPhase = (float)(s22.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S12:
                    Complex s12 = GetS12(currentF);
                    currentPhase = (float)(s12.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S13:
                    Complex s13 = GetS13(currentF);
                    currentPhase = (float)(s13.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S24:
                    Complex s24 = GetS24(currentF);
                    currentPhase = (float)(s24.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S14:
                    Complex s14 = GetS14(currentF);
                    currentPhase = (float)(s14.Phase * 180 / Math.PI);
                    break;
                default:
                    throw new InvalidOperationException("Type is not valid.");
            }

            return currentPhase;          
        }


        //public List<float> GetListOfMagnitudesOfS21()
        //{
        //    List<float> magnitudesList = new List<float>();

        //    float counter = (float)Fmin;
        //    while (counter <= (float)Fmax)
        //    {
        //        Complex currentS21 = GetS21(counter);

        //        // Модуль
        //        float currentMagnitude = (float)(20 * Math.Log10(currentS21.Magnitude));
        //        magnitudesList.Add(currentMagnitude);
        //        counter += _step;
        //    }

        //    return magnitudesList;
        //}

        //public List<float> GetListOfMagnitudesOfS31()
        //{
        //    List<float> magnitudesList = new List<float>();

        //    float counter = (float)Fmin;
        //    while (counter <= (float)Fmax)
        //    {
        //        Complex currentS31 = GetS31(counter);

        //        // Модуль
        //        float currentMagnitude = (float)(20 * Math.Log10(currentS31.Magnitude));
        //        magnitudesList.Add(currentMagnitude);
        //        counter += _step;
        //    }

        //    return magnitudesList;
        //}

        //public List<float> GetListOfPhasesOfS21()
        //{
        //    List<float> phasesList = new List<float>();

        //    float previousPhase = 0;

        //    float counter = (float)Fmin;
        //    while (counter <= (float)Fmax)
        //    {
        //        Complex currentS21 = GetS21(counter);

        //        // Фаза
        //        float currentPhase = (float)(Math.Atan(currentS21.Imaginary / currentS21.Real) * 180 / Math.PI);

        //        //currentPhase *= (float)Math.Pow(10, 11);

        //        if (counter != 0)
        //        {
        //            previousPhase = currentPhase;
        //        }

        //        if ((currentPhase - previousPhase) > 195)
        //        {
        //            currentPhase -= 360;
        //        }

        //        if ((currentPhase - previousPhase) > 195)
        //        {
        //            currentPhase -= 360;
        //        }

        //        phasesList.Add(currentPhase);
        //        counter += _step;
        //    }

        //    return phasesList;
        //}


        //public List<float> GetListOfPhasesOfS31()
        //{
        //    List<float> phasesList = new List<float>();

        //    float previousPhase = 0;

        //    float counter = (float)Fmin;
        //    while (counter <= (float)Fmax)
        //    {
        //        Complex currentS31 = GetS31(counter);

        //        // Фаза
        //        // Для перевода в градусы домножить на 180/Pi
        //        float currentPhase = (float)(Math.Atan(currentS31.Imaginary / currentS31.Real) * 180 / Math.PI);

        //        if (counter != 0)
        //        {
        //            previousPhase = currentPhase;
        //        }

        //        if ((currentPhase - previousPhase) > 195)
        //        {
        //            currentPhase -= 360;
        //        }

        //        if ((currentPhase - previousPhase) > 195)
        //        {
        //            currentPhase -= 360;
        //        }

        //        phasesList.Add(currentPhase);
        //        counter += _step;
        //    }

        //    return phasesList;
        //}
        //
        #endregion

        /// <summary>
        /// Возвращает клон объекта.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
