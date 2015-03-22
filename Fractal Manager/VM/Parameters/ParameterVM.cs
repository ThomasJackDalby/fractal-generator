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
    public class Parameter<T> : ObservableObject, IParameter
    {
        public string Name { get; set; }

        private T value;
        public T Value 
        {
            get { return Get(); }
            set
            {
                Set(value);
                OnPropertyChanged("Value");
            }
        }

        public delegate T Getter();
        public delegate void Setter(T value);

        public Getter Get { get; set; }
        public Setter Set { get; set; }

        public UserControl EditingView { get; set; }

        public Parameter()
        {
            Get = (() => { return value; });
            Set = ((o) => { value = o; });
        }

        public Parameter(Getter get, Setter set)
        {
            Get = get;
            Set = set;
            EditingView = CreateEditingControl();
        }

        public UserControl CreateEditingControl()
        {
            if (typeof(T) == typeof(double) || typeof(T) == typeof(int)) return CreateNumberBox();
            if (typeof(T) == typeof(string)) return CreateNumberBox();
            if (typeof(T) == typeof(bool)) return CreateCheckBox();
            return null;
        }

        public UserControl CreateNumberBox()
        {
            UserControl control = new UserControl();
            TextBox textBox = new TextBox();
            textBox.DataContext = this;
            textBox.SetBinding(TextBox.TextProperty, "Value");
            control.Content = textBox;
            return control;
        }

        public UserControl CreateCheckBox()
        {
            UserControl control = new UserControl();
            CheckBox checkBox = new CheckBox();
            checkBox.SetBinding(CheckBox.IsCheckedProperty, "Value");
            control.Content = checkBox;
            return control;
        }
    }
}
