using System.Collections.Generic;
using AtelierContracts.ViewModels;

namespace AtelierBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<DressViewModel> Dresses { get; set; }
    }
}
