using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StarSystem
{
    class Star: SpaceObject
    {
        public StarTypes Type { get; set; }

        public Star(string name, int radius = 20, StarTypes type = StarTypes.Yellow)
        : base(name, radius)
        {
            Type = type;
            switch (type)
            {
                case StarTypes.Blue:
                    base.ObjectColor = Color.FromRgb(0, 0, 255);
                    break;
                case StarTypes.Red:
                    base.ObjectColor = Color.FromRgb(255, 0, 0);
                    break;
                case StarTypes.Yellow:
                    base.ObjectColor = Color.FromRgb(255, 255, 0);
                    break;
                default:
                    break;
            }
        }
    }
}
