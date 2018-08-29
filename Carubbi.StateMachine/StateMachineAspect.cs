using Carubbi.Extensions;
using NConcern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Carubbi.StateMachine
{

    internal class StateMachineAspect : IAspect
    {
        public IEnumerable<IAdvice> Advise(MethodBase method)
        {
            if (method.IsConstructor)
            {
                yield return Advice.Basic.Before(InitializeStateMachineAdvice());
            }
            else
            {
                var attributes = method.GetCustomAttributes<TransitionAttribute>();
                var transitionAttributes = attributes as TransitionAttribute[] ?? attributes.ToArray();

                if (transitionAttributes.Any())
                {
                    yield return Advice.Basic.Around(TransitionAdvice(transitionAttributes));
                }
                else if (method.Name == "set_StateMachine")
                {
                    yield return Advice.Basic.Before(SetStateMachineValidationAdvice());
                }
            }
        }

        private static Action<object, object[]> InitializeStateMachineAdvice()
        {
            return (instance, arguments) =>
            {
                var attribute = instance.GetType().GetCustomAttribute<InitialStateAttribute>();
                var stateMachine = new StateMachine
                {
                    CurrentState = attribute.InitialState
                };

                instance.SetProperty(nameof(StateMachine), stateMachine);
            };
        }

        private static Action<object, object[]> SetStateMachineValidationAdvice()
        {
            return (instance, arguments) =>
            {
                var currentStateMachine = (instance as IStatedEntity)?.StateMachine;
                if (currentStateMachine != null)
                {
                    throw new InvalidOperationException();
                }
            };
        }

        private static Action<object, object[], Action> TransitionAdvice(TransitionAttribute[] transitionAttributes)
        {
            return (instance, arguments, body) =>
            {
                var currentStateMachine = (instance as IStatedEntity)?.StateMachine;
                var attribute = transitionAttributes.SingleOrDefault(a => a.From == currentStateMachine.CurrentState);


                switch (attribute)
                {
                    case null when currentStateMachine.IgnoreInvalidOperations:
                        return;
                    case null:
                        throw new InvalidOperationException();
                    default:
                        if (currentStateMachine.Transition(attribute.From, attribute.To)) body();
                        break;
                }


            };
        }
    }
}
