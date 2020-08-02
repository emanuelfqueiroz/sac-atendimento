using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace SAC.Models
{
    public class Cliente
    {
        [Key]
        [DisplayName("CPF/CNPJ")]
        [Required]
        public string Documento { get; set; }

        [RegularExpression("([0-9]+)")]
        public string Matricula { get; set; }
        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [Phone]
        [Required]
        public string Fone { get; set; }

        public bool IsCPF => Documento.Length <= 12;
    }
    
}
