using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static System.Math;

namespace TestApp
{
    /// <summary>
    /// Содержит все данные, касающиеся расчета
    /// </summary>
    class Parameters
    {
        private float _z0;
        private float _z1;
        private float _z2;
        private float _z01;
        private float _z02;
        private float _s21;

        /// <summary>
        /// Длина схемы отрезка СЛ
        /// </summary>
        private float _l;

        /// <summary>
        /// Начальная частота fi
        /// </summary>
        private float _fStart = 0;

        /// <summary>
        /// Конечная частота fi
        /// </summary>
        private float _fEnd;

        // Может быть больше единицы
        /// <summary>
        /// Диэлектрическая проницаемость среды
        /// </summary>
        private float Er = 1f;       

        /// <summary>
        /// Скорость света
        /// </summary>
        private float _c = 299_792_458f;

        private Complex _s33;
        private Complex _s44;
        private Complex _s34;
        private Complex _s43;
        private Complex _s31;
        private Complex _s42;
        private Complex _s41;
        private Complex _s23;
        private Complex _s32;

        /// <summary>
        /// Характеристический импеданс
        /// </summary>
        public float Z0
        {
            get { return _z0;}
            private set { _z0 = value;}
        }

        public float Z1
        {
            get { return _z1;}
            private set { _z1 = value;}
        }

