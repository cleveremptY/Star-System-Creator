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
        SpaceObject selectedObject;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        public Ellipse Draw(SpaceObject spaceObject)
        {
            Ellipse spaceEllipse = new Ellipse();
            spaceEllipse.Width = spaceEllipse.Height = spaceObject.Radius * 2;
            spaceEllipse.VerticalAlignment = VerticalAlignment.Top;
            spaceEllipse.Fill = spaceEllipse.Stroke = new SolidColorBrush(spaceObject.ObjectColor);
            spaceEllipse.StrokeThickness = 1.5;
            SetPositon(spaceObject, spaceEllipse);
            spaceEllipse.MouseEnter += SpaceObject_MouseEnter;
            spaceEllipse.MouseLeave += SpaceObject_MouseLeave;
            return spaceEllipse;
        }

        private void SetPositon(SpaceObject spaceObject, Ellipse spaceEllipse)
        {
            double left = (SpaceCanvas.ActualWidth - spaceEllipse.ActualWidth) / 2 + spaceObject.ObjectPosition.X;
            Canvas.SetLeft(spaceEllipse, left);
            double top = (SpaceCanvas.ActualHeight - spaceEllipse.ActualHeight) / 2 + spaceObject.ObjectPosition.Y;
            Canvas.SetTop(spaceEllipse, top);
        }

        private void SpaceObject_MouseEnter(object sender, RoutedEventArgs e)
        {
            ((Ellipse)sender).Stroke = Brushes.Red;
        }

        private void SpaceObject_MouseLeave(object sender, RoutedEventArgs e)
        {
            ((Ellipse)sender).Stroke = ((Ellipse)sender).Fill;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Star mainStar = new Star("Солнце", 20, StarTypes.Yellow);
            selectedObject = mainStar;
            SpaceCanvas.Children.Add(Draw(mainStar));
        }
    }
}
