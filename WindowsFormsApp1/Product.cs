using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public enum Status
    {
        Recieved,
        Storage,
        Sold
    }
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Name { get; set; } 
        public Status Status { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
    }
}
