using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Payment
{
    public int Paymentid { get; set; }

    public int? Orderid { get; set; }

    public decimal? Amountpaid { get; set; }

    public DateOnly? Paymentdate { get; set; }

    public string? Paymentmethod { get; set; }

    public virtual Laundryorder? Order { get; set; }
}
