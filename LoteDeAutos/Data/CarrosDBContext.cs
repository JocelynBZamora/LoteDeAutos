using LoteDeAutos.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteDeAutos.Data
{
    public class CarrosDBContext
    {
        //Forma rapida para poder crear la tabla en sql y poder hacer el CRUD
        private const string _ConectionString = "Data Source=carros.db";

        private string _dataBaseFilName = "lote.db";
        const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            //crea la base de datos si no existe
            SQLite.SQLiteOpenFlags.Create |
            //abilita multi-threaded acces
            SQLite.SQLiteOpenFlags.SharedCache;

        public string DataBasePath => Path.Combine(FileSystem.AppDataDirectory, _dataBaseFilName);
        private string _conectionString = "";



        SQLiteAsyncConnection db;

        //Crea la tabla en sql
        public async Task Init()
        {
            if (db != null)
            {
                return;
            }
            db = new SQLiteAsyncConnection(DataBasePath, Flags);
            SQLite.CreateFlags createFlags = SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK;
            var resulr = await db.CreateTableAsync<CarroModel>(createFlags);
        }


        public async Task Add(CarroModel articulo)
        {
            await Init();
            await db.InsertAsync(articulo);
        }
        public async Task<CarroModel> GetById(int id)
        {
            await Init();
            return await db.Table<CarroModel>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CarroModel>> GetAll()
        {
            await Init();
            return await db.Table<CarroModel>().ToListAsync();
        }
        public async Task<CarroModel> Actualizar(CarroModel carro)
        {
            await Init();
            await db.UpdateAsync(carro);
            var auton = await GetById(carro.Id);
            return auton;
        }
        public async Task<bool> Eliminar(int id)
        {
            await Init();
            var art = await GetById(id);
            if (art != null)
            {
                await db.DeleteAsync(art);
            }
            return false;
        }
    }
}
