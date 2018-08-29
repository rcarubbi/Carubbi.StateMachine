using System.Diagnostics;

namespace Carubbi.StateMachine.UnitTests
{
    [InitialState("State1")]
    public class Entity : IStatedEntity
    {

        [Transition(From = "State1", To = "State2")]
        [Transition(From = "State3", To = "State1")]
        public void Method1()
        {
            Trace.WriteLine("Method1");
        }

        [Transition(From = "State2", To = "State1")]
        public string Method2()
        {
            Trace.WriteLine("Method2");
            return string.Empty;
        }

        [Transition(From = "State2", To = "State3")]
        [Transition(From = "State3", To = "State4")]
        public int Method3(int p1, int p2)
        {
            Trace.WriteLine("Method3");
            return p1 + p2;
        }


        public StateMachine StateMachine { get; set; }
    }

     
}
