﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int EnumCategories { get; set; }
        public string ImageURL { get; set; }

        public User(string name, string email, string senha, int enumCategorias, string imageURL)
        {
            Name = name;
            Email = email;
            Senha = senha;
            EnumCategories = enumCategorias;
            ImageURL = imageURL;
        }
    }
}
