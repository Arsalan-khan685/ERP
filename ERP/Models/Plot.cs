﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Plot
    {
        public int PlotId { get; set; }
        public string PlotNo { get; set; }
        public int PlotType_Id { get; set; }
        public int PlotSize_Id { get; set; }
        public int Street_Id { get; set; }
    }
}
