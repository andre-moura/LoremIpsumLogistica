using LoremIpsumLogistica.Server.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace LoremIpsumLogistica.Server.Domains.DTOs.Request
{
    public class AddressRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Number { get; set; }

        public string? Complement { get; set; }

        public string? Neighborhood { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string City { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória.")]
        [StringLength(2, ErrorMessage = "A UF deve ter exatamente 2 caracteres.")]
        public string State { get; set; }

        [Required(ErrorMessage = "O tipo de endereço é obrigatório.")]
        public EAddressType AddressType { get; set; }
    }
}