using Auth.Sample.Domain;
using Auth.Sample.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Application
{
    public interface IDepartmentApplication:IBaseApplication<T_Department>
    {
        IEnumerable<TreeDto> GetChild(string parentId);
        void Update(DepartmentDto dto);
    }
}
