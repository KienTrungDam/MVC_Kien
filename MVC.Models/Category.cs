using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace MVC.Models
{
    public class Category
    {
        // [Validation]
        [Key] // priamry key
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")] //displayname ten hien thi neu goi ra
        [MaxLength(30)] // ErrorMessage = "":tuy chinh thong bao loi
        public string? Name { get; set; }

        [DisplayName("Category DisplayOrder")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")] //validation tu 1-100
        public int DesplayOrder { get; set; }
    }
}
