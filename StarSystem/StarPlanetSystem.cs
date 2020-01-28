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
            MainStar = new SpaceObjectCanvasAdapter(new Star("Солнце", 20, StarTypes.Yellow));
            MainStar.Draw();
        }

        public void AddPlanet()
        {

        }
    }
}
