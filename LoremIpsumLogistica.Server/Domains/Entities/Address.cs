using LoremIpsumLogistica.Server.Infraestructure.Entities;
using LoremIpsumLogistica.Server.Utils.Enums;

namespace LoremIpsumLogistica.Server.Domains.Entities
{
    public class Address : BaseEntity
    {
        public string ZipCode { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string? Complement { get; set; }

        public string? Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public EAddressType AddressType { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}