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

        public Position SystemCenter;

        public StarPlanetSystem(Position SystemCenter)
        {
            MainStar = new SpaceObjectCanvasAdapter(new Star("Солнце", 20, StarTypes.Yellow), SystemCenter);
            MainStar.Draw();
        }

        public void AddPlanet()
        {

        }
    }
}
