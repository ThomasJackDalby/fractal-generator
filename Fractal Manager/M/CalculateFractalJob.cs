using Fractal_Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Manager.M
{
    public class CalculateFractalJob : Job
    {
        public Fractal Fractal { get; set; }

        public string OutputFilename { get; set; }

        public CalculateFractalJob()
            : base()
        {
            Fractal = new Fractal();
            OutputFilename = @"D:\temp.fractal";
        }

        public override void Run()
        {
            FractalEngine calculator = new FractalEngine() { ProgressInterval = 0.01 };
            calculator.PropertyChanged += ((o, e) =>
            {
                Progress = calculator.Progress;
            });
      
            int[][] result = calculator.Calculate(Fractal);
            Serializer.Save<int[][]>(result, OutputFilename);
        }

        public override Job Clone()
        {
            CalculateFractalJob clone = new CalculateFractalJob();
            clone.Fractal = Fractal.Clone();
            clone.OutputFilename = OutputFilename;
            return clone;
        }
    }
}
