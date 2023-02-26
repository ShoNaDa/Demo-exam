using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public class TypesPurchase
{
    public int TypeId { get; set; }

    public int? CashTransfer { get; set; }

    public int? CreditNow { get; set; }
}