using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace How_Much_App
{
    class Data
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int DataID { get; set; }
        public string DataName { get; set; }
        public string DataPrice { get; set; }
        public DateTime DateTime { get; set; }
    }
}
