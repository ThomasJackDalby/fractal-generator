using Fractal_Manager.M;
using Fractal_Manager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Manager.VM
{
    public class RenderFractalJobVM : JobVM
    {
        public RenderFractalJob CastModel 
        {
            get { return (RenderFractalJob)Model; }
            set { Model = value; }
        }

        public Parameter<int> Limit { get; set; }
        public Parameter<string> OutputFilename { get; set; }
        public Parameter<string> InputFilename { get; set; }

        public override string Tooltip { get { return String.Format("{0} {1} {2}", CastModel.Fractal.IterationLimit, CastModel.Fractal.C0.Real, CastModel.Fractal.C0.Imaginary); } }

        public RenderFractalJobVM()
            : this(new RenderFractalJob())
        { }

        public RenderFractalJobVM(RenderFractalJob model)
            :base(model)
        {
            Model = model;

            Limit = new Parameter<int>((() => CastModel.Factor), (o => CastModel.Factor = o)) { Name = "Factor" };
            InputFilename = new Parameter<string>((() => CastModel.InputFilename), (o => CastModel.InputFilename = o)) { Name = "Input Filename" };
            OutputFilename = new Parameter<string>((() => CastModel.OutputFilename), (o => CastModel.OutputFilename = o)) { Name = "Output Filename" };

            Parameters.Add(Limit);
            Parameters.Add(InputFilename);
            Parameters.Add(OutputFilename);
        }
    }
}
