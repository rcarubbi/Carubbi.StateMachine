using System.Reflection;

namespace Carubbi.StateMachine
{
    public class StateAction<T> : Dto<IStateAction<T>>, IStateAction<T>
    {
        public StateAction(MethodInfo mi, string signatureName, string literalName)
        {
            LiteralName = literalName;
            ActionInfo = mi;
            SignatureName = signatureName;
        }

        #region IStateAction Members
        public MethodInfo ActionInfo
        {
            get;
            set;
        }

        public string SignatureName
        {
            get;
            set;
        }

        public string LiteralName
        {
            get;
            set;
        }

        public Machine<T> Machine
        {
            get;
            internal set;
        }

        public void Executar()
        {
            this.ActionInfo.Invoke(this.Machine, null);
        }

        #endregion
    }
}
