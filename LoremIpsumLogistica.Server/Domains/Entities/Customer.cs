using LoremIpsumLogistica.Server.Infraestructure.Entities;
using LoremIpsumLogistica.Server.Utils.Enums;

namespace LoremIpsumLogistica.Server.Domains.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public EGender Gender { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}