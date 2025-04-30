using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Laundryorder> Laundryorders { get; set; } = new List<Laundryorder>();
}
