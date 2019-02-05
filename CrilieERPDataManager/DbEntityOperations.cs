using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieERPDataManager
{
    public class DbEntityOperations<T> where T : class, new()
    {
        //Creates a new item in the specified database T Table
        public void CreateTableItem(T item)
        {
            if (item != null)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    dbc.GetTable<T>().InsertOnSubmit(item);
                    dbc.GetTable<T>().Context.SubmitChanges();
                }
            }
        }

        //Updates an item in the database T Table
        //To be implemented by the inheriting classes
        public virtual void UpdateTableItem(int itemID, T item) { throw new NotImplementedException(); }

        //Deletes an item in the  database T Table
        public void DeleteTableItem(T item)
        {
            if (item != null)
            {
                using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
                {
                    dbc.GetTable<T>().DeleteOnSubmit(item);
                    dbc.SubmitChanges();
                }
            }
        }

        //Searches an item in the  database T Table(by id, or by name)
        //To be implemented by the inheriting classes
        public List<T> SearchTableItem(int srcById = 0, string srcByName = "", DateTime? srcByDate = null)
        {
            List<T> foundItems = new List<T>();
            using (ErpDataManagerDataContext dbc = new ErpDataManagerDataContext())
            {
                if (srcById != 0)
                {
                    foundItems.Add(SearchTableById(srcById));
                }

                if (!string.IsNullOrEmpty(srcByName) || !string.IsNullOrWhiteSpace(srcByName))
                {
                    foreach (var item in SearchTableByName(srcByName))
                    {
                        foundItems.Add(item);
                    }
                }

                if (srcByDate != null)
                {
                    foreach (var item in SearchTableByDate((DateTime)srcByDate))
                    {
                        foundItems.Add(item);
                    }
                }
            }

            return foundItems;
        }

        //To be implemented if functionality is needed - Logic to search table by id
        public virtual T SearchTableById(int id)
        {
            return null;
        }

        //To be implemented if functionality is needed - Logic to search table by name
        public virtual List<T> SearchTableByName(string name)
        {
            return null;
        }

        //To be implemented if functionality is needed - Logic to search table by date
        public virtual List<T> SearchTableByDate(DateTime date)
        {
            return null;
        }
    }
}