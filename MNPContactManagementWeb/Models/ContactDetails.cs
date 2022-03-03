using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MNPContactManagementWeb.Models
{
    public class ContactDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "The format of Phone is XXX-XXX-XXXX")]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Last Date Contacted")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastDateContacted { get; set; } = DateTime.Now;
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string? Comments { get; set; }
    }
}
