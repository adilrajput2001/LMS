using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Inventory
{
    public int Itemid { get; set; }

    public string? Itemname { get; set; }

    public int? Quantity { get; set; }

    public string? Unit { get; set; }

    public int? Reorderlevel { get; set; }
}
