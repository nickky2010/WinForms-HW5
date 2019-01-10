using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsHW5.Interfaces;

namespace WinFormsHW5.Functions
{
    internal class FpowerFunction : StandartFunction, IFunction
    {
        public FpowerFunction(double xStart, double xEnd, int dpi) : base(xStart, xEnd, dpi)
        {
        }
        public (double[], double[]) GetPoints(Func<double, double> criterion, double pow)
        {
            int cPoint = Convert.ToInt32(Math.Round(((Xend - Xstart) / ((double)1/Dpi)) + 1));
            (double[] pointsX, double[] pointsY) pointsXY = (new double[cPoint], new double[cPoint]);
            minY = maxY = Math.Pow(criterion(0), pow);
            for (int i = 0; i < cPoint; i++)
            {
                pointsXY.pointsX[i] = Xstart + i * (1 / (double)Dpi);
                pointsXY.pointsY[i] = Math.Pow(criterion(pointsXY.pointsX[i]), pow);
                if (pointsXY.pointsY[i] < minY) minY = pointsXY.pointsY[i];
                if (pointsXY.pointsY[i] > maxY) maxY = pointsXY.pointsY[i];
            }
            return pointsXY;
        }
    }
}
