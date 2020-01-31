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
        public Position StartPosition
        {
            get
            {
                return new Position(orbitBase.ObjectPosition.X + OrbitRadius, orbitBase.ObjectPosition.Y);
            }
        }

        public double Angle { get; set; } = 0.001;

        public double Speed { get; set; } = 100;

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
                GeneralLineForm startLine = new GeneralLineForm(OrbitBase.ObjectPosition, StartPosition);
                GeneralLineForm currectLine = new GeneralLineForm(OrbitBase.ObjectPosition, ObjectPosition);

                int isDown = -1;
                if (ObjectPosition.Y > 0)
                    isDown = 1;

                double currectAngle = startLine.GetAngle(currectLine);
                orbitRadius = value;

                ObjectPosition = StartPosition;
                MoveByAngle(currectAngle * isDown);
            }
        }
        public Planet(string name, SpaceObject orbitBase, Color planetColor, double radius = 10, double orbitRadius = 60)
        : base(name, radius, objectColor:planetColor)
        {
            this.orbitBase = orbitBase;
            OrbitRadius = orbitRadius;
            base.ObjectPosition = StartPosition;
        }

        public void Move()
        {
            Position Center = orbitBase.ObjectPosition;

            double X = Center.X + (ObjectPosition.X - Center.X) * Math.Cos(Angle * Speed) - (ObjectPosition.Y - Center.Y) * Math.Sin(Angle * Speed);
            double Y = Center.Y + (ObjectPosition.Y - Center.Y) * Math.Cos(Angle * Speed) + (ObjectPosition.X - Center.X) * Math.Sin(Angle * Speed);

            ObjectPosition = new Position(X, Y);
        }

        public void MoveByAngle(double angle)
        {
            double angleRadian = angle * Math.PI / 180;

            MoveByRadian(angleRadian);
        }

        public void MoveByRadian(double radian)
        {
            Position Center = orbitBase.ObjectPosition;

            double X = Center.X + (ObjectPosition.X - Center.X) * Math.Cos(radian) - (ObjectPosition.Y - Center.Y) * Math.Sin(radian);
            double Y = Center.Y + (ObjectPosition.Y - Center.Y) * Math.Cos(radian) + (ObjectPosition.X - Center.X) * Math.Sin(radian);

            ObjectPosition = new Position(X, Y);
        }
    }
}
