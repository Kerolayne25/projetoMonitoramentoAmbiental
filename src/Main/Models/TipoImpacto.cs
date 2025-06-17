using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fase4Cap7WebserviceASPNET.Main.Models
{
    [Table("TBL_ESG_TIPO_IMPACTO")]
    public class TipoImpacto
    {
        [Key]
        [Column("ID_TIPO_IMPACTO")]
        public int Id { get; set; }

        [Column("NOME")]
        public string Nome { get; set; } = string.Empty;

        [Column("DESCRICAO")]
        public string Descricao { get; set; } = string.Empty;

        [Column("ATIVO")]
        public int Ativo { get; set; }
    }
}
