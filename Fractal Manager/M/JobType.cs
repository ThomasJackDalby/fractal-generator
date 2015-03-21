using Fractal_Manager.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Manager.M
{
    public enum JobType
    {
        Calculate,
        Render,
    }

    public static class JobTypeExtensions
    {
        public static Job GetJob(this JobType type)
        {
            switch(type)
            {
                case JobType.Calculate:
                    return new CalculateFractalJob();
                case JobType.Render:
                    return new RenderFractalJob();
                default:
                    throw new NotSupportedException();
            }
        }

        public static JobVM GetJobVM(this JobType type)
        {
            switch (type)
            {
                case JobType.Calculate:
                    return new CalculateFractalJobVM();
                case JobType.Render:
                    return new RenderFractalJobVM();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
