using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Carubbi.StateMachine
{
    public class State<T> : Dto<IState<T>>, IState<T>
    {
        protected IList<IStateAction<T>> _actions;
        internal State(string name)
        {
            Name = name;
            _actions = new List<IStateAction<T>>();
        }

        #region IState Members

        public Machine<T> Machine
        {
            get;
            internal set;
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        internal IList<IStateAction<T>> Actions
        {
            get 
            {
                return _actions;
            }
            
        }

        public bool IsInitialSatte
        {
            get;
            internal set;
        }

        internal void AddActions(Type machineType)
        {
            MethodInfo[] actionsArray = machineType.GetMethods();
            foreach (MethodInfo m in actionsArray)
            {
                object[] customAttributes = m.GetCustomAttributes(typeof(StateActionAttribute), true);
                foreach (object att in customAttributes)
                {
                    if (((StateActionAttribute)att).StateName == this.Name)
                        this.AddAction(MachineFactory<T>.GetInstance().GetStateAction(m, m.Name, ((StateActionAttribute)att).LiteralName));
                }
            }
        }

        private void AddAction(IStateAction<T> stateAction)
        {
            ((StateAction<T>)stateAction).Machine = this.Machine;
            this.Actions.Add(stateAction);
        }

        public IStateAction<T> GetActionBySignatureName(string name)
        {
            IStateAction<T> actionReturn = null;
            var result = from action in Actions where action.SignatureName == name select action;
            if (result.ToList().Count > 0)
                actionReturn = result.ToList()[0];
            else
                throw new StateMachineException(string.Format("A ação {0} não esta disponível no estado {1}", name, this.Name), null);
            return actionReturn;
        }
  
        #endregion
    }
}
