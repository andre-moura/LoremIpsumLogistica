using LoremIpsumLogistica.Server.Utils.Enums;

namespace LoremIpsumLogistica.Server.Domains.DTOs.Request
{
    public class CustomerRequest
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public EGender Gender { get; set; }

        public ICollection<AddressRequest>? Addresses { get; set; }
    }
}