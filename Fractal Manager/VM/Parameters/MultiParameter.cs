using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fractal_Manager.VM.Parameters
{
    public class MultiParameter<T> : IParameter
    {
        public List<IParameter> ParameterList { get; set; }

        public string Name { get; set; }
        public UserControl EditingView { get; set; }

        public MultiParameter(params IParameter[] parameters)
        {
            ParameterList = new List<IParameter>();
            foreach (IParameter param in parameters) ParameterList.Add(param);
            EditingView = CreateEditingControl(typeof(T));
        }

        public UserControl CreateEditingControl(Type type)
        {
            UserControl control = new UserControl();
            Grid grid = new Grid();
            foreach (IParameter param in ParameterList)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                UserControl subControl = param.EditingView;
                subControl.SetValue(Grid.ColumnProperty, ParameterList.IndexOf(param));
                grid.Children.Add(subControl);
            }
            control.Content = grid;
            return control;
        }
    }
}
