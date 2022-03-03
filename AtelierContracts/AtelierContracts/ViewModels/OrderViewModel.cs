using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtelierContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int DressId { get; set; }

        [DisplayName("Изделие")]
        public string DressName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
