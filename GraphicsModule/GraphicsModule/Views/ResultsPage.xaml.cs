using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using GraphicsModule.Models;
using GraphicsModule.Interfaces;
using GraphicsModule.Models;
using GraphicsModule.Models.Painters;

namespace GraphicsModule
{
    public partial class ResultsPage : ContentPage
    {
        /// <summary>
        /// Свойство, хранящее пользовательские данные
        /// </summary>
        public Parameters UserParameters { get; private set;}

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

        public ResultsPage()
        {
            //var z0 = 50;
            //var z1 = 120;
            //var z2 = 61;
            //var z01 = z1;
            //var z02 = z2;
            //var s21 = 10;
            //var l = 4.5E-3;
            //var fn = 20;

            var z0 = 50;
            var z1 = 75;
            var z2 = 50;
            var z01 = z1;
            var z02 = z2;
            var s21 = 3;
            var l = 7.5E-3;
            var fn = 20;

            UserParameters = new Parameters(z0, z1, z2, z01, z02, s21, l, fn);
            InitializeComponent();
        }
  
        /// <summary>
        /// Отрисовывает графику на плоскости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            IPlotPainter phaseResponsePlotPainter = new PhaseResponsePlotPainter();
            IPlotPainter frequencyResponsePlotPainter = new FrequencyResponsePlotPainter();
            IRestrictiveFramePainter concreteFramePainter = new ConcreteFramePainter();
            ICoordinatesPainter coordinatesPainter = new PhaseCoordinatesPainter();

            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            PaintsKeeper keeper = new PaintsKeeper();
            RestrictiveFrame frame = new RestrictiveFrame(args.Info, 0.1f);
            Plot plot = new Plot(PlotType.PhaseResponse, keeper.paints["Black Paint"], frame, 2f);
            Coordinates coordinates = new Coordinates(plot.Type);

            WorkingSpace workingSpace = new WorkingSpace(frequencyResponsePlotPainter, concreteFramePainter, coordinatesPainter, canvas);
            workingSpace.PaintFrame(frame);
            workingSpace.PaintPlot(plot, UserParameters);
            workingSpace.PaintCoordinates(coordinates, frame);

            //if (TrigonometricSwitch.IsToggled)
            //{               
            //    painter.Paint("Trigonometric");              
            //}
            
            //if (OtherSwitch.IsToggled)
            //{               
            //    painter.Paint("Other");
            //}
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
