using Auth.Sample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using  Auth.Sample.Repository;
namespace Auth.Sample.Application
{
    public class UserApplication : BaseApplication<T_User, IUserRepository>, IUserApplication
    {
        public UserApplication(IUserRepository repository)
            : base(repository)
        {

        }

        public T_User GetByUserName(string userName)
        {
            return _currentRepository.GetByUserName(userName);
        }
       
    }
}
