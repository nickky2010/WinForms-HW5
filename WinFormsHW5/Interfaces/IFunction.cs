using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsHW5.Interfaces
{
    public interface IFunction
    {
        double Xstart { get; set; }
        double Xend { get; set; }
        int Dpi { get; set; }
        double MinY { get; }
        double MaxY { get; }
        (double[], double[]) GetPoints(Func<double, double> criterion);
        void Paint(Graphics gr, Pen p, int size_x, int size_y,
            double minf, double maxf, ref (double[] pointsX, double[] pointsY) pointsXY);
    }
}
