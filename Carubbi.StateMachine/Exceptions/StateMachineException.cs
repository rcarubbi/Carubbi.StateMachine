using System;

namespace Carubbi.StateMachine
{
    public class StateMachineException : ApplicationException
    {
        public StateMachineException(string message, Exception innerException)
            : base(message, innerException)
        {
        
        }

    }
}
