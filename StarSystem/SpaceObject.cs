using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StarSystem
{
    public abstract class SpaceObject
    {
        public string Name { get; set; }
        public double Radius { get; set; }
        public Color ObjectColor { get; set; }
        public Position ObjectPosition { get; set; }

        public SpaceObject(string name, int radius = 1, Position position = new Position(), Color objectColor = new Color())
        {
            Name = name;
            Radius = radius;
            ObjectPosition = position;
            ObjectColor = objectColor;
        }
    }
}
