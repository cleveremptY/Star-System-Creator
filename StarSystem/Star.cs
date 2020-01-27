using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    class Star: SpaceObject
    {
        public StarTypes Type { get; set; }

        public Star(string name, int radius, StarTypes type)
        : base(name, radius)
        {
            Type = type;
        }
    }
}
