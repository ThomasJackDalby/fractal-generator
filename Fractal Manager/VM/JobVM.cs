using Fractal_Manager.M;
using Fractal_Manager.VM.Parameters;
using MvvM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fractal_Manager.VM
{
    public abstract class JobVM : ObservableObject
    {
        public abstract JobType JobType { get; }

        private Job model;
        public Job Model 
        {
            get { return model; }
            set
            {
                model = value;
                model.PropertyChanged += ((o, e) => OnPropertyChanged("Progress"));
            }
        }

        public int ID 
        {
            get { return Model.ID; }
            set
            {
                Model.ID = value;
                OnPropertyChanged("ID");
            }
                
        }
        public List<IParameter> Parameters { get; set; }
        public double Progress 
        {
            get { return Model.Progress; }
            set
            {
                Model.Progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public abstract string Tooltip { get;  }

        public JobVM(Job model)
        {
            Model = model;

            Parameters = new List<IParameter>();
        }

        public void Run()
        {
            Model.Run();
        }
    }
}
