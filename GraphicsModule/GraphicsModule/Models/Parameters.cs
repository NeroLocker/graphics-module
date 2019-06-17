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

        // Может быть больше единицы
        /// <summary>
        /// Диэлектрическая проницаемость среды.
        /// </summary>
        private double Er = 2.8f;

        /// <summary>
        /// Коэффициент импедансной связи.
        /// </summary>
        private double _k;

        /// <summary>
        /// Характеристический импеданс.
        /// </summary>
        public double Z0 { get; private set; }

        /// <summary>
        /// Характеристический импеданс первой линии.
        /// </summary>
        public double Z1 { get; private set; }

        /// <summary>
        /// Характеристический импеданс второй линии.
        /// </summary>
        public double Z2 { get; private set; }

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
        public double S21 { get; private set; }

        /// <summary>
        /// Геометрическая длина схемы отрезка СЛ.
        /// </summary>
        public double L { get; private set; }

        /// <summary>
        /// Начальная частота Fn.
        /// </summary>
        public double FStart { get; private set; }

        /// <summary>
        /// Конечная частота Fn.
        /// </summary>
        public double FEnd { get; private set; }

        /// <summary>
        /// Скорость света.
        /// </summary>
        public double C { get; private set; }

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
        public Parameters(double z0, double z1, double z2, double z01, double z02, double s21, double l, double fEnd)
        {
            Z0 = z0;
            Z1 = z1;
            Z2 = z2;
            Z01 = z01;
            Z02 = z02;
            S21 = s21;
            L = l * Math.Pow(10, -4);

            FStart = 0;
            FEnd = fEnd;

            C = 299792458;
        }

        # region Параметры, вычисляемые формулами

        /// <summary>
        /// Коэффициент импедансной связи.
        /// </summary>
        private double GetK()
        {
            // k = 10^(-S21/20)
            double result = (Math.Pow(10, -(S21 / 20)));

            return result;
        }

        /// <summary>
        /// Характеристический коэффициент k'.
        /// </summary>
        private double KHatch
        {           
            get { return (Math.Sqrt(1 - GetK() * GetK())); }            
        }

        /// <summary>
        /// Коэффициент трансформации(симметрии).
        /// </summary>
        private double N
        {
            // n
            get { return (Math.Sqrt(Z2 / Z1)); }
        }

        /// <summary>
        /// Электрическая длина отрезка СЛ.
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex GetTheta(double currentF)
        {
            Complex i = Complex.Sqrt(-1);            
            Complex result = ((i * GetOmega(currentF) * Math.Sqrt(Er) * L) / C);

            return result;
        }

        # region Нагрузочные резисторы

        /// <summary>
        /// Номинал нагрузочного резистора Z1c.
        /// </summary>
        private double Z1c
        {
            get { return ((Z0 * KHatch) / (N - GetK())); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z1π.
        /// </summary>
        private double Z1pi
        {
            get { return Z0 * (1/N - GetK())/KHatch; }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2c.
        /// </summary>
        private double Z2c
        {
            get { return ((Z0 * KHatch)/(1/N - GetK())); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z2π.
        /// </summary>
        private double Z2pi
        {
            get { return Z0 * (N - GetK())/KHatch; }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Zm.
        /// </summary>
        private double Zm
        {
            get { return ((Z0 * KHatch) / GetK()); }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z12.
        /// </summary>
        private double Z12
        {
            get { return ((Z0 * GetK()) / KHatch); }
        }

        #endregion

        # region Параметры с тильдами (~)
        private double W11Tilda
        {
            get { return ((Z0 * KHatch) / N); }
        }

        private double W22Tilda
        {
            get { return (Z0 * KHatch * N); }
        }

        private double Rho11Tilda
        {
            get { return (Z0 / (N * KHatch)); }
        }
        private double Rho22Tilda
        {
            get { return ((Z0 * N) / KHatch); }
        }

        private double UTilda
        {
            //get { return ((Z0 * KHatch) / K); }
            //UTilda = Zm
            get => Zm;
        }

        private double RTilda
        {
            //get { return ((Z0 * K) / KHatch); }
            // RTilda = z12
            get => Z12;
        }
        
        # endregion

        private double Rho11
        {
            get { return (Rho11Tilda / Z01); }
        }

        private double Rho22
        {
            get { return (Rho22Tilda / Z02); }
        }

        private double R
        {
            get { return (RTilda / Math.Sqrt(Z01 * Z02)); }
        }

        private double W11
        {
            get { return (W11Tilda / Z01); }
        }

        private double W22
        {
            get { return (W22Tilda / Z02); }
        }

        private double V
        {
            get { return (Zm / Math.Sqrt(Z01 * Z02)); }
        }

        #endregion

        public double GetOmega(double currentF)
        {
            // Точно правильно!
            double result = (2 * Math.PI * currentF * Math.Pow(10, 9));

            return result;
        }

        # region S-параметры

        public Complex GetZCT(float currentF)
        {
            Complex currentTheta = GetTheta(currentF);
            //гиперболический котангенс
            Complex cothOfTheta = 1/Complex.Tanh(currentTheta);

            Complex result = Z0/KHatch * cothOfTheta;

            return result;
        }

        public Complex GetZCS(float currentF)
        {
            Complex currentTheta = GetTheta(currentF);
            //гиперболический косеканс 
            Complex cschOfTheta = 1 / Complex.Sinh(currentTheta);

            Complex result = Z0 / KHatch * cschOfTheta;

            return result;
        }

        public Complex GetZ12(float currentF)
        {
            Complex result = GetZCT(currentF) * GetK();

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
        public Complex GetA(double currentF)
        {
            // Точно правильно
            Complex i = Complex.Sqrt(-1);
            Complex currentTheta = GetTheta(currentF);

            Complex sinTheta = Complex.Sin(currentTheta);
            Complex cosTheta = Complex.Cos(currentTheta);

            Complex alpha = Complex.Pow((R - 1 / V) * sinTheta, 2);            

            Complex beta = 2 * cosTheta + i * (Rho11 + 1/W11) * sinTheta;

            Complex gamma = 2 * cosTheta + i * (Rho22 + 1/W22) * sinTheta;

            Complex multiplication = Complex.Multiply(beta, gamma);

            Complex result = Complex.Add(alpha, multiplication);

            return result;
        }

        public Complex GetS21(double currentF)
        {
            Complex i = Complex.Sqrt(-1);

            Complex currentTheta = GetTheta(currentF);

            Complex sinThetaOfPowerOf2 = Complex.Pow(Complex.Sin(currentTheta), 2);
            Complex sin2Theta = Complex.Sin(2 * currentTheta);

            Complex numerator = -2 * (Rho11/V + R/W11) * sinThetaOfPowerOf2 + i * (R + 1/V) * sin2Theta;

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

        public Complex GetS31(double currentF)
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
        #endregion
    }
}
