using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieERPDataManager.Partners
{
    public class BusinessPartnerTypeManagement : DbEntityOperations<BusinessPartnerType>
    {
        //Updates the BusinessPartnerType in the database
        public override void UpdateTableItem(int itemID, BusinessPartnerType item)
        {
            if (item != null)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    var bpt = (from p in dbc.BusinessPartnerTypes
                              where p.Id == itemID
                              select p).Single();
                    bpt.TypeName = item.TypeName;

                    dbc.SubmitChanges();
                }
            }
        }

        //Logic to search BusinessPartnerType by id
        public override BusinessPartnerType SearchTableById(int id)
        {
            BusinessPartnerType partnerType = new BusinessPartnerType();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                partnerType = (from p in dbc.BusinessPartnerTypes
                           where p.Id == id
                           select p).Single();
            }
            return partnerType;
        }

        //Logic to search BusinessPartnerType by name
        public override List<BusinessPartnerType> SearchTableByName(string name)
        {
            List<BusinessPartnerType> partnerTypes = new List<BusinessPartnerType>();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                partnerTypes = (from p in dbc.BusinessPartnerTypes
                            where p.TypeName.Contains(name)
                            select p).ToList();
            }
            return partnerTypes;
        }

        //Logic to search BusinessPartnerType by date
        public override List<BusinessPartnerType> SearchTableByDate(DateTime date)
        {
            return new List<BusinessPartnerType>();
        }

    }
}
