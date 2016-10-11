using Auth.Sample.Domain;
using Auth.Sample.Domain.Dto;
using Auth.Sample.Infrastructure;
using Auth.Sample.Repository;
using Nelibur.ObjectMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;


namespace Auth.Sample.Application
{
    public class ModuleButtonApplication  : BaseApplication<T_Module_Button, IModuleButtonRepository>, IModuleButtonApplication
    {
        public ModuleButtonApplication(IModuleButtonRepository repository)
            : base(repository)
        {

        }
    }
}
