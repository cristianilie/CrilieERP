using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieERPDataManager
{
    public class OrderManagement : DbEntityOperations<Order>
    {

        //Updates the changes made to an order, in the Orders database table
        public override void UpdateTableItem(int itemID, Order item)
        {
            if (item != null)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    var _order = (from o in dbc.Orders
                                  where o.Id == itemID
                                  select o).Single();
                    _order.OrderInfo = item.OrderInfo;
                    _order.VendorId = item.VendorId;
                    _order.CustomerId = item.CustomerId;
                    _order.DocumentDate = item.DocumentDate;
                    _order.DeliveryDate = item.DeliveryDate;
                    _order.DueDate = item.DueDate;
                    _order.IsOpen = item.IsOpen;
                    _order.TaxRateId = item.TaxRateId;
                    _order.Remarks = item.Remarks;

                    dbc.SubmitChanges();
                }
            }
        }

        //Searches an order by id/name/date and returns a list of matching orders

        //Logic to search order by id
        public override Order SearchTableById(int id)
        {
            Order order = new Order();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                order = (from o in dbc.Orders
                        where o.Id == id
                        select o).Single();
            }
            return order;
        }

        //Logic to search order by name
        public override List<Order> SearchTableByName(string name)
        {
            List<Order> orders = new List<Order>();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                orders = (from o in dbc.Orders
                         where o.BusinessPartner.Name.Contains(name)
                         select o).ToList();
                if (orders.Count == 0)
                {
                    orders = (from o in dbc.Orders
                              where o.OrderInfo.Contains(name)
                              select o).ToList();
                }
            }
            return orders;
        }

        //Logic to search order by date
        public override List<Order> SearchTableByDate(DateTime date)
        {
            List<Order> orders = new List<Order>();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                orders = (from o in dbc.Orders
                          where o.DocumentDate.Date.Equals(date.Date)
                          select o).ToList();
            }
            return orders;
        }
    }
}
