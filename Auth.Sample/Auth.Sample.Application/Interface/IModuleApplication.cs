using Auth.Sample.Domain;
using Auth.Sample.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Auth.Sample.Application
{
    public interface IModuleApplication:IBaseApplication<T_Module>
    {
        IEnumerable<T_Module> LoadAll();
        EasyUiPageResult<T_Module> PageQuery(int pageIndex, int pageSize);
        void SaveOrUpdate(string json);
        IEnumerable<ModuleButtonDto> LoadAllWithButtons ();

        /// <summary>
        /// 根据用户id获取menu菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T_Module> GetMenuByUserId(int userId);
    }
}
