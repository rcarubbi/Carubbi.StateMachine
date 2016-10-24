using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Configuration;
using System.Configuration;


namespace Carubbi.StateMachine
{
    public class MachineFactory<T>
    {

        private volatile static MachineFactory<T> _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static MachineFactory<T> GetInstance()
        {
            if (_instance == null)
            {
                lock (typeof(MachineFactory<T>))
                {
                    if (_instance == null)
                    {
                        _instance = new MachineFactory<T>();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Cria DBManager de acordo com o tipo de banco de dados
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public IState<T> GetState(string stateName)
        {
            IState<T> state = new State<T>(stateName);
            return state;
        }

        public IMachine<T> GetMachine(T businessObject, Type machineType)
        {
            ConstructorInfo ctr = machineType.GetConstructor(new Type[] { typeof(T) });
            return (IMachine<T>)ctr.Invoke(new object[] { businessObject });
        }

        public IMachine<T> GetMachine(T businessObject, string machineName)
        {
            string className = ConfigurationManager.AppSettings[machineName];
            Type machineType = Type.GetType(className);
            return GetMachine(businessObject, machineType);
        }

        internal IStateAction<T> GetStateAction(MethodInfo mi, string internalName, string literalName)
        {
            IStateAction<T> action = new StateAction<T>(mi, internalName, literalName);
            return action;

        }
    }
}
