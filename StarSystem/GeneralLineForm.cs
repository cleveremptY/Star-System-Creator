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
            A = M2.Y - M1.Y;
            B = M2.X - M2.X;
            C = A * -1 * M1.X - B * -1 * M1.Y;
        }

        public double GetAngle(GeneralLineForm line)
        {
            double angle = (A * line.A + B * line.B) / (Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2)) * Math.Sqrt(Math.Pow(line.A, 2) + Math.Pow(line.B, 2)));
            return FromRadianToDeg(Math.Cos(angle));
            //return angle;
        }

        public static double FromRadianToDeg(double radian)
        {
            //double angleRadian = angle * Math.PI / 180;
            double angle = radian * (double)180 / Math.PI;
            return angle;
        }
    }
}
