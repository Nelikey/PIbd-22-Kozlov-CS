using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AtelierContracts.Enums;

namespace AtelierDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int DressId { get; set; }

        public int ClientId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? ImplementDate { get; set; }

        public virtual Dress Dress { get; set; }

        public virtual Client Client { get; set; }
    }
}