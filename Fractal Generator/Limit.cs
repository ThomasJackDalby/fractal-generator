using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    public class Limit
    {
        public double Min { get; private set; }
        public double Max { get; private set; }
        public double Delta { get; private set; }
        public double Range { get; set; }
        public int Steps { get; private set; }

        public void SetMinRangeSteps(double min, double range, int steps)
        {
            // Set
            Min = min;
            Range = range;
            Steps = steps;

            // Calculate
            Max = Min + Range;
            Delta = Range / Steps;
        }

        public void SetMinDeltaSteps(double min, double delta, int steps)
        {
            // Set
            Min = min;
            Delta = delta;
            Steps = steps;

            // Calculated
            Range = Delta * Steps;
            Max = Min + Range;
        }

        public void SetMinMaxSteps(double min, double max, int steps)
        {
            // Set
            Min = min;
            Max = max;
            Steps = steps;

            // Calculate
            Range = Max - Min;
            Delta = Range / Steps;
        }
    }
}
