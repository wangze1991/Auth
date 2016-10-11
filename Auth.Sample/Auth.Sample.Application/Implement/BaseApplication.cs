using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
using Auth.Sample.Repository;
using Utils;
using System.Collections;

namespace Auth.Sample.Application
{
    public abstract class BaseApplication<T, R> : IBaseApplication<T> where R : IRepository<T>
    {
        protected R _currentRepository;
        public BaseApplication(R repository)
        {
            this._currentRepository = repository;
        }

        public virtual void Insert(T t)
        {
            this._currentRepository.Insert(t);
        }
        public virtual void Delete(T t)
        {
            this._currentRepository.Delete(t);
        }

        public virtual void Delete(object id)
        {
            this._currentRepository.Delete(id);
        }
        public virtual T Get(object id)
        {
            return this._currentRepository.Get(id);
        }
        public virtual int Update(T t)
        {
            return this._currentRepository.Update(t);
        }
        public virtual EasyUiPageResult<T> EasyPage(EasyUiPage page, Hashtable ht = null, string orderBy = null)
        {
            return this._currentRepository.EasyPage(page, ht, orderBy);
        }

    }
}
