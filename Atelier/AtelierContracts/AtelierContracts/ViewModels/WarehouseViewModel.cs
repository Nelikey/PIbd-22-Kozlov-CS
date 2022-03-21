using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtelierContracts.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Менеджер")]
        public string Manager { get; set; }

        [DisplayName("Дата создания")]
        public DateTime CreationDate { get; set; }

        public Dictionary<int, (string, int)> StoredComponents { get; set; }
    }
}
