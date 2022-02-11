using System.ComponentModel.DataAnnotations;

namespace Online_Book_Shop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
