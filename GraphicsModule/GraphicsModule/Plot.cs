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
        private string _name;
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
