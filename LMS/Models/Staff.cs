using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Staff
{
    public int Staffid { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public string? Phone { get; set; }

    public DateOnly? Hiredate { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
