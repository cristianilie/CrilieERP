using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieERPDataManager
{
    public class BusinessPartnerManagement : DbEntityOperations<BusinessPartner>
    {

        public override void UpdateTableItem(int itemID, BusinessPartner item)
        {
            if (item != null)
            {

                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    var bp = (from p in dbc.BusinessPartners
                                  where p.Id == itemID
                                  select p).Single();
                    bp.Name = item.Name;
                    bp.LegalInfo = item.LegalInfo;
                    bp.ContactInfo = item.ContactInfo;
                    bp.DeliveryInfo = item.DeliveryInfo;
                    bp.Type = item.Type;
                    bp.PaymentTerms = item.PaymentTerms;
                    bp.CustomPriceList = item.CustomPriceList;
                    bp.IsMasterPartner = item.IsMasterPartner;

                    dbc.SubmitChanges();
                }
            }
        }


        //Logic to search BusinessPartner by id
        public override BusinessPartner SearchTableById(int id)
        {
            BusinessPartner partner = new BusinessPartner();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                partner = (from p in dbc.BusinessPartners
                         where p.Id == id
                         select p).Single();
            }
            return partner;
        }

        //Logic to search BusinessPartner by name
        public override List<BusinessPartner> SearchTableByName(string name)
        {
            List<BusinessPartner> partners = new List<BusinessPartner>();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                partners = (from p in dbc.BusinessPartners
                            where p.Name.Contains(name)
                          select p).ToList();
            }
            return partners;
        }

        //Logic to search BusinessPartner by date 
        public override List<BusinessPartner> SearchTableByDate(DateTime date)
        {
            return new List<BusinessPartner>();
        }

    }
}
