using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using GraphicsModule.Models;
using GraphicsModule.Models.Painters;

namespace GraphicsModule
{
    /// <summary>
    /// Класс страницы вывода.
    /// </summary>
    public partial class ResultsPage : ContentPage
    {
        private Parameters _userParameters;

        /// <summary>
        /// Свойство, хранящее пользовательские данные
        /// </summary>
        public Parameters UserParameters { get => _userParameters; 
            
            private set {
                _userParameters = value ?? throw new ArgumentNullException("Parameters can't be null.");        
                }            
            }

        /// <summary>
        /// Конструктор, инициализирующий пользовательские параметры.
        /// </summary>
        /// <param name="userParameters">Параметры.</param>
        public ResultsPage(Parameters userParameters)
        {
            InitializeComponent();

            try{
                UserParameters = userParameters;

                FrequencyResponseSwitch.IsToggled = true;

                // Выводим промежуточные расчеты
                var k = Math.Round(UserParameters.GetK(), 3, MidpointRounding.ToEven);
                var n = Math.Round(UserParameters.GetN(), 3, MidpointRounding.ToEven);
                var zo = Math.Round(UserParameters.GetZo(), 1, MidpointRounding.ToEven);
                var nInTwoPower = Math.Round(1d / n, 3, MidpointRounding.ToEven);

                kLabel.Text = "k = " + k.ToString();
                nLabel.Text = "n = " + n.ToString();
                zoLabel.Text = "Zo, Ом = " + zo.ToString();
                nInTwoPowerLabel.Text = "1/n = " + nInTwoPower.ToString();
            }
            catch (ArgumentNullException)
            {

            }
        }

        /// <summary>
        /// Отрисовывает графику на плоскости.
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
        /// Событие нажатия свитча для отрисовки АЧХ.
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
        /// Событие нажатия свитча для отрисовки ФЧХ.
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
