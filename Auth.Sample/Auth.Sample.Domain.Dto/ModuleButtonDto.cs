using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain.Dto
{
    public class ModuleButtonDto
    {
        public string ModuleId { get; set; }

        public string ModuleName { get; set; }

        public string ParentCode { get; set; }

        public string iconCls { get; set; }


        public IList<T_Button> Buttons { get; set; }

        public ModuleButtonDto() {
            this.Buttons = new List<T_Button>();
        }

    }
}
