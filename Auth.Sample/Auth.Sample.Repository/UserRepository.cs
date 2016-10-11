using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
using Utils;
using PetaPoco;
namespace Auth.Sample.Repository
{
    public class UserRepository:BaseRepository<T_User>,IUserRepository
    {
        public T_User GetByUserName(string userName)
        {
            var sql = "SELECT * FROM  {0} where UserName=@0".StringFormat(TableName);
            return this.SingleOrDefault(sql, userName);
        }
    }
}
