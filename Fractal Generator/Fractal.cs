using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    public class Fractal
    {
        public string Name { get; set; }

        public Limit XLimit { get; set; }
        public Limit YLimit { get; set; }

        public Func<Complex, Complex, Complex> FractalExpression { get; set; }
        public Func<Complex, bool> LimitExpression { get; set; }
        public Complex C0 { get; set; }

        public int iterationLimit { get; set; }
        public double ProgressInterval { get; set; }
        public double Progress { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime LastTime { get; set; }

        public Fractal()
        {
            XLimit = new Limit();
            YLimit = new Limit();
        }

        public FractalResult RunFractal()
        {
            FractalResult result = new FractalResult(this);
            Progress = 0;
            StartTime = DateTime.Now;
            LastTime = StartTime;
            for (int iX = 0; iX < XLimit.Steps; iX++)
            {
                for (int iY = 0; iY < YLimit.Steps; iY++)
                {
                    double x = XLimit.Min + XLimit.Delta * iX;
                    double y = YLimit.Min + YLimit.Delta * iY;
                    Complex z0 = new Complex(x,y);
                    Complex c0 = z0;
                    if (C0 != null) c0 = C0; 
                    result.Data[iX][iY] = CalculateFractalPoint(z0, c0);
                    DisplayProgress(iX, iY);
                }
            }
            return result;
        }

        public void DisplayProgress(int x, int y)
        {
            double total = XLimit.Steps * YLimit.Steps;
            double progress = ((x * y) + y) / total;
            if (progress >= Progress)
            {
                DateTime nowTime = DateTime.Now;
                TimeSpan deltaTime = nowTime - LastTime;
                LastTime = nowTime;
                TimeSpan fullTime = nowTime - StartTime; 
                Console.WriteLine(String.Format(@"{0} {1:00}:{2:00} {3:00}:{4:00}", Progress * 100 + "%", fullTime.Minutes, fullTime.Seconds, deltaTime.Minutes, deltaTime.Seconds));
                Progress += ProgressInterval;
            }
        }

        public int CalculateFractalPoint(Complex Z0, Complex C0)
        {
            int iteration = 1;
            Complex Zn = Z0;
            Complex C = C0;

            while (iteration < iterationLimit)
            {
                Zn = FractalExpression(Zn, C);
                if (LimitExpression(Zn)) break;
                iteration++;
            }
            return iteration;
        }
    }
}
