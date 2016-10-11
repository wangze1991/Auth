using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;

namespace Auth.Sample.Application
{
    public interface IButtonApplication:IBaseApplication<T_Button>
    {
      void SaveOrUpdate(IEnumerable<T_Button> list);
      IEnumerable<T_Button> LoadAll();
      IEnumerable<T_Button> LoadButtonsByModuleId(string moduleId);
      void AddButtonModuleRelationship(string moduleId, int[] buttonId);
    }
}
