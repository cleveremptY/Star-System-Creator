using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StarSystem
{
    class Planet : SpaceObject
    {
        private SpaceObject orbitBase;
        private double orbitRadius;

        public double Angle { get; set; } = 0.05;

        public SpaceObject OrbitBase
        {
            get
            {
                return orbitBase;
            }
        }
        public double OrbitRadius {
            get
            {
                return orbitRadius;
            }
            set
            {
                GeneralLineForm startLine = new GeneralLineForm(OrbitBase.ObjectPosition, new Position(orbitBase.ObjectPosition.X + OrbitRadius,
                orbitBase.ObjectPosition.Y));

                GeneralLineForm currectLine = new GeneralLineForm(OrbitBase.ObjectPosition, ObjectPosition);

                double currectAngle = startLine.GetAngle(currectLine);

                orbitRadius = value;
                ObjectPosition = new Position(orbitBase.ObjectPosition.X + OrbitRadius,
                orbitBase.ObjectPosition.Y);
                MoveByAngle(currectAngle);
            }
        }
        public Planet(string name, SpaceObject orbitBase, Color planetColor, double radius = 10, double orbitRadius = 60)
        : base(name, radius, objectColor:planetColor)
        {
            //Random rnd = new Random();
            //orbitRadius = rnd.Next(15, 100);
            //base.Radius = rnd.Next(5, 50);

            this.orbitBase = orbitBase;
            OrbitRadius = orbitRadius;
            base.ObjectPosition = new Position(orbitBase.ObjectPosition.X  + OrbitRadius, 
                orbitBase.ObjectPosition.Y);
        }

        public void Move()
        {
            Position Center = orbitBase.ObjectPosition;

            double X = Center.X + (ObjectPosition.X - Center.X) * Math.Cos(Angle) - (ObjectPosition.Y - Center.Y) * Math.Sin(Angle);
            double Y = Center.Y + (ObjectPosition.Y - Center.Y) * Math.Cos(Angle) + (ObjectPosition.X - Center.X) * Math.Sin(Angle);

            ObjectPosition = new Position(X, Y);
        }

        public void MoveByAngle(double angle)
        {
            double angleRadian = angle * Math.PI / 180;

            Position Center = orbitBase.ObjectPosition;

            double X = Center.X + (ObjectPosition.X - Center.X) * Math.Cos(angleRadian) - (ObjectPosition.Y - Center.Y) * Math.Sin(angleRadian);
            double Y = Center.Y + (ObjectPosition.Y - Center.Y) * Math.Cos(angleRadian) + (ObjectPosition.X - Center.X) * Math.Sin(angleRadian);

            ObjectPosition = new Position(X, Y);
        }
    }
}
