using System.ComponentModel.DataAnnotations;

namespace LoremIpsumLogistica.Server.Infraestructure.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}