using Auth.Sample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Repository
{
    public interface IDepartmentRepository : IRepository<T_Department>
    {
        IEnumerable<T_Department> GetChild(string parentId);
    }
}
