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
        private Ellipse ellipseSpaceObject;
        private Ellipse senderObject;

        public SpaceObject BaseSpaceObject
        {
            get
            {
                return baseSpaceObject;
            }
        }

        public Ellipse EllipseSpaceObject
        {
            get
            {
                return senderObject;
            }
        }

        public SpaceObjectCanvasAdapter(SpaceObject spaceObject)
        {
            baseSpaceObject = spaceObject;
            ellipseSpaceObject = DrawFunctional();
        }

        public Ellipse DrawFunctional(double resizeParam = 1)
        {
            Ellipse spaceEllipse = Draw(resizeParam);
            if (isPressed)
                spaceEllipse.Stroke = Brushes.Red;
            spaceEllipse.StrokeThickness = 1.5;
            SetPositonCenter(spaceEllipse);
            SetActions(spaceEllipse);
            return spaceEllipse;
        }

        public Ellipse Draw(double resizeParam = 1)
        {
            Ellipse spaceEllipse = new Ellipse();
            spaceEllipse.Width = spaceEllipse.Height = BaseSpaceObject.Radius * 2 * resizeParam;
            spaceEllipse.Fill = spaceEllipse.Stroke = new SolidColorBrush(BaseSpaceObject.ObjectColor);
            spaceEllipse.StrokeThickness = 1.5;
            return spaceEllipse;
        }

        public Ellipse DrawOrbit()
        {
            if (BaseSpaceObject is Planet)
            {
                Ellipse orbitEllipse = new Ellipse();
                Planet BaseSpaceObjectPlanet = (Planet)BaseSpaceObject;
                orbitEllipse.Width = orbitEllipse.Height = BaseSpaceObjectPlanet.OrbitRadius * 2;
                orbitEllipse.Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                orbitEllipse.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                orbitEllipse.StrokeThickness = 1.5;
                SetPositonCenter(orbitEllipse, StarSystemParams.SystemCenter, BaseSpaceObjectPlanet.OrbitBase.ObjectPosition);
                return orbitEllipse;
            }
            return null;
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
            SetPositonCenter(spaceEllipse, StarSystemParams.SystemCenter);
        }

        protected void SetPositonCenter(Ellipse spaceEllipse, Position canvasCenter)
        {
            SetPositonCenter(spaceEllipse, canvasCenter, BaseSpaceObject.ObjectPosition);
        }

        protected void SetPositonCenter(Ellipse spaceEllipse, Position canvasCenter, Position offset)
        {
            spaceEllipse.VerticalAlignment = VerticalAlignment.Center;
            spaceEllipse.HorizontalAlignment = HorizontalAlignment.Center;

            double left = canvasCenter.X - spaceEllipse.Width / 2 + offset.X;
            Canvas.SetLeft(spaceEllipse, left);
            double top = canvasCenter.Y - spaceEllipse.Height / 2 + offset.Y;
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
            foreach (var planet in StarSystemParams.MainStarSystem.AllSpaceObjects)
                if (planet.isPressed)
                {
                    planet.isPressed = false;
                    planet.senderObject.Stroke = planet.senderObject.Fill;
                    break;
                }
            isPressed = true;
            senderObject = ((Ellipse)sender);
            senderObject.Stroke = Brushes.Red;
            StarSystemParams.SelectedSpaceObject = baseSpaceObject;
        }

        private void Star_MouseDown(object sender, RoutedEventArgs e)
        {
            //((Ellipse)sender).Stroke = Brushes.Red;
        }
    }
}
