# Carubbi.StateMachine
Generic State Machine Aspect-oriented-programming based Implementation 

Just create your entity like this:

```csharp 
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
   ```   

  and use it this way:
    
```csharp  

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
            var result3 = ent.Method3(4, 4);
            Trace.WriteLine(ent.StateMachine.CurrentState);
        }

        private void StateMachine_TransitionEnded(object sender, TransitionEventArgs e)
        {

        }

       
        private void StateMachine_TransitionStarted(object sender, TransitionStartedEventArgs e)
        {
            
        }
   }
   ```
   
    
