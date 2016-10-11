using Auth.Sample.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Repository
{
    public interface IRoleUserRepository : IRepository<T_Role_User>
    {
        IEnumerable<T_Role> GetRolesByUserId(int userId);
        
        bool Save(int userId, int[] roles);
    }
}
