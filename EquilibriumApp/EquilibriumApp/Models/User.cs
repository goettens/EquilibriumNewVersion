using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public int IdCidade { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool IsOnline { get; set; }
        public string Token { get; set; }
        public int EnumCategorias { get; set; }
    }
}
