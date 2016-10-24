using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Carubbi.StateMachine
{
    public abstract class ListBase<T, U> : Dto<U>, IList<T>, ICollection
    {
        public ListBase()
        {
            _lista = new List<T>();
        
        }

        protected IList<T> _lista;

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _lista.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _lista.GetEnumerator();
        }

        #endregion

        #region IEnumerator<T> Members

        public T Current
        {
            get { return this._lista.GetEnumerator().Current; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _lista.GetEnumerator().Dispose();
            this.GetEnumerator().Dispose();
        }

        #endregion

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return _lista.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _lista.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _lista.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return _lista[index];
            }
            set
            {
                _lista[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        public virtual void Add(T item)
        {
            _lista.Add(item);
        }

        public void Clear()
        {
            _lista.Clear();
        }

        public bool Contains(T item)
        {
            return _lista.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _lista.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _lista.Count; }
        }

        public bool IsReadOnly
        {
            get { return _lista.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return _lista.Remove(item);
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            this._lista.CopyTo((T[])array, index);
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public override string ToString()
        {
            StringBuilder retorno = new StringBuilder();

            foreach (T item in this)
                if (!String.IsNullOrEmpty(item.ToString()))
                    retorno.AppendFormat("{0},", item.ToString());

            // Remover a última vírgula
            if (retorno.ToString().EndsWith(","))
                retorno = retorno.Remove(retorno.Length - 1, 1);

            return retorno.ToString();
        }

        #endregion
    }
}
