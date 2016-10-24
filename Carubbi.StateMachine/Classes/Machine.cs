using System;
using System.Collections.Generic;
using System.Linq;

namespace Carubbi.StateMachine
{
    public abstract class Machine<T> : ListBase<IState<T>, IMachine<T>>, IMachine<T>
    {
  
        public Machine(T businessObject)
        {
            this.BusinessObject = businessObject;
            Config();
            RetrieveState();           
        }
  
        public abstract void Config();

        public virtual IList<IStateAction<T>> RetrieveActions(IState<T> state)
        {
            return ((State<T>)state).Actions;
        }

        public IState<T> this[string name]
        {
            get
            {
                try
                {
                    var result = from state in _lista where state.Name == name select state;
                    return result.ToList()[0];
                }
                catch (Exception ex)
                {
                    throw new StateMachineException(string.Format("Erro ao tentar obter o estado {0}",name), ex);
                }
            }
        }
        
        public virtual void UpdateState()
        { }

        public virtual void RetrieveState()
        {}
        
        public T BusinessObject
        {
            get; 
            internal set; 
        }

        #region IMachine<T> Members

        private IState<T> _currentState;
        
        public virtual IState<T> CurrentState
        {
            get {

                if (_currentState == null)
                {
                    _currentState = this.InitialState;
                }
                
                return _currentState;
            }
            set 
            {
                bool isNew = (_currentState == null);
                _currentState = value;

                if (!isNew)
                    UpdateState();
            }
        }

        public override void Add(IState<T> item)
        {
            base.Add(item);
            ((State<T>)item).Machine = this;
            ((State<T>)item).AddActions(this.GetType());
        }

        public void Add(IState<T> item, bool isInitialState)
        {
            ((State<T>)item).IsInitialSatte = isInitialState;
            Add(item);
        }

        protected IState<T> InitialState
        {
            get
            {
                IState<T> initialState = null;
                var result = from state in this where state.IsInitialSatte select state;
                if (result.ToList().Count > 0)
                    initialState = result.ToList()[0];
                else
                    throw new StateMachineException("Nenhum estado foi definido como inicial", null);
                return initialState;
            }
        
        }

        public Dictionary<string, object> BusinessContext
        {
            get;
            set;
        }

        #endregion
    }
}
