using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        public string Icon { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }


    }
}
