using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ProductRecord> ProductRecords { get; set; }

        public static int GetStatusIdByEnum(StatusType statusType)
        {
            switch(statusType)
            {
                case StatusType.Recieved:
                    return 1;
                case StatusType.Storage:
                    return 2;
                case StatusType.Sold:
                    return 3;
                default:
                    return 1;
            }
        }
    }
}
