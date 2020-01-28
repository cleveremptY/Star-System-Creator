using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public struct Position
    {
        private double x;
        private double y;

        public double X
        {
            get
            {
                return x;
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
        }

        public Position(double x, double y)
        {
            this.x = this.y = 0;
            Set(x, y);
        }

        public void Set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return x.ToString() + " " + y.ToString();
        }
    }
}
