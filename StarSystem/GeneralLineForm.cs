using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    //Общее уравнение прямой
    class GeneralLineForm
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public GeneralLineForm(double a, double b,  double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public GeneralLineForm(Position M1, Position M2)
        {
            this.A = M2.Y - M1.Y;
            this.B = M2.X - M1.X;
            this.C = A * -1 * M1.X - B * M1.Y;
        }

        public double GetAngle(GeneralLineForm line)
        {
            double angle = (A * line.A + B * line.B) / (Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2)) * Math.Sqrt(Math.Pow(line.A, 2) + Math.Pow(line.B, 2)));
            return Math.Acos(angle) / Math.PI * 180; 
        }

        public static double FromRadianToDeg(double radian)
        {
            double angle = radian * 180 / Math.PI;
            return angle;
        }

        public override string ToString()
        {
            return "(" + A.ToString() + "x) + (" + B.ToString() + "y) + (" + C.ToString() + ")";
        }
    }
}
