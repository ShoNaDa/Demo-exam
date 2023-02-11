using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class TypesPurchase
{
    public int TypeId { get; set; }

    public short CashTransfer { get; set; }

    public short CreditNow { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
