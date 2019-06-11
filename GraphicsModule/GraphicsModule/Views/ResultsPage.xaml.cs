﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using GraphicsModule.Models;
using GraphicsModule.Interfaces;
using GraphicsModule.Painters;

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
            IPlotPainter phaseResponsePlotPainter = new PhaseResponsePlotPainter();
            IFramePainter concreteFramePainter = new ConcreteFramePainter();
            ICoordinatesPainter coordinatesPainter = new PhaseCoordinatesPainter();

            SKCanvas canvas = args.Surface.Canvas;
            PaintsKeeper keeper = new PaintsKeeper();
            Models.Frame frame = new Models.Frame(args.Info, 0.1f);
            Plot plot = new Plot(PlotType.PhaseResponse, keeper.paints["Black Paint"], frame, 2f);
            Coordinates coordinates = new Coordinates(plot.Type);

            WorkingSpace workingSpace = new WorkingSpace(phaseResponsePlotPainter, concreteFramePainter, coordinatesPainter, canvas);
            workingSpace.PaintFrame(frame);
            workingSpace.PaintPlot(plot);
            workingSpace.PaintCoordinates(coordinates, frame, canvas);

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
