using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
namespace Auth.Sample.Repository
{
    public interface IUserRepository : IRepository<T_User>
    {
        T_User GetByUserName(string userName);
    }
}
