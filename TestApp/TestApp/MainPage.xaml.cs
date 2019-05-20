using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace TestApp
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Список схем включения
        /// </summary>
        public IList<Scheme> Schemes { get; private set; }

        /// <summary>
        /// Класс схемы
        /// </summary>
        public class Scheme
        {
            public string Name { get; set; }
            public string ImagePath { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            // Заполняем список данными
            Schemes = new List<Scheme>();

            Schemes.Add(new Scheme
            {
                Name = "Ответвитель",
                ImagePath = "coupler.jpg"
            });

            Schemes.Add(new Scheme
            {
                Name = "Мост-делитель",
                ImagePath = "bridge_divider.jpg"                
            });

            Schemes.Add(new Scheme
            {
                Name = "Трансформатор",
                ImagePath = "transformer.jpg"
            });

            BindingContext = this;
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
        /// Событие выбора схемы из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Scheme selectedItem = e.SelectedItem as Scheme;
        }

        /// <summary>
        /// Событие нажатия на элемент списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Scheme tappedItem = e.Item as Scheme;
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
