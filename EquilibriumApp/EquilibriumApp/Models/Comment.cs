using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    public class Comment
    {
        public string IdComment { get; set; }
        public string IdRecommendedActivity { get; set; }
        public string IdUser { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
    }
}
