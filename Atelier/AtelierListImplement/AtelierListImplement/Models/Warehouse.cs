using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierListImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public DateTime CreationDate { get; set; }
        public Dictionary<int, int> StoredComponents { get; set; }
    }
}
