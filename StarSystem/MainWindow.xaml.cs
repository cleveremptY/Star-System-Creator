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
        StarPlanetSystem MainSystem;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SpaceCanvas.Children.Add(MainSystem.MainStar.Draw());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainSystem = new StarPlanetSystem();
            StarSystemParams.SystemCenter = new Position(SpaceCanvas.ActualWidth / 2, SpaceCanvas.ActualHeight / 2);
            SpaceCanvas.Children.Add(MainSystem.MainStar.Draw());

            StarSystemParams.selectedSpaceObject = MainSystem.MainStar.BaseSpaceObject;
            PrintSelectedSpaceObjectInfo();
        }

        private void SpaceCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrintSelectedSpaceObjectInfo();
        }

        private void PrintSelectedSpaceObjectInfo()
        {
            SpaceObjectName.Text = StarSystemParams.selectedSpaceObject.Name;
        }
    }
}
