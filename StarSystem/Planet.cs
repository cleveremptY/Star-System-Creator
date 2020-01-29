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

        public double Angle { get; set; } = 0.05;

        public SpaceObject OrbitBase
        {
            get
            {
                return orbitBase;
            }
        }
        public int OrbitRadius { get; set; }
        public Planet(string name, SpaceObject orbitBase, Color planetColor, int radius = 10, int orbitRadius = 60)
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

            //X = x0 + (x - x0) * cos(a) - (y - y0) * sin(a);
            //Y = y0 + (y - y0) * cos(a) + (x - x0) * sin(a);
        }
    }
}
