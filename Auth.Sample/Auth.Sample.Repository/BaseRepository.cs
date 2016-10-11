using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Configuration;
using Auth.Sample.Domain;
using Utils;
using PetaPoco;
using System.Collections;
namespace Auth.Sample.Repository
{
    public abstract class BaseRepository<T> : Auth.Sample.Repository.IRepository<T>
    {
        protected AuthDB Repo
        {
            get
            {
                return AuthDB.GetInstance();
            }
        }
        public string TableName
        {
            get
            {
                return TableInfo.FromPoco(typeof(T)).TableName;
            }
        }
        public string PrimaryKey
        {
            get
            {
                return TableInfo.FromPoco(typeof(T)).PrimaryKey;
            }
        }
        public virtual object Insert(T t) { return Repo.Insert(t); }
        public virtual void Save(T t) { Repo.Save(t); }
        public virtual T Get(object id)
        {
            return this.SingleOrDefault(id);
        }
        public virtual int Update(T t) { return Repo.Update(t); }
        public virtual int Delete(T t) { return Repo.Delete(t); }
        public virtual bool Exists(object primaryKey) { return Repo.Exists<T>(primaryKey); }

        public virtual int Update(IEnumerable<string> columns, T t) { return Repo.Update(t, columns); }

        /// <summary>
        /// EasyUiPage
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ht"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual Page<T> Page(EasyUiPage page, Hashtable ht = null, string orderBy = null)
        {
            Sql sql = Sql.Builder.Append("SELECT * FROM " + this.TableName);
            if (ht!=null&&ht.Count>0)
            {
                sql.Append(" WHERE ");
                for (var i = 0; i < ht.Count; i++)
                {
                    sql.Append(string.Format(" @{0}", i), ht[0]);
                }
            }
            if (orderBy.IsNotNullOrEmpty())
            {
                sql.Append(" Order By "+orderBy);
            }
           return this.Page(page.Page, page.Rows, sql);
        }


        public virtual EasyUiPageResult<T> EasyPage(EasyUiPage page, Hashtable ht = null, string orderBy = null)
        {
            var result = this.Page(page, ht, orderBy);
            return new EasyUiPageResult<T>() { rows = result.Items, total = result.TotalItems };
        }
        public virtual bool Exists(string sql, params object[] args) { return Repo.Exists<T>(sql, args); }

        #region  不是接口实现的方法

        public int Update(string sql, params object[] args) { return Repo.Update<T>(sql, args); }
        public int Update(Sql sql) { return Repo.Update<T>(sql); }

        public bool IsNew(T t) { return Repo.IsNew(t); }

        public int Delete(string sql, params object[] args) { return Repo.Delete<T>(sql, args); }
        public int Delete(Sql sql) { return Repo.Delete<T>(sql); }
        public int Delete(object primaryKey) { return Repo.Delete<T>(primaryKey); }


        public T SingleOrDefault(object primaryKey) { return Repo.SingleOrDefault<T>(primaryKey); }
        public T SingleOrDefault(string sql, params object[] args) { return Repo.SingleOrDefault<T>(sql, args); }
        public T SingleOrDefault(Sql sql) { return Repo.SingleOrDefault<T>(sql); }
        public T FirstOrDefault(string sql, params object[] args) { return Repo.FirstOrDefault<T>(sql, args); }
        public T FirstOrDefault(Sql sql) { return Repo.FirstOrDefault<T>(sql); }
        public T Single(object primaryKey) { return Repo.Single<T>(primaryKey); }
        public T Single(string sql, params object[] args) { return Repo.Single<T>(sql, args); }
        public T Single(Sql sql) { return Repo.Single<T>(sql); }
        public T First(string sql, params object[] args) { return Repo.First<T>(sql, args); }
        public T First(Sql sql) { return Repo.First<T>(sql); }
        public List<T> Fetch(string sql, params object[] args) { return Repo.Fetch<T>(sql, args); }
        public List<T> Fetch(Sql sql) { return Repo.Fetch<T>(sql); }
        public List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return Repo.Fetch<T>(page, itemsPerPage, sql, args); }
        public List<T> Fetch(long page, long itemsPerPage, Sql sql) { return Repo.Fetch<T>(page, itemsPerPage, sql); }
        public List<T> SkipTake(long skip, long take, string sql, params object[] args) { return Repo.SkipTake<T>(skip, take, sql, args); }
        public List<T> SkipTake(long skip, long take, Sql sql) { return Repo.SkipTake<T>(skip, take, sql); }
        public Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return Repo.Page<T>(page, itemsPerPage, sql, args); }
        public Page<T> Page(long page, long itemsPerPage, Sql sql) { return Repo.Page<T>(page, itemsPerPage, sql); }

        #region 延迟加载
        public virtual IEnumerable<T> Query(string sql, params object[] args) { return Repo.Query<T>(sql, args); }
        public virtual IEnumerable<T> Query(Sql sql) { return Repo.Query<T>(sql); }

        #endregion

        #endregion
    }
}
