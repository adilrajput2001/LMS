using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Complaint
{
    public int Complaintid { get; set; }

    public int? Customerid { get; set; }

    public int? Orderid { get; set; }

    public string? Description { get; set; }

    public DateOnly? Complaintdate { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Laundryorder? Order { get; set; }
}
