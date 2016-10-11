using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain
{
    [TableName("dbo.T_Dep_User")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class T_Dep_User
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public string DepId { get; set; }
        [Column]
        public int? UserId { get; set; }
    }
}
