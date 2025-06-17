using System.ComponentModel.DataAnnotations;

namespace Fase4Cap7WebserviceASPNET.Main.ViewModels
{
    public class OcorrenciaViewModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data da ocorrência é obrigatória.")]
        public DateTime DataOcorrencia { get; set; }

        [Required(ErrorMessage = "O tipo (GTYPE) é obrigatório.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ID do tipo de impacto é obrigatório.")]
        public int TipoImpactoId { get; set; }
    }
}
