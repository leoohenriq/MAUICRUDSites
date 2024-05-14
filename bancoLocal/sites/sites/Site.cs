using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace sites
{
    [Table("Sites")]
    public class Site
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250), Unique]
        public string endereco { get; set; }
    }
}
