using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Delivery
{
    public int Deliveryid { get; set; }

    public int? Orderid { get; set; }

    public int? Staffid { get; set; }

    public DateTime? Deliverytime { get; set; }

    public string? Deliverystatus { get; set; }

    public virtual Laundryorder? Order { get; set; }

    public virtual Staff? Staff { get; set; }
}
