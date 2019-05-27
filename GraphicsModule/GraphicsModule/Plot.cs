using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModule
{
    /// <summary>
    /// График
    /// </summary>
    class Plot
    {
        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// Имя
        /// </summary>
        private string Name
        {
            get{ return _name;} 
            set
            {
                if ((value != "Frequency Response") || (value != "Phase Response"))
                {
                    throw new ArgumentException("This name is forbidden");
                }
                else
                {
                    _name = value;
                }

            }
        }
    }
}
