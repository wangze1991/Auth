using Auth.Sample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Application
{
    public interface IRoleModuleButtonApplication : IBaseApplication<T_Role_Module_Button>
    {
        T_Role_Module_Button Get(T_Role_Module_Button button);

        IEnumerable<T_Role_Module_Button> LoadByRoleId(int roleId);
    }
}
