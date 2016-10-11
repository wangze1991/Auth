using Auth.Sample.Domain;
using Auth.Sample.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
namespace Auth.Sample.Application
{
    public class AccountApplicaton : IAccountApplicaton
    {
        private IUserApplication _userApplication;

        public AccountApplicaton(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public Tuple<UserLoginResult, T_User> ValidateUser(string userName, string password)
        {
            var user = _userApplication.GetByUserName(userName);
            if (user == null) return new Tuple<UserLoginResult, T_User>(UserLoginResult.用户名不存在, null);
            //TODO  现在默认密码为111111
            //if (Encrypt.CreatePasswordHash(password) != user.Pwd) return UserLoginResult.密码错误;
            if (password != "111111") return new Tuple<UserLoginResult, T_User>(UserLoginResult.密码错误, null);
            user.UpdateTime = DateTime.Now;
            if (user.IsOpen == false) new Tuple<UserLoginResult, T_User>(UserLoginResult.禁止登录, user);
            return new Tuple<UserLoginResult, T_User>(UserLoginResult.登录成功, user);
        }

    }
}
