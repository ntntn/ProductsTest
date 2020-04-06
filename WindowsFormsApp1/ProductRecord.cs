using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ProductRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductRecordId { get; set; }

        public int ProductId { get; set; }
        public int StatusId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
    }
}
