using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Orderdetail
{
    public int Orderdetailid { get; set; }

    public int? Orderid { get; set; }

    public int? Serviceid { get; set; }

    public decimal? Weightinkg { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Laundryorder? Order { get; set; }

    public virtual Service? Service { get; set; }
}
