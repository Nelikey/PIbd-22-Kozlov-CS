﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    public class CreateOrderBindingModel
    {
        public int DressId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
