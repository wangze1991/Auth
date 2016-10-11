using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain
{
    [TableName("dbo.T_Role_Module_Button")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class T_Role_Module_Button
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public string ModuleId { get; set; }
        [Column]
        public int ButtonId { get; set; }
        [Column]
        public int? RoleId { get; set; }
    }
}
