using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public class Purchase
{
    public int PurchaseId { get; set; }

    public int? ProductIdFk { get; set; }

    public int? ClientIdFk { get; set; }

    public int? TypePurchaseIdFk { get; set; }

    public DateOnly? DatePurchase { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Client? Client { get; set; }

    public virtual TypesPurchase? TypesPurchase { get; set; }
}