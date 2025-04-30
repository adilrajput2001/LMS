using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Service
{
    public int Serviceid { get; set; }

    public string? Name { get; set; }

    public decimal? Rateperkg { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
