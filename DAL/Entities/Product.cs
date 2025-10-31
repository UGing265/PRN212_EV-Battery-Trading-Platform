﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public partial class Product
    {
        public int Id { get; set; }

        public string? Type { get; set; }

        public string? Status { get; set; }

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? CreatedBy { get; set; }

        public string? Color { get; set; }

        public string? Dimension { get; set; }
    }
}
