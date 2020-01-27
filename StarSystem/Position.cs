using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public struct Position
    {
        private int x;
        private int y;

        public int X
        {
            get
            {
                return x;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
        }

        public Position(int x, int y)
        {
            this.x = this.y = 0;
            Set(x, y);
        }

        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
