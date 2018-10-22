using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    class Comentario
    {
        public int IdAtividade { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
