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

namespace StarSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlanetCreate;
        private Planet createPlanet;
        private StarPlanetSystem MainSystem;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            isPlanetCreate = true;
            string basePlanetName = MainSystem.MainStar.BaseSpaceObject.Name + "-" + (MainSystem.Planets.Count + 1);
            Color basePlanetColor = Color.FromRgb(Convert.ToByte(rand.Next(256)),
                Convert.ToByte(rand.Next(256)),
                Convert.ToByte(rand.Next(256)));
            createPlanet = new Planet(basePlanetName, StarSystemParams.selectedSpaceObject, basePlanetColor);
            MainSystem.Planets.Add(new SpaceObjectCanvasAdapter(createPlanet));
            Ellipse ellipsePlanet = MainSystem.Planets[MainSystem.Planets.Count - 1].DrawFunctional();
            SpaceCanvas.Children.Add(ellipsePlanet);

            //StarSystemParams.selectedSpaceObject = MainSystem.Planets[MainSystem.Planets.Count - 1].BaseSpaceObject;
            //PrintSelectedSpaceObjectInfo();

            MessageBox.Show(MainSystem.Planets[MainSystem.Planets.Count - 1].BaseSpaceObject.Name + "\n" +
                MainSystem.Planets[MainSystem.Planets.Count - 1].BaseSpaceObject.ObjectPosition);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainSystem = new StarPlanetSystem();
            StarSystemParams.SystemCenter = new Position(SpaceCanvas.ActualWidth / 2, SpaceCanvas.ActualHeight / 2);
            SpaceCanvas.Children.Add(MainSystem.MainStar.DrawFunctional());

            StarSystemParams.selectedSpaceObject = MainSystem.MainStar.BaseSpaceObject;
            PrintSelectedSpaceObjectInfo();
        }

        private void SpaceCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SpaceObjectView.Children.Clear();
            PrintSelectedSpaceObjectInfo();
        }

        private void PrintSelectedSpaceObjectInfo()
        {
            SpaceObjectName.Text = StarSystemParams.selectedSpaceObject.Name;
            Ellipse viewPlanet = MainSystem.FindSpaceObject(StarSystemParams.selectedSpaceObject.Name).Draw(2);
            
            double left = (SpaceObjectView.ActualWidth - viewPlanet.Width) / 2;
            Canvas.SetLeft(viewPlanet, left);
            double top = (SpaceObjectView.ActualHeight - viewPlanet.Height) / 2;
            Canvas.SetTop(viewPlanet, top);

            SpaceObjectView.Children.Add(viewPlanet);
        }
    }
}
