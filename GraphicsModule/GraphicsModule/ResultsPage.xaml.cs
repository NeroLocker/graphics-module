using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace GraphicsModule
{
    public partial class ResultsPage : ContentPage
    {
        /// <summary>
        /// Свойство, хранящее пользовательские данные
        /// </summary>
        private Parameters UserParameters { get; set;}

        /// <summary>
        /// Конструктор, инициализирующий свойство пользовательских параметров
        /// </summary>
        /// <param name="userParameters"></param>
        public ResultsPage(Parameters userParameters)
        {
            InitializeComponent();

            if (userParameters != null)
            {
                UserParameters = userParameters;
            }
        }
  
        /// <summary>
        /// Отрисовывает графику на плоскости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            PlotPainter plotPainter = new PlotPainter(sender, args);

            if (TrigonometricSwitch.IsToggled)
            {               
                plotPainter.Paint("Trigonometric");              
            }
            
            if (OtherSwitch.IsToggled)
            {               
                plotPainter.Paint("Other");
            }
        }

        /// <summary>
        /// Событие нажатия свитча для отрисовки тригонометрических функций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrigonometricSwitchToggled(object sender, EventArgs e)
        {
            if (TrigonometricSwitch.IsToggled)
            {
                TrigonometricSwitch.IsEnabled = false;
                OtherSwitch.IsEnabled = true;

                OtherSwitch.IsToggled = false;

                OutputCanvasView.InvalidateSurface();
            }
        }

        /// <summary>
        /// Событие нажатия свитча для отрисовки других графиков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOtherSwitchToggled(object sender, EventArgs e)
        {
            if (OtherSwitch.IsToggled)
            {
                OtherSwitch.IsEnabled = false;
                TrigonometricSwitch.IsEnabled = true;

                TrigonometricSwitch.IsToggled = false;

                OutputCanvasView.InvalidateSurface();
            }
        }        
    }
}
