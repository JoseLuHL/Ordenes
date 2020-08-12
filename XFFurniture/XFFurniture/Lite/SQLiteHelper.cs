using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //db.CreateTableAsync<Subcategorium>().Wait();
            //db.CreateTableAsync<Categoria>().Wait();
            //db.CreateTableAsync<TiendaModelo>().Wait();
            db.CreateTableAsync<TiendaModelo>().Wait();
            db.CreateTableAsync<ProductoModelo>().Wait();
        }

        private SQLiteAsyncConnection _sqlCon;





        //public async Task<IList<TiendaModelo>> GetAll()
        //{
        //    return await _sqlCon.GetAllWithChildrenAsync<TiendaModelo>();
        //}



        //
        public async Task<int> GuardarAsync<T>(T modelo)
        {
            var datos = await db.InsertAsync(modelo);
            return datos;
        }
        public async Task<ObservableCollection<TiendaModelo>> GetTiendaAsync()
        {
            var datos = await db.Table<TiendaModelo>().ToListAsync();
            return new ObservableCollection<TiendaModelo>(datos);
        }
        public async Task<ObservableCollection<ProductoModelo>> GetProductoAsync()
        {
            var datos = await db.Table<ProductoModelo>().ToListAsync();
            return new ObservableCollection<ProductoModelo>(datos);
        }

        public async Task<int> EliminarAsync<T>(T modelo)
        {
            var datos = await db.DeleteAsync(modelo);
            return datos;
        }

        public async Task<int> ActualizarAsync<T>(T modelo)
        {
            var datos = await db.UpdateAsync(modelo);
            return datos;
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
        public Task<int> EliminarTiendaAsync()
        {
            return db.DeleteAllAsync<TiendaModelo>();
        }
        public Task<int> EliminarproductoAsync()
        {
            return db.DeleteAllAsync<ProductoModelo>();
        }

        //Read All Items
        public Task<List<ClienteModelo>> GetItemsAsync()
        {
            return db.Table<ClienteModelo>().ToListAsync();
        }



        //Read Item
        public Task<ClienteModelo> GetItemAsync(int personId)
        {
            return db.Table<ClienteModelo>().Where(i => i.ClieId == personId).FirstOrDefaultAsync();
        }
    }
}
