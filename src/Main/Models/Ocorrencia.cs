using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fase4Cap7WebserviceASPNET.Main.Models
{
    [Table("TBL_ESG_OCORRENCIA_AMBIENTAL")]
    public class Ocorrencia
    {
        [Key]
        [Column("ID_OCORRENCIA")]
        public int Id { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; } = string.Empty;

        [Column("DATA_OCORRENCIA")]
        public DateTime DataOcorrencia { get; set; }

        [Column("GTYPE")]
        public string Tipo { get; set; } = string.Empty;

        [Column("STATUS")]
        public string Status { get; set; } = "PENDENTE";

        [Column("TIPO_IMPACTO_ID")]

        public int TipoImpactoId { get; set; }

        public TipoImpacto? TipoImpacto { get; set; }
    }
}
