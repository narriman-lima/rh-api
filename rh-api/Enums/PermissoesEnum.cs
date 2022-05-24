using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace rh_api.Enums
{
    public enum PermissoesEnum
    {
        [XmlEnum("Funcionario")]
        [Display(Name = "Funcionário")]
        Funcionario = 1,
        [XmlEnum("Gerente")]
        [Display(Name = "Gerente")]
        Gerente = 2,
        [XmlEnum("Administrador")]
        [Display(Name = "Administrador")]
        Administrador = 3,
    }
}
