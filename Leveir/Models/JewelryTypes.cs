using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leveir.Models
{
    public class JewelryTypes
    {
        public int JewelryTypeId { get; set; }
        public string JewelryName { get; set; }
        public string Description { get; set; }
        public string JewelryImgName { get; set; }
        public HttpPostedFileBase JewelryImg { get; set; }
        public DateTime Created_dt { get; set; }
    }
}