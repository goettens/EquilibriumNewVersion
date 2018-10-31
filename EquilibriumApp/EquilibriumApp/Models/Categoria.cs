﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    public class Categoria 
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Subcategories> Subs { get; set; }
    }
}
