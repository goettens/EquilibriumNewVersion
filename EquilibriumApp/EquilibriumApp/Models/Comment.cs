using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    class Comment
    {
        public string idComment { get; set; }
        public string idRecommendedActivity { get; set; }
        public string idUser { get; set; }
        public string imageURL { get; set; }
        public string userName { get; set; }
        public string description { get; set; }
    }
}
