using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    class Program
    {
        List<FractalResult> ResultsList { get; set; }

        // Fractal Variables
        public string Name { get; set; }
        public FractalType Type { get; set; }
        public int Power { get; set; }
        public Complex C0 { get; set; }

        // Limit Variables
        public Limit XLimit { get; set; }
        public Limit YLimit { get; set; }
        public int IterationLimit { get; set; }
        public double MagnitudeLimit { get; set; }

        // Results Variables
        public double ProgressInterval { get; set; }
        public int ImageThreshold { get; set; }

        public Program()
        {
            XLimit = new Limit();
            YLimit = new Limit();
            ResultsList = new List<FractalResult>();

            ProgressInterval = 0.05;
            ImageThreshold = 100;

            Console.WriteLine("---Welcome to Fractal Generator v1.0---");

            // Set up the fractals here
            Name = "Julia";
            XLimit.SetMinMaxSteps(-1.5, 1.5, 5000);
            YLimit.SetMinMaxSteps(-1.5, 1.5, 5000);
            IterationLimit = 40;
            MagnitudeLimit = 2;

            Type = FractalType.Julia;
            Power = 2;

            string analysisName = "negative";
            Directory.CreateDirectory(analysisName);
            
            //Limit realLimit = new Limit();
            Limit imaginaryLimit = new Limit();
            //realLimit.SetMinMaxSteps(-2, 2, 20);
            imaginaryLimit.SetMinMaxSteps(-2, 2, 20);

            //for (double real = realLimit.Min; real < realLimit.Max; real += realLimit.Delta)
            //{
                //for (double imag = imaginaryLimit.Min; imag < imaginaryLimit.Max; imag += imaginaryLimit.Delta)
                //{
                    double real = 0;
                    double imag = imaginaryLimit.Min;

                    //Power = (int)real;
                    C0 = new Complex(real, imag);

                    Name = "Julia " + C0.ToString();
                    // Running
                    FractalResult result = RunFractal();

                    // Process the results here
                    result.WriteToImageGradient(analysisName + @"/" + result.Filename + ".jpg", IterationLimit);
                    //OpenFile(@"range/" + result.Filename + ".jpg");
               // }
            //}
        }

        public void OpenFile(string filename)
        {
            System.Diagnostics.Process.Start(filename);
        }

        [Obsolete]
        public void ReadInputFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            string[] data = lines[0].Split(',');
            int i = 0;

            Name = data[i++];
            XLimit = new Limit();
            YLimit = new Limit();
            XLimit.SetMinMaxSteps(double.Parse(data[i++]), double.Parse(data[i++]), int.Parse(data[i++]));
            YLimit.SetMinMaxSteps(double.Parse(data[i++]), double.Parse(data[i++]), int.Parse(data[i++]));
            IterationLimit = int.Parse(data[i++]);
            MagnitudeLimit = double.Parse(data[i++]);

            Type = (FractalType)Enum.Parse(typeof(FractalType),data[i++]);
            Power = int.Parse(data[i++]);
            if (Type == FractalType.Julia) C0 = new Complex(double.Parse(data[i++]), double.Parse(data[i++]));
        }

        public FractalResult RunFractal()
        {
            FractalCalculator calculator = new FractalCalculator();
            calculator.ProgressInterval = ProgressInterval;

            Fractal fractal = new Fractal() { XLimit = XLimit, YLimit = YLimit, Name = Name };       
            fractal.FractalExpression = (Complex z, Complex c) => z.Pow(Power) + c;
            fractal.LimitExpression = (Complex x) => x.Magnitude > MagnitudeLimit;
            fractal.IterationLimit = IterationLimit;

            if (Type == FractalType.Julia) fractal.C0 = C0;

            Console.WriteLine("---Running fractal---");
            FractalResult result = calculator.Calculate(fractal);
            ResultsList.Add(result);
            Console.WriteLine("---Finished---");
            return result;
        }

        public void ListResults()
        {
            foreach(FractalResult result in ResultsList)
            {
                Console.WriteLine(String.Format("{0}) {1}",result.ID, result.Filename));
            }
        }

        static double MyPow(double num, int exp)
        {
            double result = 1.0;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                    result *= num;
                exp >>= 1;
                num *= num;
            }
            return result;
        }

        static void Main(string[] args)
        {
            Program program = new Program();
        }
    }
}
