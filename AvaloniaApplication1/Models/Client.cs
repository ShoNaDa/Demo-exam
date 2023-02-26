using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public class Client
{
    public int ClientId { get; set; }

    public string? Fio { get; set; }

    public string? Address { get; set; }

    public string? Passport { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}