using Auth.Sample.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Repository
{
    public class DeparatmentRepository : BaseRepository<T_Department>, IDepartmentRepository
    {


        public IEnumerable<T_Department> GetChild(string parentId)
        {
            Sql sql = Sql.Builder.Append(" SELECT * FROM T_Department WHERE ParentId=@0", parentId);
            return this.Query(sql);
        }

    }
}
