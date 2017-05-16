using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba12
{
    class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("Laba12")
        {

        }
        public DbSet<Students> setStudents { get; set; }
        public DbSet<Subjects> setSubjectses { get; set; }

    }
}
