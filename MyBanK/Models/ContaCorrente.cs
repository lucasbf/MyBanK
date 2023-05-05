using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBanK.Models
{
    [Table("ContasCorrentes")]
    public class ContaCorrente
    {
        [Key]
        [Display(Name = "Número da Conta")]
        public int Id { get; set; }
        public string? Titular { get; set; }
        [Display(Name = "Data da Abertura")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DataAbertura { get; set; }
        public ICollection<Movimento> Movimentos { get; set; }
        [NotMapped]
        public double Saldo
        {
            get { return Movimentos == null ? 0 : Movimentos.Sum(mv => mv.Valor); }
        }
    }
}