        public float Z2
        {
            get { return _z2;}
            private set { _z2 = value;}
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z01
        /// </summary>
        public float Z01
        {
            get { return _z01; }
            private set { _z01 = value; }
        }

        /// <summary>
        /// Номинал нагрузочного резистора Z02
        /// </summary>
        public float Z02
        {
            get { return _z02;}
            private set { _z02 = value;}
        }

        public float S21
        {
            get { return _s21;}
            private set { _s21 = Math.Abs(value);}
        }

        /// <summary>
        /// Длина схемы отрезка СЛ
        /// </summary>
        public float L
        {
            get { return _l;}
            private set { _l = value;}
        }

        /// <summary>
        /// Начальная частота fi
        /// </summary>
        public float FStart
        {
            get { return _l;}
        }

        /// <summary>
        /// Конечная частота fi
        /// </summary>
        public float FEnd
        {
            get { return _fEnd; }
            private set { _fEnd = value; }
        }

        /// <summary>
        /// Скорость света
        /// </summary>
        public float C
        {
            get { return _c; }
            private set { _c = value; }
        }

        /// <summary>
        /// Конструктор, инициализирующий поля класса входными данными пользователя
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

        // Реализуем вычисление формул в геттерах свойств класса, представляющих параметры
        // Идея работать через методы вместо свойств кажется нецелесообразной

        /// <summary>
        /// Коэффициент импедансной связи
        /// </summary>
        private float K
        {
            // k = 10^(-S21/20)
            get { return (float)(Math.Pow(10, -(S21 / 20))); }            
        }

        /// <summary>
        /// Характеристический коэффициент k'
        /// </summary>
        private float KHatch
        {           
            get { return (float)(Math.Sqrt(1 - K * K)); }            
        }

        /// <summary>
        /// Коэффициент трансформации(симметрии)
        /// </summary>
        private float N
        {
            // n
            get { return (float)(Math.Sqrt(Z2 / Z1)); }
        }

        /// <summary>
        /// Электрическая длина отрезка СЛ
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        private float Theta(float currentF)
        {
            
            return (float)((360 * currentF * Math.Sqrt(Er)*L)/C);
        }

        /// <summary>
        /// Импеданс первой линии при синфазном возбуждении
        /// </summary>
        private float Z1c
        {
            get { return (float)((Z0 * KHatch) / (N - K)); }
        }

        /// <summary>
        /// Импеданс первой линии при противофазном возбуждении
        /// </summary>
        private float Z1pi
        {
            get { return (float)(Z0 * ((1/N - K) / KHatch)); }
        }

        /// <summary>
        /// Импеданс второй линии при синфазном возбуждении
        /// </summary>
        private float Z2c
        {
            get { return (float)((Z0 * KHatch) / (1/N - K)); }
        }

        /// <summary>
        /// Импеданс второй линии при противофазном возбуждении
        /// </summary>
        private float Z2pi
        {
            get { return (float)(Z0 * ((N - K) / KHatch)); }
        }

        private float Zm
        {
            get { return (float)((Z0 * KHatch) / K); }
        }

        private float Z12
        {
            get { return (float)((Z0 * K) / KHatch); }
        }

        // Параметры с тильдами ~
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
        // -

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
        /// Общий знаменатель для всех S-параметров
        /// </summary>
        /// <param name="currentF"></param>
        /// <returns></returns>
        public Complex A(float currentF)
        {
            // Обозначим комплексные числа выражения А переменными Alpha и Beta
            Complex alpha = new Complex(2 * Cos(Theta(currentF)), (Rho11 + 1/W11) * Sin(Theta(currentF)));
            Complex beta = new Complex(2 * Cos(Theta(currentF)), (Rho22 + 1 / W22) * Sin(Theta(currentF)));

            return ((Math.Pow(((R - 1 / V) * Sin(Theta(currentF))), 2)) + alpha * beta);
        }

        public Complex S11(float currentF)
        {
            // Числитель выражения
            Complex numerator = new Complex(( ((Math.Pow(R, 2) - 1/ Math.Pow(V, 2)) - (Rho11 - 1/W11) - (Rho22 - 1 / W22)) * Math.Pow(Sin(Theta(currentF)), 2) ), (Rho11 - 1 / W11) * Sin(2 * Theta(currentF)));

            // Считаем
            Complex res = (numerator / A(currentF));

            // Поскольку S11 = S33
            S33 = res;

            return res;
;        }

        // Параметр, зависящий от S11 (S11 = S33)
        public Complex S33
        {            
            get {return _s33; }
            private set { _s33 = value;}
        }

        public Complex S22(float currentF)
        {
            // Числитель выражения
            Complex numerator = new Complex((((Math.Pow(R, 2) - 1 / Math.Pow(V, 2)) - (Rho11 - 1 / W11) - (Rho22 - 1 / W22)) * Math.Pow(Sin(Theta(currentF)), 2)), (Rho22 - 1 / W22) * Sin(2 * Theta(currentF)));

            // Считаем
            Complex res = (numerator / A(currentF));

            // Поскольку S22 = S44
            S44 = res;

            return res;
        }

        // Параметр, зависящий от S22 (S22 = S44)
        public Complex S44
        {
            get { return _s44; }
            private set { _s44 = value; }
        }

        public Complex S12(float currentF)
        {
            // Числитель выражения
            Complex numerator = new Complex((-2*(Rho11/V + R/W11)*Math.Pow(Sin(Theta(currentF)),2)), ((R + 1/V) * Sin(2 * Theta(currentF))));

            // Считаем
            Complex res = (numerator / A(currentF));

            // Поскольку S12 = S21 = S34 = S43
            S34 = res;
            S43 = res;

            // Значение S21 инициализируется в конструкторе

            return res;
        }

        // Параметр, зависящий от S12 (S12 = S34)
        public Complex S34
        {
            get { return _s34; }
            private set { _s34 = value; }
        }

        // Параметр, зависящий от S12 (S12 = S43)
        public Complex S43
        {
            get { return _s43; }
            private set { _s43 = value; }
        }

        public Complex S13(float currentF)
        {
            // комплексное число, стоящее в выражении
            Complex complexNumber = new Complex(2 * Cos(Theta(currentF)), (Rho22 + 1 / W22));

            // Считаем
            Complex res = ((2 * complexNumber * Sin(Theta(currentF))) / A(currentF));

            // Поскольку S13 = S31
            S31 = res;

            return res;
        }

        // Параметр, зависящий от S13 (S13 = S31)
        public Complex S31
        {
            get { return _s31; }
            private set { _s31 = value; }
        }

        public Complex S24(float currentF)
        {
            // комплексное число, стоящее в выражении
            Complex complexNumber = new Complex(2 * Cos(Theta(currentF)), (Rho11 + 1 / W11));

            // Считаем
            Complex res = ((2 * complexNumber * Sin(Theta(currentF))) / A(currentF));

            // Поскольку S24 = S42
            S42 = res;

            return res;
        }

        // Параметр, зависящий от S24 (S24 = S42)
        public Complex S42
        {
            get { return _s42; }
            private set { _s42 = value; }
        }

        public Complex S14(float currentF)
        {
            // Объявляем комплексную мнимую единицу
            Complex i = Complex.Sqrt(-1);

            // Считаем
            Complex res = -i * (2 * (R - 1/V) * Sin(Theta(currentF)))/A(currentF);

            // Поскольку S14 = S41 = S23 = S32
            S41 = res;
            S23 = res;
            S32 = res;

            return res;
        }

        // Параметр, зависящий от S14 (S14 = S41)
        public Complex S41
        {
            get { return _s41; }
            private set { _s41 = value; }
        }

        // Параметр, зависящий от S14 (S14 = S23)
        public Complex S23
        {
            get { return _s23; }
            private set { _s23 = value; }
        }

        // Параметр, зависящий от S14 (S14 = S32)
        public Complex S32
        {
            get { return _s32; }
            private set { _s32 = value; }
        }

        #endregion

    }
}
