﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Phone_Market.Models;

public partial class OrderedItem
{
    public int OrderId { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public virtual Receipt Order { get; set; }

    public virtual Product Product { get; set; }
}