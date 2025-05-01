using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leveir.Models
{
    public class Diamonds
    {
        public int DiamondId { get; set; }
        public int DiamondTypeId { get; set; }
        public string DiamondTypeName { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Carat { get; set; }
        public string Cut { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public HttpPostedFileBase DiamondImg { get; set; }
        public string DiamondImgName { get; set; }
        public DateTime Created_dt { get; set; }
    }
}