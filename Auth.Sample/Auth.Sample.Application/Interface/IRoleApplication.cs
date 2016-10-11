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
    public interface IRoleApplication:IBaseApplication<T_Role>
    {
        void SaveOrUpdate(RoleDto dto);

        EasyUiPageResult<RoleDto> Page(EasyUiPage page, System.Collections.Hashtable ht = null, string orderBy = null);
    }
}
