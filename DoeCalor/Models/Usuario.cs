using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoeCalor.Models
{
   public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlPhoto { get; set; }
        public bool Doador { get; set; }
        public bool Receptor { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsLogged { get; set; }
    }
}
