using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsHW5.Interfaces;

namespace WinFormsHW5.Functions
{
    internal class FfunctionPlusNumber : StandartFunction, IFunction
    {
        public FfunctionPlusNumber(double xStart, double xEnd, int dpi) : base(xStart, xEnd, dpi)
        {
        }
        public (double[], double[]) GetPoints(Func<double, double> criterion, double sum)
        {
            int cPoint = Convert.ToInt32(Math.Round((Xend - Xstart) / ((double)1 / Dpi) + 1));
            (double[] pointsX, double[] pointsY) pointsXY = (new double[cPoint], new double[cPoint]);
            minY = maxY = criterion(0) + sum;
            for (int i = 0; i < cPoint; i++)
            {
                pointsXY.pointsX[i] = Xstart + i * ((double)1 / Dpi);
                pointsXY.pointsY[i] = criterion(pointsXY.pointsX[i]) + sum;
                if (pointsXY.pointsY[i] < minY) minY = pointsXY.pointsY[i];
                if (pointsXY.pointsY[i] > maxY) maxY = pointsXY.pointsY[i];
            }
            return pointsXY;
        }
    }
}
