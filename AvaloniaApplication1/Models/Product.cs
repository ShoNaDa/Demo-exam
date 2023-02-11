using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ManufCountry { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int Count { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
