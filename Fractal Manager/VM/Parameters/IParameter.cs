using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fractal_Manager.VM.Parameters
{
    public interface IParameter
    {
        string Name { get; }
        UserControl EditingView { get; }
    }

}
