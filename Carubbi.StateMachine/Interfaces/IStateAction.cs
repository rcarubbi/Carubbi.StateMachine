using System;
using System.Reflection;

namespace Carubbi.StateMachine
{
    public interface IStateAction<T> :IDto<IStateAction<T>>
    {
        Machine<T> Machine { get; }
        MethodInfo ActionInfo {get; set;}
        String LiteralName {get; set;}
        string SignatureName { get; set; }
        void Executar();
    }
}
