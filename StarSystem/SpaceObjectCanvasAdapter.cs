﻿using System;
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
        private bool isPressed;
        private SpaceObject baseSpaceObject;
        public SpaceObject BaseSpaceObject
        {
            get
            {
                return baseSpaceObject;
            }
        }

        public SpaceObjectCanvasAdapter(SpaceObject spaceObject)
        {
            baseSpaceObject = spaceObject;
        }

        public Ellipse DrawFunctional(double resizeParam = 1)
        {
            Ellipse spaceEllipse = Draw(resizeParam);
            spaceEllipse.StrokeThickness = 1.5;
            SetPositonCenter(spaceEllipse);
            SetActions(spaceEllipse);
            return spaceEllipse;
        }

        public Ellipse Draw(double resizeParam = 1)
        {
            Ellipse spaceEllipse = new Ellipse();
            spaceEllipse.Width = spaceEllipse.Height = BaseSpaceObject.Radius * 2 * resizeParam;
            spaceEllipse.VerticalAlignment = VerticalAlignment.Top;
            spaceEllipse.Fill = spaceEllipse.Stroke = new SolidColorBrush(BaseSpaceObject.ObjectColor);
            spaceEllipse.StrokeThickness = 1.5;
            return spaceEllipse;
        }

        private void SetActions(Ellipse spaceEllipse)
        {
            spaceEllipse.MouseEnter += SpaceObject_MouseEnter;
            spaceEllipse.MouseLeave += SpaceObject_MouseLeave;
            spaceEllipse.MouseDown += SpaceObject_MouseDown;
            if (baseSpaceObject is Star)
                spaceEllipse.MouseDown += Star_MouseDown;
        }

        protected void SetPositonCenter(Ellipse spaceEllipse)
        {
            //double left = StarSystemParams.SystemCenter.X - spaceEllipse.ActualWidth / 2 + BaseSpaceObject.ObjectPosition.X;
            //Canvas.SetLeft(spaceEllipse, left);
            //double top = StarSystemParams.SystemCenter.Y - spaceEllipse.ActualHeight / 2 + BaseSpaceObject.ObjectPosition.Y;
            //Canvas.SetTop(spaceEllipse, top);
            SetPositonCenter(spaceEllipse, StarSystemParams.SystemCenter);
        }

        protected void SetPositonCenter(Ellipse spaceEllipse, Position canvasCenter)
        {
            double left = canvasCenter.X - spaceEllipse.Width / 2 + BaseSpaceObject.ObjectPosition.X;
            Canvas.SetLeft(spaceEllipse, left);
            double top = canvasCenter.Y - spaceEllipse.Height / 2 + BaseSpaceObject.ObjectPosition.Y;
            Canvas.SetTop(spaceEllipse, top);
        }

        private void SpaceObject_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (!isPressed)
                ((Ellipse)sender).Stroke = Brushes.RosyBrown;
        }

        private void SpaceObject_MouseLeave(object sender, RoutedEventArgs e)
        {
            if (!isPressed)
                ((Ellipse)sender).Stroke = ((Ellipse)sender).Fill;
        }

        private void SpaceObject_MouseDown(object sender, RoutedEventArgs e)
        {
            isPressed = true;
            ((Ellipse)sender).Stroke = Brushes.Red;
            StarSystemParams.selectedSpaceObject = baseSpaceObject;
        }

        private void Star_MouseDown(object sender, RoutedEventArgs e)
        {
            //((Ellipse)sender).Stroke = Brushes.Red;
        }
    }
}
