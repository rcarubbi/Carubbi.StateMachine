using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carubbi.StateMachine
{
    [global::System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class StateActionAttribute : Attribute
    {

        public StateActionAttribute()
        {
            

        }

        public string StateName
        {
            get;
            set;
        }

        public string LiteralName
        {
            get;
            set;
        }

       
    }
   
}
