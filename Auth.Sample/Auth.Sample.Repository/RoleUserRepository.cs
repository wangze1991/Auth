using Auth.Sample.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Auth.Sample.Repository
{
    public class RoleUserRepository : BaseRepository<T_Role_User>, IRoleUserRepository
    {
        public IEnumerable<T_Role> GetRolesByUserId(int userId)
        {
            Sql sql = Sql.Builder.Append("SELECT b.* FROM  T_Role_User a inner join T_Role b on a.RoleId=b.Id where a.UserId=@0", userId);
            return this.Repo.Query<T_Role>(sql);
        }

        public bool Save(int userId,int[] roles)
        {
            try
            {
                this.Repo.BeginTransaction();
                Sql sql = Sql.Builder.Append("DELETE FROM T_Role_User WHERE UserId=@0", userId);
                this.Repo.Execute(sql);
                foreach (var role in roles)
                {
                    var obj = new T_Role_User()
                    {
                        RoleId = role,
                        UserId = userId
                    };
                    this.Repo.Insert(obj);
                }
                this.Repo.CompleteTransaction();
                return true;
            }
            catch (Exception ex)
            {
                this.Repo.AbortTransaction();
                throw ex;
            }
          
        }
    }
}
