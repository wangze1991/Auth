using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain
{
    [TableName("dbo.T_User")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class T_User 
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public string UserName { get; set; }
        [Column]
        public string Pwd { get; set; }
        [Column]
        public string Remark { get; set; }
        [Column]
        public bool? IsOpen { get; set; }
        [Column]
        public int? LoginTime { get; set; }
        [Column]
        public DateTime? LastLoginTime { get; set; }
        [Column]
        public int? CreateUserId { get; set; }
        [Column]
        public DateTime? CreateTime { get; set; }
        [Column]
        public int? UpdateUser { get; set; }
        [Column]
        public DateTime? UpdateTime { get; set; }

        [Column]
        public string Sex { get; set; }

        [Column]
        public string Mobile { get; set; }

        [Column]
        public string QQ { get; set; }
    }
}
