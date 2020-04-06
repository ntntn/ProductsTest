using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class StatusContext: DbContext
    {
        public DbSet<Status> Statuses { get; set; }
    }
}
