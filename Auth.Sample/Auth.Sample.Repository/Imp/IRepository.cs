using PetaPoco;
using System;
using System.Collections;
using System.Collections.Generic;
using Utils;
namespace Auth.Sample.Repository
{
   public  interface IRepository<T>
    {
  
        int Delete(object primaryKey);

        T Get(object id);

        int Delete(T t);
        bool Exists(object primaryKey);
       
        object Insert(T t);
   
        void Save(T t);
      
        int Update(T t);

        int Update(IEnumerable<string> columns, T t);

        Page<T> Page(EasyUiPage page, Hashtable ht = null, string orderBy = null);

        EasyUiPageResult<T> EasyPage(EasyUiPage page, Hashtable ht = null, string orderBy = null);
    }
}
