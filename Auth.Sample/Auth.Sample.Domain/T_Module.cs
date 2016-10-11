using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain
{
    [TableName("dbo.T_Module")]
    [PrimaryKey("Id",AutoIncrement=false)]
    [ExplicitColumns]
    public partial class T_Module 
    {

        public T_Module() {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }


        [Column]
        public string Id { get; set; }

        [Column]
        public string Icon { get; set; }

        [Column]
        public string MenuCode { get; set; }
        [Column]
        public int? Sort { get; set; }
        [Column]
        public string ParentCode { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public bool? IsView { get; set; }
        [Column]
        public bool? IsDisabled { get; set; }
        [Column]
        public int? CreateUserId { get; set; }
        [Column]
        public DateTime? CreateTime { get; set; }
        [Column]
        public int? UpdateUserId { get; set; }
        [Column]
        public string Url { get; set; }
        [Column]
        public DateTime? UpdateTime { get; set; }
    }
}
