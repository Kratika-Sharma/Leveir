using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leveir.Models
{
	public class Metals
	{
        public int MetalId { get; set; }
        public int JewelryTypeId { get; set; }
        public int SettingStyleId { get; set; }
        public string MetalName { get; set; }
        public string JewelryName { get; set; }
        public string StyleName { get; set; }
        public decimal MetalPrice { get; set; }
        public string Description { get; set; }
        public int MetalQuantity { get; set; }
        public string MetalCarat { get; set; }
        public DateTime Created_dt { get; set; }
    }
}