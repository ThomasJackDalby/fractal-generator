using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    [Serializable]
    public class Fractal
    {
        public string Name { get; set; } //TODO generate this dynamically

        public Complex C0 { get; set; }
        public Limit XLimit { get; set; }
        public Limit YLimit { get; set; }
        public int IterationLimit { get; set; }
        public double MagnitudeLimit { get; set; }
        public int Power { get; set; }

        public Func<Complex, Complex, Complex> FractalExpression { get { return (Complex z, Complex c) => z.Pow(Power) + c; } }
        public Func<Complex, bool> LimitExpression { get { return (Complex x) => x.Magnitude > MagnitudeLimit; } }

        public string Filename { get { return String.Format("{0} ({1},{2}) ({3},{4}) {5}x{6}", Name, XLimit.Min, YLimit.Min, XLimit.Max, YLimit.Max, XLimit.Steps, YLimit.Steps); } }

        public Fractal()
        {
            C0 = new Complex(0,0);
            XLimit = new Limit();
            YLimit = new Limit();
            Power = 3;
            IterationLimit = 40;
            MagnitudeLimit = 2;
            XLimit.SetMinMaxSteps(-1.5, 1.5, 500);
            YLimit.SetMinMaxSteps(-1.5, 1.5, 500);
        }

        public Fractal Clone()
        {
            Fractal clone = new Fractal();
            clone.C0 = C0.Clone();
            clone.XLimit = XLimit.Clone();
            clone.XLimit = XLimit.Clone();
            clone.IterationLimit = IterationLimit;
            clone.MagnitudeLimit = MagnitudeLimit;
            clone.Power = Power;
            return clone;
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
