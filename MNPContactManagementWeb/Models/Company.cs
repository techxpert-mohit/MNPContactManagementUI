using System.ComponentModel.DataAnnotations;
namespace MNPContactManagementWeb.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
