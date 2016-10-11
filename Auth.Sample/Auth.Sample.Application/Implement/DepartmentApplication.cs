using Auth.Sample.Domain;
using Auth.Sample.Domain.Dto;
using Auth.Sample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Nelibur.ObjectMapper;
namespace Auth.Sample.Application
{
    public class DepartmentApplication : BaseApplication<T_Department, IDepartmentRepository>, IDepartmentApplication
    {
        public DepartmentApplication(IDepartmentRepository repository)
            : base(repository)
        {
        }
        
        /// <summary>
        /// 获取子元素
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IEnumerable<TreeDto> GetChild(string parentId)
        {
            var list = this._currentRepository.GetChild(parentId);
            if (!list.IsNotNull()) return null;
            return list.Select(x =>
            {
                return TinyMapper.Map<TreeDto>(x);
            });
        }

        public void Update(DepartmentDto dto)
        {
            if (dto.Id == 0) throw new Exception("出错了，没有Id");
            var department = this._currentRepository.Get(dto.Id);
            if (department == null) throw new Exception("没有找到要编辑的数据");
            department = TinyMapper.Map(dto, department);
            department.UpdateTime = DateTime.Now;
            this._currentRepository.Update(department);
        }

        
    }
}
