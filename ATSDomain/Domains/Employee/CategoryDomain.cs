using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Entites.Employee;
using ATSCommon.Enums;
using ATSDataAccess.SQLImlementation.Employee;
using ATSDomain.BaseClasses;

namespace ATSDomain.Domains.Employee
{
    public class CategoryDomain : BusinessDomainBase<Category>
    {
        public CategoryDomain(int userID, LanguagesEnum language)
            : base(userID, language)
        {
            DBRepository = new CategoryRepository();
        }

        public override void Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public override List<Category> FindAll()
        {
            return DBRepository.FindAll(ActionState);
        }

        public override Category FindByID(int entityID)
        {
            return DBRepository.FindByID(entityID, ActionState);
        }

        public override void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
