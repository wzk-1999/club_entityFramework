using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace zhankui_wang_Practice_Asst_5.Models;
public class BridgeClub
{
    [Key]
    public int ClubID { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Club Name")]
    public string ClubName { get; set; } = null!;

    [Required]
    [StringLength(30)]
    [Display(Name = "City Name")]
    public string CityName { get; set; } = null!;

    [ForeignKey("CityName")]
    public virtual City City { get; set; } = null!;

    [NotMapped]
    [Display(Name = "Members Body Size")]
    public int MembersBodySize => Members.Count;

    public virtual ICollection<User> Members { get; set; } = new List<User>();
}
