using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    public class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public double Magnitude { get { return Math.Sqrt(Real * Real + Imaginary * Imaginary); } }

        public Complex(double x, double y)
        {
            this.Real = x;
            this.Imaginary = y;
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }
        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }
        public static Complex operator *(Complex complexA, Complex complexB)
        {
            return new Complex(complexA.Real * complexB.Real - complexA.Imaginary * complexB.Imaginary, complexA.Real * complexB.Imaginary + complexA.Imaginary * complexB.Real);
        }

        public Complex Pow(int exp)
        {
            return Complex.Pow(this, exp);
        }
        public static Complex Pow(Complex c1, int exp)
        {
            Complex result = new Complex(1,0);
            while (exp > 0)
            {
                result *= c1;
                exp--;
            }
            return result;
        }

        public Complex Square()
        {
            return Complex.Square(this);
        }
        public static Complex Square(Complex complex)
        {
            return complex * complex;
        }

        public override string ToString()
        {
            return Real + " + " + Imaginary + "i";
        }

        public override bool Equals(object obj)
        {
            Complex other = (Complex)obj;
            return (Real == other.Real && Imaginary == other.Imaginary);
        }
    }
}
