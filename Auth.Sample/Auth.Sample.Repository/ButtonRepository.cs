using Auth.Sample.Domain;
using PetaPoco;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
namespace Auth.Sample.Repository
{
    public class ButtonRepository : BaseRepository<T_Button>, IButtonRepository
    {
        public IEnumerable<T_Button> LoadAll(Hashtable ht)
        {
            Sql sql = Sql.Builder.Append("SELECT * FROM " + this.TableName);
            if (ht != null && ht.Count > 0)
            {
                for (var i = 0; i < ht.Count; i++)
                {
                    sql.Append("@{0}".StringFormat(i), ht[i]);
                }
            }
            return this.Query(sql);
        }

        public IEnumerable<T_Button> LoadButtonsByModuleId(string moduleId)
        {
            Sql sql = Sql.Builder.Append(" SELECT a.* FROM "+this.TableName +" as a INNER JOIN T_Module_Button b on a.id=b.buttonid  Where b.moduleId=@0",moduleId);
            return this.Query(sql);
        }

        public IEnumerable<T_Button> GetButtonsByModuleId(string  moduleId)
        {
            Sql sql = Sql.Builder.Append("SELECT * FROM T_Module_Button WHERE ModuleId=@0 ",moduleId);
            return this.Query(sql);
        }
        /// <summary>
        /// 删除已经存在的关系
        /// </summary>
        /// <param name="ids"></param>
        public void AddButtonModuleRelationship(string moduleId,int[] buttons)
        {
            this.Repo.BeginTransaction();
            Sql sql = Sql.Builder.Append("DELETE FROM T_Module_Button WHERE moduleId=@0",moduleId);
            this.Repo.Execute(sql);
            foreach (var buttonId in buttons)
            {
                var relationShip = new T_Module_Button();
                relationShip.ModuleId = moduleId;
                relationShip.ButtonId =buttonId;
                this.Repo.Insert(relationShip);
            }
            this.Repo.CompleteTransaction();
        }
    }
}
