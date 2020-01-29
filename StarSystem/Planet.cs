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
    }
}
