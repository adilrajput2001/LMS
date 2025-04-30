using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Feedback
{
    public int Feedbackid { get; set; }

    public int? Customerid { get; set; }

    public int? Orderid { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateOnly? Feedbackdate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Laundryorder? Order { get; set; }
}
