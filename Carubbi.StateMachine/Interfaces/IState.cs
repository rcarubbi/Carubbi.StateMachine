namespace Carubbi.StateMachine
{
    public interface IState<T> : IDto<IState<T>>
    {
        int Id { get; set; }
        string Name { get; set; }
        

        bool IsInitialSatte { get; }

        IStateAction<T> GetActionBySignatureName(string name);


        Machine<T> Machine
        {
            get;
           
        }
    }
}
