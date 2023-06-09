﻿using Corp.ERP.Common.Domain.Contract.Models;

namespace Corp.ERP.Inventory.Domain.Models;

internal class Software: InventoryItem, IEntity
{
    public virtual DateTime ExpiryDate { get; set; }
}
