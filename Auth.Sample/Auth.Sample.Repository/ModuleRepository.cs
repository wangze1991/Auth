using Auth.Sample.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Auth.Sample.Repository
{
    public class ModuleRepository:BaseRepository<T_Module>,IModuleRepository
    {
        /// <summary>
        /// 加载所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T_Module> LoadAll() {
            Sql sql = Sql.Builder.Append("SELECT * FROM T_Module");
            return this.Query(sql);
        }
        public EasyUiPageResult<T_Module> PageQuery(int pageIndex, int pageSize)
        {
            Sql sql = Sql.Builder.Append("SELECT * FROM T_Module");
            var list=this.Page(pageIndex, pageSize, sql);
            return new EasyUiPageResult<T_Module>() {total=list.TotalItems,rows=list.Items };
        }

        public IEnumerable<T_Button> GetButtonByModuleId(string id)
        {
            Sql sql = Sql.Builder.Append("SELECT b.* FROM  dbo.T_Module_Button a INNER JOIN  dbo.T_Button b ON a.ButtonId=b.Id WHERE a.[ModuleId]=@0",id);
           return this.Repo.Query<T_Button>(sql);
        }

        public IEnumerable<T_Module> GetMenuByUserId(int userId)
        {
            Sql sql = Sql.Builder.Append(@"select c.ModuleId as Id,MAX(c.Name) as Name,MAX(c.ParentCode) as ParentCode ,MAX(c.Url) as Url,MAX(c.MenuCode) as MenuCode,Max(c.Icon) as Icon,Max(c.Sort) as Sort  from  T_User a  INNER JOIN  T_Role_User b on a.id=b.UserId
                                            INNER JOIN(
                                            select c.Id as ModuleId,a.Id as RoleId,c.Name,c.Url,c.MenuCode,c.ParentCode,c.IsDisabled,c.Sort,c.Icon from T_Role a INNER JOIN T_Role_Module_Button b on a.Id=b.RoleId and a.IsDisabled=0 
                                            INNER JOIN T_Module c on b.ModuleId=c.MenuCode and c.IsView='1' )  
                                            c on b.RoleId=c.RoleId
                                            where  a.Id=@0
                                            GROUP BY c.ModuleId", userId);
            return Repo.Query<T_Module>(sql);
        }
    }
}
