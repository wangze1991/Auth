using System;
using  Auth.Sample.Infrastructure;
using Auth.Sample.Domain;
namespace Auth.Sample.Application
{
    public  interface IAccountApplicaton
    {
       Tuple<UserLoginResult, T_User> ValidateUser(string userName, string password);
    }
}
