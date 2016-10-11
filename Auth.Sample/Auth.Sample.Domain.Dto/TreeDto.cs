using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain.Dto
{
    /// <summary>
    /// easyui tree
    /// </summary>
    public class TreeDto
    {
        public int id;
        public string text;
        public string state;
        public IList<TreeDto> children;
        public string iconCls { get; set; }
        public TreeDto() {
            this.state = "closed";
            this.iconCls = "icon-home";
            children = new List<TreeDto>();
        }
    }
}
