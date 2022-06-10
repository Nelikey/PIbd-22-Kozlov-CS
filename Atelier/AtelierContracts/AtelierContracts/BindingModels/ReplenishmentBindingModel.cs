﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierContracts.BindingModels
{
    public class ReplenishmentBindingModel
    {
        public int WarehouseId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int Count { get; set; }
    }
}
