using MvvM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    public class FractalEngine : ObservableObject
    {
        public double ProgressInterval { get; set; }
        public double Progress { get; set; }

        public int[][] Calculate(Fractal fractal)
        {
            Progress = 0;
            int[][] data = CreateArray(fractal.XLimit.Steps,fractal.YLimit.Steps);

            for (int iX = 0; iX < fractal.XLimit.Steps; iX++)
            {
                for (int iY = 0; iY < fractal.YLimit.Steps; iY++)
                {
                    double x = fractal.XLimit.Min + fractal.XLimit.Delta * iX;
                    double y = fractal.YLimit.Min + fractal.YLimit.Delta * iY;
                    Complex z0 = new Complex(x,y);
                    Complex c0 = z0;
                    if (fractal.C0 != null) c0 = fractal.C0;
                    data[iX][iY] = fractal.CalculatePoint(z0, c0);

                    double total = fractal.XLimit.Steps * fractal.YLimit.Steps;
                    double progress = ((iX * iY) + iY) / total;
                    if (progress >= Progress)
                    {
                        Progress += ProgressInterval;
                        OnPropertyChanged("Progress");
                    }
                }
            }
            Progress = 1;
            return data;
        }

        public Bitmap Render(int[][] data, int factor)
        {
            Progress = 0;
            Bitmap outputBitmap = new Bitmap(data.Length, data[0].Length);
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    int hue = 0;
                    Math.DivRem(data[y][x] * factor, 255, out hue);

                    outputBitmap.SetPixel(x, data.Length - y - 1, ColorFromHSV(hue, 1.0, 1.0));

                    double total = data.Length * data[0].Length;
                    double progress = ((x * y) + y) / total;
                    if (progress >= Progress)
                    {
                        Progress += ProgressInterval;
                        OnPropertyChanged("Progress");
                    }
                }
            }
            Progress = 1;
            return outputBitmap;
        }

        private static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        private static int[][] CreateArray(int xSteps, int ySteps)
        {
            int[][] Data = new int[xSteps][];
            for (int i = 0; i < xSteps; i++)
            {
                Data[i] = new int[ySteps];
            }
            return Data;
        }    
    }
}
