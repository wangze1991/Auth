using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Infrastructure
{
    public enum UserLoginResult
    {
        用户名不存在=1,
        密码错误=2,
        登录成功=3,
        禁止登录=4
    }
}
