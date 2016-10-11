using Auth.Sample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Application
{
    public interface IUserApplication:IBaseApplication<T_User>
    {
        T_User GetByUserName(string userName);
    }
}
