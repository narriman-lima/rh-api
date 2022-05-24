using Microsoft.OpenApi.Extensions;
using rh_api.Enums;

namespace rh_api.Models
{
    public class FuncionarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public decimal Salario { get; set; }
        public PermissoesEnum Permissao { get; set; }
        public string PermissaoNome => Permissao.GetDisplayName();
    }
}
