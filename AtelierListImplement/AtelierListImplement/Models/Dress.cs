using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierListImplement.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в ателье
    /// </summary>
    public class Dress
    {
        public int Id { get; set; }

        public string DressName { get; set; }

        public decimal Price { get; set; }

        public Dictionary<int, int> DressComponents { get; set; }
    }
}
