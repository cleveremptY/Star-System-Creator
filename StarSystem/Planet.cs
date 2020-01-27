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

        public SpaceObject OrbitBase { get; }
        public int OrbitRadius { get; set; }
        public Planet(string name, SpaceObject orbitBase, Color planetColor, int radius = 10, int orbitRadius = 10)
        : base(name, radius, objectColor:planetColor)
        {
            this.orbitBase = orbitBase;
            OrbitRadius = orbitRadius;
        }
    }
}
