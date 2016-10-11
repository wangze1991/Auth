using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
using Auth.Sample.Domain.Dto;
using System.ComponentModel;
namespace Auth.Sample.Domain.Dto.Mapping
{
    public class TinyMapperConfig
    {
        public static void RegisterMapperConfig(){

            //Department
            TinyMapper.Bind<T_Department, TreeDto>(x =>
            {
                x.Bind(source => source.Id, target => target.id);
                x.Bind(source => source.Name, target =>target.text );
                x.Bind(source =>source.Icon, taget => taget.iconCls);
            });

            TinyMapper.Bind<T_Module, ModuleButtonDto>(x => {
                x.Bind(source => source.MenuCode, target => target.ModuleId);
                x.Bind(source => source.ParentCode, target => target.ParentCode);
                x.Bind(source => source.Name, target => target.ModuleName);
                x.Bind(source => source.Icon, target => target.iconCls); 
            });

            //Role
            TypeDescriptor.AddAttributes(typeof(T_Role), new TypeConverterAttribute(typeof(RoleConverter)));
        }




      
    }
}
