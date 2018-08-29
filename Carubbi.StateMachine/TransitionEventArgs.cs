using System;

namespace Carubbi.StateMachine
{
    public class TransitionEventArgs : EventArgs
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}
