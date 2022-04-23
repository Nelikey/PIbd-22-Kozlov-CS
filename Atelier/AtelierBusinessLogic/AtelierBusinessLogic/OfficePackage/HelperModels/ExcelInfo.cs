﻿using AtelierContracts.ViewModels;
using System.Collections.Generic;

namespace AtelierBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportDressComponentViewModel> DressComponents { get; set; }
    }
}
