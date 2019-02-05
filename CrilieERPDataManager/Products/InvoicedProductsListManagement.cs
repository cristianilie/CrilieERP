using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieERPDataManager.Orders
{
    public class InvoicedProductsListManagement : DbEntityOperations<InvoicedProduct>
    {
        // Updates an InvoicedProduct in the database
        public override void UpdateTableItem(int itemID, InvoicedProduct item)
        {
            if (item != null)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    var invoicedProduct = (from ip in dbc.InvoicedProducts
                                where ip.Id == itemID
                                select ip).Single();
                    invoicedProduct.OrderId = item.OrderId;
                    invoicedProduct.InvoicedQuantity = item.InvoicedQuantity;
                    invoicedProduct.InvoicePrice = item.InvoicePrice;

                    dbc.SubmitChanges();
                }
            }
        }





    }
}
