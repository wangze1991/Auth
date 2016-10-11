using System;
using System.Collections;
using System.Linq;
using Utils;
namespace Auth.Sample.Application
{
    public interface IBaseApplication<T>
    {
        void Delete(T t);
        void Delete(object id);
        T Get(object id);
        void Insert(T t);
        EasyUiPageResult<T> EasyPage(EasyUiPage page, Hashtable ht = null, string orderBy = null);
        int Update(T t);
    }
}
