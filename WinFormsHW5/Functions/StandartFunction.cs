using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsHW5.Interfaces;

namespace WinFormsHW5
{
    class StandartFunction : IFunction
    {
        public double Xstart { get; set; }
        public double Xend { get; set; }
        public int Dpi { get; set; }
        protected double minY;
        protected double maxY;
        public double MinY { get => minY; }
        public double MaxY { get=> maxY; }
        public StandartFunction(double xStart, double xEnd, int dpi)
        {
            Xstart = xStart;
            Xend = xEnd;
            Dpi = dpi;
            minY = 0;
            maxY = 0;
        }
        public (double[], double[]) GetPoints(Func<double, double> criterion)
        {
            int cPoint = Convert.ToInt32(Math.Round((Xend - Xstart) / ((double)1 / Dpi) + 1));
            (double[] pointsX, double[] pointsY) pointsXY = (new double[cPoint], new double[cPoint]);
            minY = maxY = criterion(0);
            for (int i = 0; i < cPoint; i++)
            {
                pointsXY.pointsX[i] = Xstart + i * ((double)1 / Dpi);
                pointsXY.pointsY[i] = criterion(pointsXY.pointsX[i]);
                if (pointsXY.pointsY[i] < minY) minY = pointsXY.pointsY[i];
                if (pointsXY.pointsY[i] > maxY) maxY = pointsXY.pointsY[i];
            }
            return pointsXY;
        }

        public void Paint(Graphics gr, Pen p, int size_x, int size_y,
            double minf, double maxf, ref (double[] pointsX, double[] pointsY) pointsXY)
        {
            int de_x_p, de_y_p = 0; //длины единичных отрезков по осям в пикселах
            Point f1_p_1 = new Point(), f1_p_2 = new Point();
            de_x_p = Convert.ToInt32(Math.Round(size_x / (Xend - Xstart)));
            int cPoint = pointsXY.pointsY.Count();
            de_y_p = Convert.ToInt32(Math.Round(size_y / (maxf - minf)));
            f1_p_1.X = 0;
            f1_p_1.Y = size_y - Convert.ToInt32(Math.Round((pointsXY.pointsY[0] - minf) * de_y_p));
            for (int i = 1; i < cPoint; i++)
            {
                f1_p_2.X = Convert.ToInt32((pointsXY.pointsX[i] - pointsXY.pointsX[0]) * de_x_p);
                f1_p_2.Y = size_y - Convert.ToInt32(Math.Round((pointsXY.pointsY[i] - minf) * de_y_p));
                gr.DrawLine(p, f1_p_1, f1_p_2);
                f1_p_1 = f1_p_2;
            }
        }
    }
}
