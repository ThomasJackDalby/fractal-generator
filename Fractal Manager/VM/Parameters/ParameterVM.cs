using Fractal_Manager.M;
using Fractal_Manager.VM.Parameters;
using MvvM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Parameter()
        {
            Get = (() => { return value; });
            Set = ((o) => { value = o; });
        }

        public Parameter(Getter get, Setter set)
        {
            Get = get;
            Set = set;
        }
    }
}
