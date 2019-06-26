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
  
        /// <summary>
        /// Отрисовывает графику на плоскости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();
           
            PaintsKeeper keeper = new PaintsKeeper();
            RestrictiveFrame frame = new RestrictiveFrame(args.Info);
            
            if (FrequencyResponseSwitch.IsToggled)
            {
                WorkingSpace workingSpace = new WorkingSpace(new FrequencyResponsePlotPainter(), new ConcreteFramePainter(), new FrequencyCoordinatesPainter(), canvas);

                Plot plot = new Plot(PlotType.FrequencyResponse, frame, 2f);
                Coordinates coordinates = new Coordinates(plot.Type);

                workingSpace.PaintFrame(frame);
                workingSpace.PaintPlot(plot, UserParameters);
                workingSpace.PaintCoordinates(coordinates, UserParameters, frame);
            }

            if (PhaseResponseSwitch.IsToggled)
            {
                WorkingSpace workingSpace = new WorkingSpace(new PhaseResponsePlotPainter(), new ConcreteFramePainter(), new PhaseCoordinatesPainter(), canvas);

                Plot plot = new Plot(PlotType.PhaseResponse, frame, 2f);
                Coordinates coordinates = new Coordinates(plot.Type);

                workingSpace.PaintFrame(frame);
                workingSpace.PaintPlot(plot, UserParameters);
                workingSpace.PaintCoordinates(coordinates, UserParameters, frame);
            }
        }

        /// <summary>
        /// Событие нажатия свитча для отрисовки тригонометрических функций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFrequencyResponseSwitchToggled(object sender, EventArgs e)
        {
            if (FrequencyResponseSwitch.IsToggled)
            {
                FrequencyResponseSwitch.IsEnabled = false;
                PhaseResponseSwitch.IsEnabled = true;

                PhaseResponseSwitch.IsToggled = false;

                OutputCanvasView.InvalidateSurface();
            }
        }

        /// <summary>
        /// Событие нажатия свитча для отрисовки других графиков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPhaseResponseSwitchToggled(object sender, EventArgs e)
        {
            if (PhaseResponseSwitch.IsToggled)
            {
                PhaseResponseSwitch.IsEnabled = false;
                FrequencyResponseSwitch.IsEnabled = true;

                FrequencyResponseSwitch.IsToggled = false;

                OutputCanvasView.InvalidateSurface();
            }
        }        
    }
}
