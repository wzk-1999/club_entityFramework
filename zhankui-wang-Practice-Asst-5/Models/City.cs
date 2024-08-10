using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace zhankui_wang_Practice_Asst_5.Models;

public partial class City
{
    public int Id { get; set; }
    [Key]
    public string Name { get; set; } = null!;

    public string ProvCode { get; set; } = null!;

    public virtual Province ProvCodeNavigation { get; set; } = null!;
    public virtual ICollection<BridgeClub> Clubs { get; set; } = new List<BridgeClub>();


    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
