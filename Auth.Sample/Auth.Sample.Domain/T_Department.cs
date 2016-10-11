using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain
{

    [TableName("dbo.T_Department")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class T_Department
    {
        public T_Department() {
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
            this.Icon = "icon-home";
        }


        [Column]
        public int Id { get; set; }
        [Column]
        public int ParentId { get; set; }
        [Column]
        public int? Sort { get; set; }

        [Column]
        public string Icon { get; set; }

        [Column]
        public string Name { get; set; }
        [Column]
        public string Remark { get; set; }
        [Column]
        public int? CreateUserId { get; set; }
        [Column]
        public DateTime? CreateTime { get; set; }
        [Column]
        public int? UpdateUserId { get; set; }
        [Column]
        public DateTime? UpdateTime { get; set; }
    }
}
