using Auth.Sample.Domain;
using Auth.Sample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
namespace Auth.Sample.Application
{
    public class ButtonApplication : BaseApplication<T_Button,IButtonRepository>,IButtonApplication
    {
        public ButtonApplication(IButtonRepository repository)
            : base(repository)
        {
        }

        public void SaveOrUpdate(IEnumerable<T_Button> list)
        {
            if (!list.IsNotNull()) throw new Exception("没有数据");
            foreach (var button in list)
            {
                if (button.Id == 0)//新增
                {
                    this.Insert(button);
                }
                else//修改
                {
                    var entity = this.Get(button.Id);
                    if (entity == null) throw new Exception("没有这条数据");
                    entity.Name = button.Name;
                    entity.Icon = button.Icon;
                    entity.Sort = button.Sort;
                    entity.Remark = button.Remark;
                    entity.Code = button.Code;
                    entity.UpdateTime = DateTime.Now;
                    this.Update(entity);
                }
            }
        }

        public IEnumerable<T_Button> LoadAll()
        {
            return this._currentRepository.LoadAll(null);
        }

        public IEnumerable<T_Button> LoadButtonsByModuleId(string moduleId) {

            return this._currentRepository.LoadButtonsByModuleId(moduleId);
        }
        
        public void AddButtonModuleRelationship(string moduleId, int[] buttonId)
        {
             this._currentRepository.AddButtonModuleRelationship(moduleId, buttonId);
        }
    }
}
