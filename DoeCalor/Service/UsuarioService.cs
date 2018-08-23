using DoeCalor.Database;
using DoeCalor.Helpers;
using DoeCalor.Models;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoeCalor.Service
{
    public class UsuarioService
    {
        public static DbAccess<Usuario> dbAccess = new DbAccess<Usuario>();

        public static void Insert(Usuario usuario)
        {
            usuario.CreationDate = DateTime.Now;
            usuario.IsLogged = true;
            dbAccess.Save(usuario);
        }

        public static Usuario Authenticate(string email, string password)
        {
            var usuario = dbAccess.Find(x => x.Email.ToLower().Equals(email.ToLower()) && x.Password.Equals(password));
            if (usuario != null)
            {
                usuario.IsLogged = true;
                Edit(usuario);
                return usuario;
            }

            return null;
        }

        public static void Edit(Usuario usuario)
        {
            dbAccess.Update(usuario);
        }

        public static Usuario Load(int id)
        {
            return dbAccess.Find(x => x.Id == id);
        }

        public static void Remove(int id)
        {
            dbAccess.Delete(id);
        }

        public static Usuario GetUserLogged()
        {
            return List(x => x.IsLogged).FirstOrDefault();
        }

        public static void Logout(Usuario usuario)
        {
            usuario.IsLogged = false;
            Edit(usuario);
        }

        public static List<Usuario> List(Expression<Func<Usuario, bool>> predicate)
        {
            return dbAccess.List(predicate).ToList();
        }

        public static List<Usuario> ListAll()
        {
            return dbAccess.ListAll().ToList();
        }

        public async static Task<string> SaveOrUpdatePhoto(Stream stream, string email)
        {
            //Create File Photo
            var rootFolder = new LocalRootFolder();
            var folder = await rootFolder.CreateFolderAsync(email, CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync("profile.jpg", CreationCollisionOption.ReplaceExisting);

            var memory = new MemoryStream();
            stream.CopyTo(memory);
            byte[] bytes = memory.ToArray();

            file.WriteAllBytes(bytes);
            Settings.UrlPhoto = file.Path;
            return file.Path;
        }
    }
}
