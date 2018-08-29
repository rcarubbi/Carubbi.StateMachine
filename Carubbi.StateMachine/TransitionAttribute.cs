using System;

namespace Carubbi.StateMachine
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class TransitionAttribute : Attribute
    {
        public string From { get; set; }

        public string To { get; set; }

    }
}
