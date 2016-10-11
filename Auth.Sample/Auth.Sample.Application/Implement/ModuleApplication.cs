using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Sample.Domain;
using Auth.Sample.Repository;
using Newtonsoft.Json;
using Utils;
using Nelibur.ObjectMapper;
using Auth.Sample.Domain.Dto;
namespace Auth.Sample.Application
{
    public class ModuleApplication : BaseApplication<T_Module, IModuleRepository>, IModuleApplication
    {
        public ModuleApplication(IModuleRepository repository)
            : base(repository)
        {

        }

        /// <summary>
        /// 加载所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T_Module> LoadAll()
        {
            return _currentRepository.LoadAll();
        }

        public IEnumerable<ModuleButtonDto> LoadAllWithButtons()
        {
            var list = _currentRepository.LoadAll();
            if (list.IsNotNull())
            {
                return list.Select(x =>
                {
                    var result = TinyMapper.Map<ModuleButtonDto>(x);
                    result.Buttons = _currentRepository.GetButtonByModuleId(result.ModuleId).ToList();
                    return result;
                });
            }
            return null;
        }

        public EasyUiPageResult<T_Module> PageQuery(int pageIndex, int pageSize)
        {
            return _currentRepository.PageQuery(pageIndex, pageSize);
        }

        public void SaveOrUpdate(string json)
        {
            if (json.IsNullOrEmpty()) throw new Exception("数据为空");
            try
            {
                List<T_Module> list = JsonConvert.DeserializeObject<List<T_Module>>(json);
                this.SaveOrUpdate(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveOrUpdate(IList<T_Module> list)
        {
            if (!list.IsNotNull()) throw new Exception("参数为空");
            foreach (var module in list)
            {
                if (module.Id.IsNullOrEmpty())//新增
                {
                    module.Id = Guid.NewGuid().ToString();
                    this.Insert(module);
                }
                else//编辑
                {
                    module.UpdateTime = DateTime.Now;
                    this.Update(module);
                }
            }
        }

        public IEnumerable<T_Module> GetMenuByUserId(int userId)
        {
            return this._currentRepository.GetMenuByUserId(userId);
        }

    }
}
