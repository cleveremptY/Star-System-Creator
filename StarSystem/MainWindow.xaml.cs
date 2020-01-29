using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StarSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Planet createPlanet;
        double angle = 0.05;

        DispatcherTimer timerSystem;

        public MainWindow()
        {
            StarSystemParams.MainStarSystem = new StarPlanetSystem();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            string basePlanetName = StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name + "-" + (StarSystemParams.MainStarSystem.Planets.Count + 1);
            Color basePlanetColor = Color.FromRgb(Convert.ToByte(rand.Next(256)),
                Convert.ToByte(rand.Next(256)),
                Convert.ToByte(rand.Next(256)));
            createPlanet = new Planet(basePlanetName, StarSystemParams.SelectedSpaceObject, basePlanetColor);
            StarSystemParams.MainStarSystem.Planets.Add(new SpaceObjectCanvasAdapter(createPlanet));
            Ellipse ellipsePlanet = StarSystemParams.MainStarSystem.Planets[StarSystemParams.MainStarSystem.Planets.Count - 1].DrawFunctional();
            Ellipse ellipsePlanetOrbit = StarSystemParams.MainStarSystem.Planets[StarSystemParams.MainStarSystem.Planets.Count - 1].DrawOrbit();
            SpaceCanvas.Children.Add(ellipsePlanetOrbit);
            SpaceCanvas.Children.Add(ellipsePlanet);

            MessageBox.Show(StarSystemParams.MainStarSystem.Planets[StarSystemParams.MainStarSystem.Planets.Count - 1].BaseSpaceObject.Name + "\n" +
                StarSystemParams.MainStarSystem.Planets[StarSystemParams.MainStarSystem.Planets.Count - 1].BaseSpaceObject.ObjectPosition);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StarSystemParams.SystemCenter = new Position(SpaceCanvas.ActualWidth / 2, SpaceCanvas.ActualHeight / 2);
            SpaceCanvas.Children.Add(StarSystemParams.MainStarSystem.MainStar.DrawFunctional());

            timerSystem = new DispatcherTimer();
            timerSystem.Tick += new EventHandler(timerTick);
            timerSystem.Interval = new TimeSpan(0, 0, 0, 0, 30);

            StarSystemParams.SelectedSpaceObject = StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject;
            PrintSelectedSpaceObjectInfo();
        }

        private void SpaceCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RedrawSpaceObjectView();
        }

        private void PrintSelectedSpaceObjectInfo()
        {
            if (StarSystemParams.SelectedSpaceObject != null)
            {
                SpaceObjectName.Text = StarSystemParams.SelectedSpaceObject.Name;
                Ellipse viewPlanet = StarSystemParams.MainStarSystem.FindSpaceObject(StarSystemParams.SelectedSpaceObject.Name).Draw(2);

                double left = (SpaceObjectView.ActualWidth - viewPlanet.Width) / 2;
                Canvas.SetLeft(viewPlanet, left);
                double top = (SpaceObjectView.ActualHeight - viewPlanet.Height) / 2;
                Canvas.SetTop(viewPlanet, top);

                SpaceObjectView.Children.Add(viewPlanet);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RedrawAll();
        }

        private void timerTick(object sender, EventArgs e)
        {
            //double angleRadian = angle * Math.PI / 180; ;

            //Position Center = StarSystemParams.SystemCenter;
            Position Center = StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.ObjectPosition;
            Position TestPlanet = StarSystemParams.SelectedSpaceObject.ObjectPosition;

            double X = Center.X + (TestPlanet.X - Center.X) * Math.Cos(angle) - (TestPlanet.Y - Center.Y) * Math.Sin(angle);
            double Y = Center.Y + (TestPlanet.Y - Center.Y) * Math.Cos(angle) + (TestPlanet.X - Center.X) * Math.Sin(angle);

            StarSystemParams.SelectedSpaceObject.ObjectPosition = new Position(X, Y);

            RedrawSystem();
            //X = x0 + (x - x0) * cos(a) - (y - y0) * sin(a);
            //Y = y0 + (y - y0) * cos(a) + (x - x0) * sin(a);
        }

        public void RedrawAll()
        {
            RedrawSpaceObjectView();
            RedrawSystem();
        }

        public void RedrawSystem()
        {
            StarSystemParams.SystemCenter = new Position(SpaceCanvas.ActualWidth / 2, SpaceCanvas.ActualHeight / 2);
            SpaceCanvas.Children.Clear();
            SpaceCanvas.Children.Add(StarSystemParams.MainStarSystem.MainStar.DrawFunctional());
            if (StarSystemParams.MainStarSystem.Planets != null)
                foreach (var planet in StarSystemParams.MainStarSystem.Planets)
                    SpaceCanvas.Children.Add(planet.DrawFunctional());
        }

        public void RedrawSpaceObjectView()
        {
            SpaceObjectView.Children.Clear();
            PrintSelectedSpaceObjectInfo();
        }

        private void SpaceObjectName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StarSystemParams.SelectedSpaceObject == null)
                return;
            if ( StarSystemParams.MainStarSystem.FindSpaceObject(SpaceObjectName.Text) == null)
            {
                SpaceObjectName.Foreground = Brushes.Black;
                StarSystemParams.SelectedSpaceObject.Name = SpaceObjectName.Text;
            }
            else
            {
                SpaceObjectName.Foreground = Brushes.Red;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timerSystem.Start();
        }
    }
}
