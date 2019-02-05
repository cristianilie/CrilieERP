using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieERPDataManager.Products
{
    public class ProductStockManagement
    {

        public void AddTo_ProductStock(List<InvoicedProduct> incomingProducts)
        {
            if (incomingProducts.Count() > 0)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    for (int i = 0; i < incomingProducts.Count(); i++)
                    {
                        var tempProductStockItem = (from ps in dbc.ProductStocks
                                                   where ps.ProductId == incomingProducts[i].ProductId
                                                   select ps).Single();
                        tempProductStockItem.Quantity += incomingProducts[i].InvoicedQuantity;
                        tempProductStockItem.Cost = ProductPurchaseCostAmortisation(incomingProducts[i].InvoicePrice);
                    }

                    dbc.SubmitChanges();
                }
            }
        }

        //TO BE IMPLEMENTED  After gathering info about cost averaging on purchase price variations
        private decimal? ProductPurchaseCostAmortisation(decimal invoicePrice)
        {
            throw new NotImplementedException();
        }


        public void RemoveFrom_ProductStock(List<InvoicedProduct> outgoingProducts)
        {
            if (outgoingProducts.Count() > 0)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    for (int i = 0; i < outgoingProducts.Count(); i++)
                    {
                        var tempProductStockItem = (from ps in dbc.ProductStocks
                                                   where ps.ProductId == outgoingProducts[i].ProductId
                                                   select ps).Single();
                        if(tempProductStockItem.Quantity >= outgoingProducts[i].InvoicedQuantity)
                        {
                            tempProductStockItem.Quantity -= outgoingProducts[i].InvoicedQuantity;
                        }
                    }

                    dbc.SubmitChanges();
                }
            }
        }
    }
}
