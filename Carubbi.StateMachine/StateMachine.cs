using NConcern;
using System;

namespace Carubbi.StateMachine
{
    public class StateMachine
    {
        internal bool Transition(string from, string to)
        {
            var transitionStartedEventArgs = new TransitionStartedEventArgs { From = from, To = to };
            TransitionStarted?.Invoke(this, transitionStartedEventArgs);
            if (transitionStartedEventArgs.Cancel) return false;

            CurrentState = to;

            var transitionEndedEventArgs = new TransitionEventArgs { From = from, To = to };
            TransitionEnded?.Invoke(this, transitionEndedEventArgs);

            return true;
        }

        public event EventHandler<TransitionStartedEventArgs> TransitionStarted;
        public event EventHandler<TransitionEventArgs> TransitionEnded;

        public bool IgnoreInvalidOperations { get; set; }

        public static void Configure()
        {
            Aspect.Weave<StateMachineAspect>(typeof(InitialStateAttribute));
        }

        public string CurrentState { get; internal set; }
    }
}
