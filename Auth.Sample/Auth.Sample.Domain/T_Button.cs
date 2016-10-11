using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain
{

    [TableName("dbo.T_Button")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class T_Button
    {

        public T_Button()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }
        [Column]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }

        [Column]
        public string Code { get; set; }
        [Column]
        public int? Sort { get; set; }
        [Column]
        public string Icon { get; set; }
        [Column]
        public string Remark { get; set; }
        [Column]
        public DateTime? CreateTime { get; set; }
        [Column]
        public int? UpdateUserId { get; set; }
        [Column]
        public DateTime? UpdateTime { get; set; }
        [Column]
        public int? CreateUserId { get; set; }
    }
}
