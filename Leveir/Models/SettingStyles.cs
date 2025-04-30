using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Leveir.Models
{
    public class SettingStyles
    {
        public int SettingStyleId { get; set; }
        public int JewelryTypeId { get; set; }
        public string JewelryName { get; set; }
        public string StyleName { get; set; }
        public string Description { get; set; }
        public string StyleImg { get; set; }
        public HttpPostedFileBase SettingStyleImg { get; set; }
        public int StyleQuantity { get; set; }
        public decimal StylePrice { get; set; }
        public DateTime Created_dt { get; set; }
    }
}