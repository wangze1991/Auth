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
    public class RoleApplication : BaseApplication<T_Role, IRoleRepository>, IRoleApplication
    {
        public RoleApplication(IRoleRepository repository)
            : base(repository)
        {


        }

        public override EasyUiPageResult<T_Role> EasyPage(EasyUiPage page, Hashtable ht = null, string orderBy = null)
        {
            return base.EasyPage(page, ht, orderBy);
        }

        public EasyUiPageResult<RoleDto> Page(EasyUiPage page, Hashtable ht = null, string orderBy = null)
        {
            var list = this.EasyPage(page, ht, orderBy);
            var result = new EasyUiPageResult<RoleDto>();
            result.total = list.total;
            result.rows = list.rows.ToList().Select(x =>
            {
                return TinyMapper.Map<RoleDto>(x);
            }).ToList();
            return result;
        }

        public void SaveOrUpdate(RoleDto dto)
        {
            if (dto.Id == 0)//新增
            {
                var role = TinyMapper.Map<T_Role>(dto);
                _currentRepository.Insert(role);

            }
            else//编辑
            {
                var old = this.Get(dto.Id);
                if (old == null) throw new Exception("出错了，请重新操作");
                var newEntity = TinyMapper.Map<RoleDto, T_Role>(dto, old);
                newEntity.UpdateTime = DateTime.Now;
                _currentRepository.Update(newEntity);
            }
        }

    }
}
