using Auth.Sample.Domain;
using Auth.Sample.Domain.Dto;
using Auth.Sample.Infrastructure;
using Auth.Sample.Repository;
using Nelibur.ObjectMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Auth.Sample.Application
{
    public class RoleModuleButtonApplication : BaseApplication<T_Role_Module_Button, IRoleModuleButtonRepository>, IRoleModuleButtonApplication
    {

        public RoleModuleButtonApplication(IRoleModuleButtonRepository repository)
            : base(repository)
        {

        }
        public T_Role_Module_Button Get(T_Role_Module_Button button)
        {
            return _currentRepository.Get(button);
        }
        public  IEnumerable<T_Role_Module_Button> LoadByRoleId(int roleId)
        {
            return _currentRepository.LoadByRoleId(roleId);
        }

    }
}
