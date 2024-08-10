using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using zhankui_wang_Practice_Asst_5.Utilities;

namespace zhankui_wang_Practice_Asst_5.Models
{
    public partial class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Residential Address")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "City Name is required.")]
        [Display(Name = "City Name")]
        public string CityName { get; set; } = null!;

        [Required(ErrorMessage = "Postal Code is required.")]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[ ]*[A-Za-z]\d[A-Za-z][ ]?\d[A-Za-z]\d[ ]*$",
            ErrorMessage = "Invalid Postal Code format. Expected format is A0A0A0.")]
        public string PostalCode { get; set; } = null!;
      
        public virtual City CityNameNavigation { get; set; } = null!;

        public string Province
        {
            get
            {
                return CRUDutilities.ProvinceOfCity(CityName);
            }
        }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return $"{Address}, {CityName}, {Province}, {PostalCode}";
            }
        }

        public virtual ICollection<BridgeClub> Clubs { get; set; } = new List<BridgeClub>();
    }
}
