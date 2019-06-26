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
        private double _step = 0.04f;

        // Может быть больше единицы
        /// <summary>
        /// Диэлектрическая проницаемость среды.
        /// </summary>
        private double Er { get; set;}

        /// <summary>
        /// Коэффициент импедансной связи.
        /// </summary>
        private double _k;

        private double _z0;

        private double _z1;

        private double _z2;

        private double _s21;

        private double _l;

        /// <summary>
        /// Характеристический импеданс первой линии.
        /// </summary>
        public double Z1
        {
            get => _z1;
            private set
            {
                if (!(value >= 1 && value <= 100))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                _z1 = value;
            }
        }

        /// <summary>
        /// Характеристический импеданс второй линии.
        /// </summary>
        public double Z2
        {
            get => _z2; 
            private set
            {
                if (!(value >= 1 && value <= 100))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                _z2 = value;
            }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z01.
        /// </summary>
        public double Z01 { get; private set; }

        /// <summary>
        /// Номинал нагрузочного резистора Z02.
        /// </summary>
        public double Z02 { get; private set; }

        /// <summary>
        /// Коэффициент связи 1-ой и 2-ой линии.
        /// </summary>
        public double S21
        {
            get => _s21; private set
            {
                if (!(value >= 2 && value <= 10))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                _s21 = value;
            }
        }

        /// <summary>
        /// Геометрическая длина схемы отрезка СЛ.
        /// </summary>
        public double L { get => _l; private set {
                if (!(value >= 10 && value <= 100))
                {
                    throw new ArgumentException("Value is not in valid range");
                }

                _l = value * Math.Pow(10, -3); } }

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
        private double C { get; set; }

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
            //Fmin = fmin + 0.04;
            Fmin = fmin;
            Fmax = fmax;
            L = l;
            Er = er;
            S21 = s21;
            Z01 = z01;
            Z02 = z02;
            Z1 = z1;
            Z2 = z2;

            C = 2.998E8;  
        }

        # region Параметры, вычисляемые формулами

        /// <summary>
        /// Возвращает коэффициент симметрии n.
        /// </summary>
        /// <returns></returns>
        private double GetN()
        {
            double z2 = Z2;
            double z1 = Z1;

            double n = Math.Sqrt(z2/z1);
            return n;
        }

        /// <summary>
        /// Возващает характеристическое сопротивление СЛ Zo.
        /// </summary>
        /// <returns></returns>
        private double GetZo()
        {
            double z2 = Z2;
            double z1 = Z1;

            double zo = Math.Sqrt(z1 * z2);
            return zo;
        }

        /// <summary>
        /// Возвращает среднее геометрическое сопротивление нагрузок Z0.
        /// </summary>
        /// <returns></returns>
        private double GetZ0()
        {
            double z01 = Z01;
            double z02 = Z02;

            double z0 = Math.Sqrt(z01 * z02);
            return z0;
        }

        /// <summary>
        /// Возвращает характеристический коэффициент k'.
        /// </summary>
        private double GetKHatch()
        {
            double k = GetK();
            double kHatch = Math.Sqrt(1 - Math.Pow(k, 2));

            return kHatch;
        }

        /// <summary>
        /// Возвращает ρ11.
        /// </summary>
        /// <returns></returns>
        private double GetRho11()
        {
            double zo = GetZo();
            double z01 = Z01;
            double n = GetN();
            double kHatch = GetKHatch();

            double rho11 = zo/(z01 * n * kHatch);

            return rho11;
        }

        /// <summary>
        /// Возвращает ρ22.
        /// </summary>
        /// <returns></returns>
        private double GetRho22()
        {
            double zo = GetZo();
            double z02 = Z02;
            double n = GetN();
            double kHatch = GetKHatch();

            double rho22 = (zo * n)/(z02 * kHatch);

            return rho22;
        }

        /// <summary>
        /// Возвращает r.
        /// </summary>
        /// <returns></returns>
        private double GetR()
        {
            double zo = GetZo();
            double z0 = GetZ0();
            double k = GetK();
            double kHatch = GetKHatch();

            double r = (zo * k)/(z0 * kHatch);

            return r;
        }

        /// <summary>
        /// Возвращает W11.
        /// </summary>
        /// <returns></returns>
        private double GetW11()
        {
            double zo = GetZo();
            double kHatch = GetKHatch();
            double z01 = Z01;
            double n = GetN();
            
            double w11 = (zo * kHatch)/(z01 * n);

            return w11;
        }

        /// <summary>
        /// Возвращает W22.
        /// </summary>
        /// <returns></returns>
        private double GetW22()
        {
            double zo = GetZo();
            double kHatch = GetKHatch();
            double z02 = Z02;
            double n = GetN();

            double w22 = (zo * kHatch * n)/(z02);

            return w22;
        }

        /// <summary>
        /// Возвращает v.
        /// </summary>
        /// <returns></returns>
        private double GetV()
        {
            double zo = GetZo();
            double kHatch = GetKHatch();
            double z0 = GetZ0();
            double k = GetK();

            double v = (zo * kHatch)/(z0 * k);

            return v;
        }

        #endregion



        /// <summary>
        /// Возвращает значение ω для текущего Fi.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        private double GetOmega(double currentF)
        {
            double omega = (2 * Math.PI * currentF * Math.Pow(10, 9));

            return omega;
        }

        /// <summary>
        /// Возвращает электрическую длину отрезка СЛ (рад).
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        private double GetTheta(double currentF)
        {
            double omega = GetOmega(currentF);

            double theta = ((omega * Math.Sqrt(Er) * L) / C);

            return theta;
        }

        /// <summary>
        /// Возвращает коэффициент импедансной связи.
        /// </summary>
        private double GetK()
        {
            // k = 10^(-S21/20)
            double k = Math.Pow(10, -S21/20);

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
            double r = GetR();
            double v = GetV();
            double rho11 = GetRho11();
            double w11 = GetW11();

            double rho22 = GetRho22();
            double w22 = GetW22();

            double theta = GetTheta(currentF);
            double sinOfTheta = Math.Sin(theta);
            double cosOfTheta = Math.Cos(theta);

            Complex i = Complex.Sqrt(-1);

            double alpha = Math.Pow((r - 1/v) * sinOfTheta, 2);
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
            double r = GetR();
            double v = GetV();
            double rho11 = GetRho11();
            double w11 = GetW11();
            double rho22 = GetRho22();
            double w22 = GetW22();

            double theta = GetTheta(currentF);
            double sinOfTheta = Math.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            double alpha = Math.Pow(r, 2) - 1/Math.Pow(v, 2);
            double beta = rho11 - 1/w11;
            double gamma = rho22 + 1/w22;

            Complex numerator = (alpha - beta * gamma) * Math.Pow(sinOfTheta, 2) + i * beta * Math.Sin(2 * theta);
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
            double r = GetR();
            double v = GetV();
            double rho11 = GetRho11();
            double w11 = GetW11();
            double rho22 = GetRho22();
            double w22 = GetW22();

            double theta = GetTheta(currentF);
            double sinOfTheta = Math.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            double alpha = Math.Pow(r, 2) - 1 / Math.Pow(v, 2);
            double beta = rho11 - 1 / w11;
            double gamma = rho22 + 1 / w22;

            Complex numerator = (alpha - beta * gamma) * Math.Pow(sinOfTheta, 2) + i * gamma * Math.Sin(2 * theta);
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
            double r = GetR();
            double v = GetV();
            double rho11 = GetRho11();
            double w11 = GetW11();

            double theta = GetTheta(currentF);
            double sinOfTheta = Math.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            Complex numerator = -2 * (rho11/v + r/w11) * Math.Pow(sinOfTheta, 2) + i * (r + 1/v) * Math.Sin(2 * theta);
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

            double rho22 = GetRho22();
            double w22 = GetW22();

            double theta = GetTheta(currentF);
            double sinOfTheta = Math.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            double gamma = rho22 + 1 / w22;

            double cosOfTheta = Math.Cos(theta);

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

            double rho11 = GetRho11();
            double w11 = GetW11();

            double theta = GetTheta(currentF);
            double sinOfTheta = Math.Sin(theta);

            Complex i = Complex.Sqrt(-1);

            double beta = rho11 - 1 / w11;

            double cosOfTheta = Math.Cos(theta);

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
            double r = GetR();
            double v = GetV();
            Complex i = Complex.Sqrt(-1);
            double theta = GetTheta(currentF);
            double sinOfTheta = Math.Sin(theta);

            
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
        public double GetMagnitude(ParameterType type, double currentF)
        {
            double currentMagnitude;

            switch (type)
            {
                case ParameterType.S11:
                    Complex s11 = GetS11(currentF);
                    currentMagnitude = (20 * Math.Log10(s11.Magnitude));
                    break;
                case ParameterType.S22:
                    Complex s22 = GetS22(currentF);
                    currentMagnitude = (20 * Math.Log10(s22.Magnitude));
                    break;
                case ParameterType.S12:
                    Complex s12 = GetS12(currentF);
                    currentMagnitude = (20 * Math.Log10(s12.Magnitude));
                    break;
                case ParameterType.S13:
                    Complex s13 = GetS13(currentF);
                    currentMagnitude = (20 * Math.Log10(s13.Magnitude));
                    break;
                case ParameterType.S24:
                    Complex s24 = GetS24(currentF);
                    currentMagnitude = (20 * Math.Log10(s24.Magnitude));
                    break;
                case ParameterType.S14:
                    Complex s14 = GetS14(currentF);
                    currentMagnitude = (20 * Math.Log10(s14.Magnitude));
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
        public double GetPhase(ParameterType type, double currentF)
        {
            double currentPhase;

            switch (type)
            {
                case ParameterType.S11:
                    Complex s11 = GetS11(currentF);
                    currentPhase = (s11.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S22:
                    Complex s22 = GetS22(currentF);
                    currentPhase = (s22.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S12:
                    Complex s12 = GetS12(currentF);
                    currentPhase = (s12.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S13:
                    Complex s13 = GetS13(currentF);
                    currentPhase = (s13.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S24:
                    Complex s24 = GetS24(currentF);
                    currentPhase = (s24.Phase * 180 / Math.PI);
                    break;
                case ParameterType.S14:
                    Complex s14 = GetS14(currentF);
                    currentPhase = (s14.Phase * 180 / Math.PI);
                    break;
                default:
                    throw new InvalidOperationException("Type is not valid.");
            }

            return currentPhase;          
        }
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
