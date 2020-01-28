using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    class StarPlanetSystem
    {
        public SpaceObjectCanvasAdapter MainStar { get; set; }
        public List<SpaceObjectCanvasAdapter> Planets { get; }

        public StarPlanetSystem()
        {
            Planets = new List<SpaceObjectCanvasAdapter>();

            MainStar = new SpaceObjectCanvasAdapter(new Star("Солнце", 20, StarTypes.Yellow));
        }

        public SpaceObjectCanvasAdapter FindSpaceObject(string name)
        {
            if (MainStar.BaseSpaceObject.Name == name)
                return MainStar;
            foreach (var planet in Planets)
                if (planet.BaseSpaceObject.Name == name)
                    return planet;
            return null;
        }
    }
}
