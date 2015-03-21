using Fractal_Generator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Manager.M
{
    public class RenderFractalJob : Job
    {
        public Fractal Fractal { get; set; }
        public string InputFilename { get; set; }
        public string OutputFilename { get; set; }
        public int Factor { get; set; }

        public RenderFractalJob()
            : base()
        {
            Fractal = new Fractal();
            InputFilename = @"D:\temp.fractal";
            OutputFilename = @"D:\temp.jpg";
        }

        public override void Run()
        {
            FractalEngine calculator = new FractalEngine() { ProgressInterval = 0.01 };
            calculator.PropertyChanged += ((o, e) =>
            {
                Progress = calculator.Progress;
            });

            int[][] data = Serializer.Load<int[][]>(InputFilename);
            Bitmap image = calculator.Render(data, Factor);
            image.Save(OutputFilename);
        }

        public override Job Clone()
        {
            RenderFractalJob clone = new RenderFractalJob();
            clone.Fractal = Fractal.Clone();
            clone.OutputFilename = OutputFilename;
            clone.InputFilename = InputFilename;
            clone.Factor = Factor;
            return clone;
        }
    }
}
