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

        public Complex C0 { get; set; }
        public Limit XLimit { get; set; }
        public Limit YLimit { get; set; }
        public int IterationLimit { get; set; }

        public Func<Complex, Complex, Complex> FractalExpression { get; set; }
        public Func<Complex, bool> LimitExpression { get; set; }

        public string Filename { get { return String.Format("{0} ({1},{2}) ({3},{4}) {5}x{6}", Name, XLimit.Min, YLimit.Min, XLimit.Max, YLimit.Max, XLimit.Steps, YLimit.Steps); } }

        public Fractal()
        {
            XLimit = new Limit();
            YLimit = new Limit();
        }

        public int CalculatePoint(Complex Z0, Complex C0)
        {
            int iteration = 1;
            Complex Zn = Z0;
            Complex C = C0;

            while (iteration < IterationLimit)
            {
                Zn = FractalExpression(Zn, C);
                if (LimitExpression(Zn)) break;
                iteration++;
            }
            return iteration;
        }
    }
}
