using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
namespace Auth.Sample.Application
{
    public interface IAuthenticationApplication
    {
        void SignIn(T_User user, bool createPersistentCookie);
        void SignOut();
    }
}
