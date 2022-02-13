using System.ComponentModel.DataAnnotations;

namespace Online_Book_Shop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,100, ErrorMessage = "Display order must be between 1 and 100")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
