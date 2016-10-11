using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain.Dto
{
    public  class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }

        public int Sort { get; set; }

        public bool IsDisabled { get; set; }

        public string IsDisabledName { get; set; }
    }
}
