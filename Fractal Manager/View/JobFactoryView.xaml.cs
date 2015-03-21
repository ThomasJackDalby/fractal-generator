using Fractal_Manager.M;
using Fractal_Manager.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fractal_Manager.View
{
    /// <summary>
    /// Interaction logic for JobFactoryView.xaml
    /// </summary>
    public partial class JobFactoryView : UserControl
    {
        public JobFactoryView(JobFactory dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            JobTypesComboBox.ItemsSource = Enum.GetValues(typeof(JobType));
        }
    }
}
