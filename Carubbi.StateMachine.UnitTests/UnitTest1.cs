using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Carubbi.StateMachine.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            StateMachine.Configure();
            var ent = new Entity
            {
                StateMachine =
                {
                    IgnoreInvalidOperations = true
                }
            };

            ent.StateMachine.TransitionStarted += StateMachine_TransitionStarted;
            ent.StateMachine.TransitionEnded += StateMachine_TransitionEnded;

            Trace.WriteLine(ent.StateMachine.CurrentState);
            ent.Method1();
            Trace.WriteLine(ent.StateMachine.CurrentState);
            ent.Method2();
            Trace.WriteLine(ent.StateMachine.CurrentState);
            ent.Method1();
            Trace.WriteLine(ent.StateMachine.CurrentState);
            var result = ent.Method3(2, 4);
            Trace.WriteLine(ent.StateMachine.CurrentState);
            var result2 = ent.Method3(4, 4);
            Trace.WriteLine(ent.StateMachine.CurrentState);
        }

        private void StateMachine_TransitionEnded(object sender, TransitionEventArgs e)
        {

        }

       
        private void StateMachine_TransitionStarted(object sender, TransitionStartedEventArgs e)
        {
            
        }
    }
}
