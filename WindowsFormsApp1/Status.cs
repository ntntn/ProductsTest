﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }
        public string Name { get; set; }
    }
}
