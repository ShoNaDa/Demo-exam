using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public class Product
{
    public int ProductId { get; set; }

    public string? ManufCountry { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Color { get; set; }

    public int? Count { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}