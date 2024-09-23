using LoremIpsumLogistica.Server.Utils.Enums;

namespace LoremIpsumLogistica.Server.Domains.DTOs.Response
{
    public class AddressResponse
    {
        public int Id { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string? Complement { get; set; }

        public string? Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public EAddressType AddressType { get; set; }
    }
}