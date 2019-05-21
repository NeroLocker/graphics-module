using System;
using System.Collections.Generic;
using System.Text;

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
        private float _l;
        /// <summary>
        /// Начальная частота fi
        /// </summary>
        private float _fStart = 0;
        /// <summary>
        /// Конечная частота fi
        /// </summary>
        private float _fEnd;
        private float eps = (float)8.854E-12;
        /// <summary>
        /// Скорость света
        /// </summary>
        private float _c = 299_792_458f;


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

        public float Z01
        {
            get { return _z01; }
            private set { _z01 = value; }
        }

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
            get { return _fEnd; }
            private set { _fEnd = value; }
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
        private float K
        {
            // k = 10^(-S21/20)
            get { return (float)(Math.Pow(10, -(S21 / 20))); }            
        }

        private float KHatch
        {
            // k'
            get { return (float)(Math.Sqrt(1 - K * K)); }            
        }

        private float N
        {
            // n
            get { return (float)(Math.Sqrt(Z2 / Z1)); }
        }

        private float Theta(float currentF)
        {
            
            return (float)((360*currentF*Math.Sqrt(eps)*L)/C);
        }
        
        private float Z1c
        {
            get { return (float)((Z0 * KHatch) / (N - K)); }
        }
        private float Z1pi
        {
            get { return (float)(Z0 * ((1/N - K) / KHatch)); }
        }
        private float Z2c
        {
            get { return (float)((Z0 * KHatch) / (1/N - K)); }
        }
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
        



        #endregion

    }
}
