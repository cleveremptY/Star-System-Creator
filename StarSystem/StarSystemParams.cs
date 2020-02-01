using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    static class StarSystemParams
    {
        public static Position SystemCenter { get; set; }
        public static Position SpaceObjectViewCenter { get; set; }
        public static SpaceObject SelectedSpaceObject { get; set; }
        public static StarPlanetSystem MainStarSystem { get; set; }
        public static double StarSystemSpeed { get; set; } = 1;
        public static double StarSystemSize { get; set; } = 1;
    }
}
