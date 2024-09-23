using LoremIpsumLogistica.Server.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace LoremIpsumLogistica.Server.Domains.DTOs.Request
{
    public class CustomerRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public EGender Gender { get; set; }

        public ICollection<AddressRequest>? Addresses { get; set; }
    }
}