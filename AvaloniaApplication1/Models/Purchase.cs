using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int ProductIdFk { get; set; }

    public int ClientIdFk { get; set; }

    public DateOnly DatePurchase { get; set; }

    public int TypePurchaseIdFk { get; set; }

    public virtual Client ClientIdFkNavigation { get; set; } = null!;

    public virtual Product ProductIdFkNavigation { get; set; } = null!;

    public virtual TypesPurchase TypePurchaseIdFkNavigation { get; set; } = null!;
}
