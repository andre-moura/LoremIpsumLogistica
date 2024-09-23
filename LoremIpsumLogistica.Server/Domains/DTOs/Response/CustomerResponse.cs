using LoremIpsumLogistica.Server.Utils.Enums;

namespace LoremIpsumLogistica.Server.Domains.DTOs.Response
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public EGender Gender { get; set; }

        public ICollection<AddressResponse>? Addresses { get; set; }
    }
}