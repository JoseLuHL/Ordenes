using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SwipeMenu.Models;
using XFFurniture.Models;

namespace XamarinSQLite
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<ClienteModelo>().Wait();
        }

        //Insert and Update new record
        public Task<int> SaveItemAsync(ClienteModelo person)
        {
            return db.InsertAsync(person);
        } 
        public Task<int> UpdateItemAsync(ClienteModelo person)
        {
            return db.UpdateAsync(person);
           
        }

        //Delete
        public Task<int> DeleteItemAsync()
        {
            return db.DeleteAllAsync<ClienteModelo>();
        }

        //Read All Items
        public  Task<List<ClienteModelo>> GetItemsAsync()
        {
            return  db.Table<ClienteModelo>().ToListAsync();
        }


        //Read Item
        public Task<ClienteModelo> GetItemAsync(int personId)
        {
            return db.Table<ClienteModelo>().Where(i => i.ClieId == personId).FirstOrDefaultAsync();
        }
    }
}
