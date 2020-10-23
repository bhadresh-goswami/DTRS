using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRS.Models.DataTableModel
{
    public class DataTableModel
    {
        public List<string> DataCols { get; set; }
        public dynamic DataRows { get; set; }
        public string Header { get; set; }
        public string Link { get; set; }
    }
}