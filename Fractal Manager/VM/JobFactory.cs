using Fractal_Manager.M;
using Fractal_Manager.View;
using MvvM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fractal_Manager.VM
{
    public class JobFactory : ObservableObject
    {
        public UserControl View { get; set; }

        private JobType jobType;
        public JobType JobType 
        {
            get { return jobType; }
            set
            {
                jobType = value;
                Job = JobList.Single(x => x.JobType == value);
                OnPropertyChanged("Job");
                OnPropertyChanged("JobType");
            }
        }
        public JobVM Job { get; set; }
        public List<JobVM> JobList { get; set; }

        public bool CreateJob { get; set; }

        public ICommand CreateNewJobCommand { get; set; }

        public JobFactory()
        {
            View = new JobFactoryView(this);

            JobList = new List<JobVM>();
            foreach (JobType jobType in Enum.GetValues(typeof(JobType))) JobList.Add(jobType.GetJobVM());

            CreateNewJobCommand = new RelayCommand(new Action<object>(CreateNewJob));
        }

        public void CreateNewJob(object sender)
        {
            CreateJob = true;
            OnPropertyChanged("CreateJob");
        }
    }
}
