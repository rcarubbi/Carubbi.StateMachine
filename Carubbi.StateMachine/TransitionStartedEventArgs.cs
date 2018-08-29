namespace Carubbi.StateMachine
{
    public class TransitionStartedEventArgs : TransitionEventArgs
    {
        public bool Cancel { get; set; }
    }
}
