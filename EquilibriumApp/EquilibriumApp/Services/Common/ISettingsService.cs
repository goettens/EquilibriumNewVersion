using EquilibriumApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquilibriumApp.Services.Common
{
    public interface ISettingsService
    {
        string AccessToken { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string DefaultAPIUrl { get; }
        string IdUserAtual { get; set; }
        bool LembrarMe { get; set; }
        int EnumCategorias { get; set; }
    }
}
