using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
using PetaPoco;
namespace Auth.Sample.Repository
{
    public class RoleModuleButtonRepository:BaseRepository<T_Role_Module_Button>,IRoleModuleButtonRepository
    {
        public  override int Delete(T_Role_Module_Button button)
        {
            Sql sql = Sql.Builder.Append("DELETE FROM T_Role_Module_Button Where ModuleId=@0 and RoleId=@1 and ButtonId=@2", button.ModuleId, button.RoleId, button.ButtonId);
           return this.Repo.Delete<T_Role_Module_Button>(sql);
        }

        public  T_Role_Module_Button Get(T_Role_Module_Button button)
        {
            Sql sql = Sql.Builder.Append(" SELECT * FROM  T_Role_Module_Button  Where ModuleId=@0 and RoleId=@1 and ButtonId=@2 ", button.ModuleId, button.RoleId, button.ButtonId);
           return this.Repo.SingleOrDefault<T_Role_Module_Button>(sql);
        }

        public  IEnumerable<T_Role_Module_Button> LoadByRoleId(int roleId)
        {
            Sql sql = Sql.Builder.Append(" SELECT * FROM T_Role_Module_Button WHERE RoleId=@0",roleId);
            return this.Repo.Query<T_Role_Module_Button>(sql);
        }
      
    }
}
