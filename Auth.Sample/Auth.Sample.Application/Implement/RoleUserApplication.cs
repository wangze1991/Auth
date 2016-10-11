using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
using Auth.Sample.Repository;
using Newtonsoft.Json;
using Utils;
using Nelibur.ObjectMapper;
using Auth.Sample.Domain.Dto;
namespace Auth.Sample.Application
{
    public class RoleUserApplication : BaseApplication<T_Role_User, IRoleUserRepository>,IRoleUserApplication
    {
        public RoleUserApplication(IRoleUserRepository roleUserRep):base(roleUserRep)
        {

        }

        public IEnumerable<T_Role> GetRolesByUserId(int userId)
        {
            return this._currentRepository.GetRolesByUserId(userId);
        }

        public bool Save(int userId, int[] roles)
        {
            return this._currentRepository.Save(userId, roles);
        }
    }
}
