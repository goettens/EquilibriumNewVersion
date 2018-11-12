using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    public class Atividades
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LongDescription { get; set; }
        public float MinimumPrice { get; set; }
        public float MaximumPrice { get; set; }
        public string Link { get; set; }
        public string ImageURL { get; set; }
        public int EnumCategories { get; set; }
        public int Likes { get; set; }
        public int ReportsCount { get; set; }
    }
}
