using System;

namespace Carubbi.StateMachine
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class InitialStateAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public InitialStateAttribute(string stateName)
        {
            InitialState = stateName;
        }

        public string InitialState { get; }
    }
}
