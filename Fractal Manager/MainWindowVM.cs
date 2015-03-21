using Fractal_Generator;
using Fractal_Manager.M;
using Fractal_Manager.VM;
using MvvM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fractal_Manager
{
    public class MainWindowVM : ObservableObject
    {
        public MainWindow MainWindow { get; set; }

        public ObservableCollection<JobVM> QueuedJobs { get; set; }
        public ObservableCollection<JobVM> RunningJobs { get; set; }
        public ObservableCollection<JobVM> FinishedJobs { get; set; }

        public JobFactory JobFactory { get; set; }

        public int MaxNumberOfParallelJobs { get; set; }



        public MainWindowVM(MainWindow mainWindow)
        {
            MainWindow = mainWindow;

            QueuedJobs = new ObservableCollection<JobVM>();
            RunningJobs = new ObservableCollection<JobVM>();
            FinishedJobs = new ObservableCollection<JobVM>();
            JobFactory = new JobFactory();

            MaxNumberOfParallelJobs = 3;

            JobFactory.PropertyChanged += ((o, e) => {
                if (e.PropertyName == "CreateJob")
                {
                    JobFactory.CreateJob = false;
                    QueuedJobs.Add(JobFactory.Job);

                    Job job = JobFactory.Job.Model.Clone();
                    JobVM jobVM = JobFactory.JobType.GetJobVM();
                    jobVM.Model = job;
                    JobFactory.Job = jobVM;

                    RunJobFromQueue();
                }
            });
        }

        public void RunJobFromQueue()
        {
            if (QueuedJobs.Count == 0) return;
            if (RunningJobs.Count > MaxNumberOfParallelJobs) return;

            JobVM nextJob = QueuedJobs[0];
            RunningJobs.Add(nextJob);
            QueuedJobs.Remove(nextJob);
            ThreadPool.QueueUserWorkItem(RunJobInThread, nextJob);
        }

        public void RunJobInThread(object obj)
        {
            JobVM job = (JobVM)obj;
            job.Run();
            MainWindow.Dispatcher.BeginInvoke(new WaitCallback(FinishedRunningJobInThread), obj);
        }
        
        public void FinishedRunningJobInThread(object obj)
        {
            JobVM job = (JobVM)obj;
            job.Progress = 1;
            FinishedJobs.Add(job);
            RunningJobs.Remove(job);
            RunJobFromQueue();
        }
    }
}
