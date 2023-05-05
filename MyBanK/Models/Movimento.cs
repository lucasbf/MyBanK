using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBanK.Models
{
    [Table("Movimentos")]
    public class Movimento
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Descrição")]
        public string? Descr { get; set; }
        [Required]
        [Display(Name = "Data da Ocorrência")]
        [DataType(DataType.DateTime)]
        public DateTime? Ocorrencia { get; set; }
        [Required]
        public double Valor { get; set; }
        [ForeignKey(nameof(Movimento))]
        public int ContaCorrenteId { get; set; }
    }
}
