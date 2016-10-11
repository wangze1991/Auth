using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
using PetaPoco;
using Utils;
namespace Auth.Sample.Repository
{
    public interface IModuleRepository:IRepository<T_Module>
    {
        IEnumerable<T_Module> LoadAll();

        EasyUiPageResult<T_Module> PageQuery(int pageIndex, int pageSize);

        IEnumerable<T_Button> GetButtonByModuleId(string id);

        /// <summary>
        /// 根据用户id获取menu菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T_Module> GetMenuByUserId(int userId);
    }
}
