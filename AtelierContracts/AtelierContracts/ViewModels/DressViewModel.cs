using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtelierContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в ателье
    /// </summary>
    public class DressViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название изделия")]
        public string DressName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> DressComponents { get; set; }
    }
}
