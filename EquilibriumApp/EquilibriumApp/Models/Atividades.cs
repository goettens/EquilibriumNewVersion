using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Models
{
    class Atividades
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoLonga { get; set; }
        public float PrecoMinimo { get; set; }
        public float PrecoMaximo { get; set; }
        public string Link { get; set; }
        public byte[] FotoCapa { get; set; }
        public int EnumCategorias { get; set; }
        public int Likes { get; set; }
        public int IdCidade { get; set; }
        public int ReportsCount { get; set; }
    }
}
