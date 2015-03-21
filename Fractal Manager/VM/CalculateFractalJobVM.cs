using Fractal_Manager.M;
using Fractal_Manager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Manager.VM
{
    public class CalculateFractalJobVM : JobVM
    {
        public CalculateFractalJob CastModel 
        {
            get { return (CalculateFractalJob)Model; }
            set { Model = value; }
        }

        public Parameter<double> Real { get; set; }
        public Parameter<double> Imaginary { get; set; }
        public Parameter<int> Power { get; set; }

        public Parameter<double> XMin { get; set; }
        public Parameter<double> XMax { get; set; }
        public Parameter<int> XSteps { get; set; }

        public Parameter<double> YMin { get; set; }
        public Parameter<double> YMax { get; set; }
        public Parameter<int> YSteps { get; set; }

        public Parameter<int> IterationLimit { get; set; }
        public Parameter<string> OutputFilename { get; set; }





        public override string Tooltip { get { return String.Format("{0} {1} {2}", CastModel.Fractal.IterationLimit, CastModel.Fractal.C0.Real, CastModel.Fractal.C0.Imaginary); } }

        public CalculateFractalJobVM()
            :this(new CalculateFractalJob())
        { }

        public CalculateFractalJobVM(CalculateFractalJob model)
            :base(model)
        {
            Model = model;

            Real = new Parameter<double>((() => CastModel.Fractal.C0.Real), (o => CastModel.Fractal.C0.Real = o)) { Name = "Real" };
            Imaginary = new Parameter<double>((() => CastModel.Fractal.C0.Imaginary), (o => CastModel.Fractal.C0.Imaginary = o)) { Name = "Imaginary" };
            Power = new Parameter<int>((() => CastModel.Fractal.Power), (o => CastModel.Fractal.Power = o)) { Name = "Power" };
            IterationLimit = new Parameter<int>((() => CastModel.Fractal.IterationLimit), (o => CastModel.Fractal.IterationLimit = o)) { Name = "IterationLimit" };
          
            OutputFilename = new Parameter<string>((() => CastModel.OutputFilename), (o => CastModel.OutputFilename = o)) { Name = "Output Filename" };

            XMin = new Parameter<double>((() => CastModel.Fractal.XLimit.Min), (o => { CastModel.Fractal.XLimit.Min = o; CastModel.Fractal.XLimit.Calculate(); })) { Name = "X Min" };
            XMax = new Parameter<double>((() => CastModel.Fractal.XLimit.Max), (o => { CastModel.Fractal.XLimit.Max = o; CastModel.Fractal.XLimit.Calculate(); })) { Name = "X Max" };
            XSteps = new Parameter<int>((() => CastModel.Fractal.XLimit.Steps), (o => { CastModel.Fractal.XLimit.Steps = o; CastModel.Fractal.XLimit.Calculate(); })) { Name = "X Steps" };

            YMin = new Parameter<double>((() => CastModel.Fractal.YLimit.Min), (o => { CastModel.Fractal.YLimit.Min = o; CastModel.Fractal.YLimit.Calculate(); })) { Name = "Y Min" };
            YMax = new Parameter<double>((() => CastModel.Fractal.YLimit.Max), (o => { CastModel.Fractal.YLimit.Max = o; CastModel.Fractal.YLimit.Calculate(); })) { Name = "Y Max" };
            YSteps = new Parameter<int>((() => CastModel.Fractal.YLimit.Steps), (o => { CastModel.Fractal.YLimit.Steps = o; CastModel.Fractal.YLimit.Calculate(); })) { Name = "Y Steps" };


            Parameters.Add(Real);
            Parameters.Add(Imaginary);
            Parameters.Add(Power);
            Parameters.Add(OutputFilename);

            Parameters.Add(XMin);
            Parameters.Add(XMax);
            Parameters.Add(XSteps);

            Parameters.Add(YMin);
            Parameters.Add(YMax);
            Parameters.Add(YSteps);

            Parameters.Add(IterationLimit);
        }

    }
}
