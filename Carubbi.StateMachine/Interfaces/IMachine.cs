using System.Collections.Generic;

namespace Carubbi.StateMachine
{
    public interface IMachine<T> : IDto<IMachine<T>>
    {
        T BusinessObject { get; }

        void Config();
        IState<T> this[string name] { get; }
        IState<T> CurrentState { get; set; }
        Dictionary<string, object> BusinessContext { get; set; }

        IList<IStateAction<T>> RetrieveActions(IState<T> state);

        /// <summary>
        /// Método responsável por chamar o método do objeto de negócio que persiste o estado corrente na base de dados
        /// </summary>
        void UpdateState();

        /// <summary>
        /// Método responsável por chamar o método/propriedade do objeto de negócio que alimenta o estado na máquina
        /// </summary>
        void RetrieveState();
 
        
    }
}
