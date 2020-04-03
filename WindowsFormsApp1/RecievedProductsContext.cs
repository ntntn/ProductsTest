using System;
using System.Data.Entity;

namespace WindowsFormsApp1
{
    internal class RecievedProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}