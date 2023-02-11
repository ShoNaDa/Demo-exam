using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string Fio { get; set; } = null!;

    public string Passport { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
