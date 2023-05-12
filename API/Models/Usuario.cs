using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Senha { get; set; }
        public string? CPF { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
    }
}
