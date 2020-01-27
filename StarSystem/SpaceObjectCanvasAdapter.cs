using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StarSystem
{
    public class SpaceObjectCanvasAdapter
    {
        private SpaceObject baseSpaceObject;
        public SpaceObject BaseSpaceObject
        {
            get
            {
                return baseSpaceObject;
            }
        }
        public Position SystemCenter { get; set; }

        public SpaceObjectCanvasAdapter(SpaceObject spaceObject, Position systemCenter = new Position())
        {
            baseSpaceObject = spaceObject;
            SystemCenter = systemCenter;
        }

        public Ellipse Draw()
        {
            Ellipse spaceEllipse = new Ellipse();
            spaceEllipse.Width = spaceEllipse.Height = BaseSpaceObject.Radius * 2;
            spaceEllipse.VerticalAlignment = VerticalAlignment.Top;
            spaceEllipse.Fill = spaceEllipse.Stroke = new SolidColorBrush(BaseSpaceObject.ObjectColor);
            spaceEllipse.StrokeThickness = 1.5;
            SetPositon(spaceEllipse);
            spaceEllipse.MouseEnter += SpaceObject_MouseEnter;
            spaceEllipse.MouseLeave += SpaceObject_MouseLeave;
            return spaceEllipse;
        }

        //Установка позиции относительно центра канваса
        private void SetPositon(Ellipse spaceEllipse)
        {
            double left = SystemCenter.X - spaceEllipse.ActualWidth / 2 + BaseSpaceObject.ObjectPosition.X;
            Canvas.SetLeft(spaceEllipse, left);
            double top = SystemCenter.Y - spaceEllipse.ActualHeight / 2 + BaseSpaceObject.ObjectPosition.Y;
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
    }
}
