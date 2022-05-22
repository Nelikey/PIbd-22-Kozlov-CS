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
    /// Компонент, требуемый для изготовления платьев
    /// </summary>
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 100, visible: false)]
        public int Id { get; set; }
        [Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }
}
