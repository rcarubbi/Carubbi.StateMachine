using System;
using System.Data;

namespace Carubbi.StateMachine
{
    /// <summary>
    /// Classe auxiliar de comunicação com os dados (Procedure ou Adapter)
    /// </summary>
    /// <typeparam name="T">Classe que será manipulada pelos métodos (Classe do contexto)</typeparam>
    [Serializable]
    public abstract class Dto<T> : IDto<T>
    {
        public Dto()
        {
            
        }

        /// <summary>
        /// Responsável por popular os atributos simples do objeto a partir de uma instrução SQL executada pelo IDBManager
        /// </summary>
        /// <param name="dr">Objeto DataReader que contem os dados do objeto retornados pela instrução SQL</param>
        protected virtual void ConvertToThisObject(IDataReader dr)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Responsável por popular os atributos simples do objeto a partir de um método de uma classe adapter da estrutura antiga
        /// </summary>
        /// <param name="dr">Objeto DataRow do DataTable retornado pelo método da classe adapter</param>
        protected virtual void ConvertToThisObject(DataRow dr)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Retorna um datareader a partir da chave do objeto
        /// </summary>
        /// <param name="chave">Instancia do objeto para poder acessar a chave</param>
        /// <returns>Instancia de um data reader com o objeto obtido</returns>
        protected virtual IDataReader GetByKey(T chave)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Responsável por carregar as agregações do objeto
        /// </summary>
        protected virtual void LoadAgregations()
        {
            throw new NotSupportedException();
        }

        #region IDto<T> Members

        public virtual void Save()
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
