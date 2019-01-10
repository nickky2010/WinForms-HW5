using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsHW5.Functions;
using WinFormsHW5.Interfaces;

namespace WinFormsHW5
{
    public partial class Form1 : Form
    {
        double xStart;
        double xEnd;
        int dpi;
        IFunction fSinxPow2;
        IFunction fCosxPlus1;
        bool createSinxPow2 = false;
        bool createCosxPlus1 = false;
        Graphics gSinxPow2;
        Graphics gCosxPlus1;
        Pen pSinxPow2;
        Pen pCosxPlus1;
        public Form1()
        {
            try
            {
                InitializeComponent();
                groupBox1.Controls.Add(label6);
                groupBox1.Controls.Add(label2);
                groupBox1.Controls.Add(label4);
                groupBox1.Controls.Add(textBox1);
                groupBox1.Controls.Add(textBox2);
                groupBox1.Controls.Add(textBox3);
                groupBox1.Font = new Font("Microsoft Sans Serif", 8);
                xStart = Convert.ToDouble(textBox1.Text);
                xEnd = Convert.ToDouble(textBox2.Text);
                dpi = Convert.ToInt32(textBox3.Text);
                gSinxPow2 = panel1.CreateGraphics();
                gCosxPlus1 = panel1.CreateGraphics();
                pSinxPow2 = new Pen(Color.Blue, 1);
                pCosxPlus1 = new Pen(Color.Red, 3);
                float[] xx = { 2, 3, 1, 2 };
                pCosxPlus1.DashPattern = xx;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong! " + e.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
        // SinxPow2
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!createSinxPow2)
            {
                fSinxPow2 = new FpowerFunction(xStart, xEnd, dpi);
                createSinxPow2 = true;
            }
        }
        // CosxPlus1
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!createCosxPlus1)
            {
                fCosxPlus1 = new FfunctionPlusNumber(xStart, xEnd, dpi);
                createCosxPlus1 = true;
            }
        }
        // Create grafic
        private void button1_Click(object sender, EventArgs e)
        {
            Erase();
            double minf = 0;
            double maxf = 0;
            (double[] pX, double[] pY) points1 = (null, null);
            (double[] pX, double[] pY) points2 = (null, null);
            if (checkBox1.Checked && checkBox2.Checked)
            {
                CalculateGraph(0, ref fSinxPow2, ref points1, ref minf, ref maxf);
                CalculateGraph(1, ref fCosxPlus1, ref points2, ref minf, ref maxf);
                fSinxPow2.Paint(gSinxPow2, pSinxPow2, panel1.Width, panel1.Height, minf, maxf, ref points1);
                fCosxPlus1.Paint(gCosxPlus1, pCosxPlus1, panel1.Width, panel1.Height, minf, maxf, ref points2);
            }
            else if (checkBox1.Checked && !checkBox2.Checked)
            {
                CalculateGraph(0, ref fSinxPow2, ref points1, ref minf, ref maxf);
                fSinxPow2.Paint(gSinxPow2, pSinxPow2, panel1.Width, panel1.Height, minf, maxf, ref points1);
            }
            else if (!checkBox1.Checked && checkBox2.Checked)
            {
                CalculateGraph(1, ref fCosxPlus1, ref points2, ref minf, ref maxf);
                fCosxPlus1.Paint(gCosxPlus1, pCosxPlus1, panel1.Width, panel1.Height, minf, maxf, ref points2);
            }
            else
            {
                MessageBox.Show("Please enter a function!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Erase()
        {
            if (gSinxPow2 != null)
            {
                gSinxPow2.Clear(Color.Turquoise);
            }
            if (gCosxPlus1 != null)
            {
                gCosxPlus1.Clear(Color.Turquoise);
            }
        }
        // Erase
        private void button2_Click(object sender, EventArgs e)
        {
            Erase();
        }

        // Xstart
        private void textBox1_Leave(object sender, EventArgs e)
        {
            ChangeTextBox(1);
        }

        // Xend
        private void textBox2_Leave(object sender, EventArgs e)
        {
            ChangeTextBox(2);
        }

        // dpi
        private void textBox3_Leave(object sender, EventArgs e)
        {
            ChangeTextBox(3);
        }

        private void CalculateGraph(int graficNumber, ref IFunction function, 
            ref (double[] pX, double[] pY) points, ref double minf, ref double maxf)
        {
            function.Xstart = xStart;
            function.Xend = xEnd;
            function.Dpi = dpi;
            switch (graficNumber)
            {
                case 0:
                    {
                        points = ((FpowerFunction)function).GetPoints(Math.Sin, 2.0);
                        break;
                    }
                case 1:
                    {
                        points = ((FfunctionPlusNumber)function).GetPoints(Math.Cos, 1.0);
                        break;
                    }
            }
            if (function.MinY < minf) { minf = function.MinY; }
            if (function.MaxY > maxf) { maxf = function.MaxY; }
        }
        private void ChangeTextBox(int numberTextBox, int dpiMin = 5)
        {
            try
            {
                double start = xStart;
                double end = xEnd;
                int dpi_;
                switch (numberTextBox)
                {
                    case 1:
                        start = Convert.ToDouble(textBox1.Text);
                        if (start < xEnd)
                            xStart = start;
                        else
                            textBox1.Text = xStart.ToString();
                        break;
                    case 2:
                        end = Convert.ToDouble(textBox2.Text);
                        if (start < end)
                            xEnd = end;
                        else
                            textBox2.Text = xEnd.ToString();
                        break;
                    case 3:
                        dpi_ = Convert.ToInt32(textBox3.Text);
                        if (dpi_ > dpiMin && dpi_ > 0)
                            dpi = dpi_;
                        else
                        {
                            MessageBox.Show("dpi should be more " + dpiMin, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox3.Text = dpi.ToString();
                        }
                        break;
                }
                if (start >= end)
                {
                    MessageBox.Show("Xstart should be less Xend", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Parameter must be a number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = xStart.ToString();
                textBox2.Text = xEnd.ToString();
                textBox3.Text = dpi.ToString();
            }
        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(pSinxPow2, 5, label5.Size.Height / 2,
                label5.Size.Width - 1, label5.Size.Height / 2);
        }

        private void label7_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine (pCosxPlus1, 5, label7.Size.Height / 2, 
                label7.Size.Width - 1, label7.Size.Height / 2);
        }
    }
}
