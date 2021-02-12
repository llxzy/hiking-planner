using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DataClasses
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
