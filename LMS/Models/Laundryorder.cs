using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Laundryorder
{
    public int Orderid { get; set; }

    public int? Customerid { get; set; }

    public DateOnly? Orderdate { get; set; }

    public DateOnly? Pickupdate { get; set; }

    public DateOnly? Deliverydate { get; set; }

    public string? Status { get; set; }

    public decimal? Totalamount { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
