using Fractal_Generator;
using MvvM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Manager.M
{
    public abstract class Job : ObservableObject
    {
        public int ID { get; set; }
        public static int NextID { get; set; }

        private double progress;
        public double Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public Job()
        {
            ID = NextID++;
        }

        public abstract void Run();

        public abstract Job Clone();
    }
}
