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
    public interface IRoleUserApplication:IBaseApplication<T_Role_User>
    {
        IEnumerable<T_Role> GetRolesByUserId(int userId);

        bool Save(int userId, int[] roles);
    }
}
