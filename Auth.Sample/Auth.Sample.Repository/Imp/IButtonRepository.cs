using Auth.Sample.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Repository
{
    public interface IButtonRepository : IRepository<T_Button>
    {
        IEnumerable<T_Button> LoadAll(Hashtable ht);

        IEnumerable<T_Button> LoadButtonsByModuleId(string moduleId);

        void AddButtonModuleRelationship(string moduleId, int[] buttons);
    }
}
