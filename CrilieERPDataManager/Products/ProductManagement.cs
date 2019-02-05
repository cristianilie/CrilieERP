using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieERPDataManager.Products
{
    public class ProductManagement : DbEntityOperations<Product>
    {
        //Updates a product in the database
        public override void UpdateTableItem(int itemID, Product item)
        {
            if (item != null)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    var product = (from p in dbc.Products
                               where p.Id == itemID
                               select p).Single();
                    product.Name = item.Name;

                    dbc.SubmitChanges();
                }
            }
        }

        //Logic to search Product by id
        public override Product SearchTableById(int id)
        {
            Product product = new Product();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                product = (from p in dbc.Products
                           where p.Id == id
                           select p).Single();
            }
            return product;
        }

        //Logic to search Product by name
        public override List<Product> SearchTableByName(string name)
        {
            List<Product> products = new List<Product>();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                products = (from p in dbc.Products
                            where p.Name.Contains(name)
                            select p).ToList();
            }
            return products;
        }

        //Logic to search Product by date 
        public override List<Product> SearchTableByDate(DateTime date)
        {
            return new List<Product>();
        }
    }
}
