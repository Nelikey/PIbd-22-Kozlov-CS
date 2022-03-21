﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierContracts.BindingModels
{
    /// <summary>
    /// Изделие, изготавливаемое в ателье
    /// </summary
    public class DressBindingModel
    {
        public int? Id { get; set; }

        public string DressName { get; set; }

        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> DressComponents { get; set; }
    }
}
