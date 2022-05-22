using AtelierContracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtelierContracts.ViewModels
{
    /// <summary>
    /// Платье, изготавливаемое в магазине
    /// </summary>
    public class DressViewModel
    {
        [Column(title: "Номер", width: 100, visible: false)]
        public int Id { get; set; }
        [Column(title: "Название платьев", width: 150)]
        public string DressName { get; set; }
        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }
        [Column(title: "Компоненты", gridViewAutoSize: GridViewAutoSize.Fill)]
        public Dictionary<int, (string, int)> DressComponents { get; set; }
        public string GetComponents()
        {
            string stringComponents = string.Empty;
            if (DressComponents != null)
            {
                foreach (var ingr in DressComponents)
                {
                    stringComponents += ingr.Key + ") " + ingr.Value.Item1 + ": " + ingr.Value.Item2 + ", ";
                }
            }
            return stringComponents;
        }
    }
}
