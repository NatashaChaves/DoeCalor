using DoeCalor.Models;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DoeCalor.Database
{
    public class DbAccess<T> : IDisposable where T : class, new()
    {
        private SQLiteConnection _sqLiteConnection;

        public DbAccess()
        {
            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("banco.db3", CreationCollisionOption.OpenIfExists);

            _sqLiteConnection = new SQLiteConnection(arquivo.Path);

            _sqLiteConnection.CreateTable<Usuario>();
        }

        public void Save(T item)
        {
            _sqLiteConnection.Insert(item);
        }

        public IEnumerable<T> ListAll()
        {
            return _sqLiteConnection.Table<T>().ToList();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _sqLiteConnection.Table<T>().Where(predicate).ToList();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _sqLiteConnection.Find<T>(predicate);
        }

        public void Delete(int id)
        {
            _sqLiteConnection.Delete<T>(id);
        }

        public void Update(T item)
        {
            _sqLiteConnection.Update(item);
        }

        public void Dispose()
        {
            _sqLiteConnection.Dispose();
        }
    }
}
