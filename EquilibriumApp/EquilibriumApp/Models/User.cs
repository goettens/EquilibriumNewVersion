using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    public class User
    {
        public string Email { get; set; }
        public int EnumCategories { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }

        public User(string email, int enumCategorias, string imageURL, string name)
        {
            Name = name;
            Email = email;
            EnumCategories = enumCategorias;
            ImageURL = imageURL;
        }
        public User()
        {

        }
    }
}
