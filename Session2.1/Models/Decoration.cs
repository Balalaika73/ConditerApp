using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class Decoration
    {
        public Decoration()
        {
            CakeDecorationSpecifications = new HashSet<CakeDecorationSpecification>();
        }

        public string DecVendorCode { get; set; } = null!;
        public string NameDec { get; set; } = null!;
        public string DecUnit { get; set; } = null!;
        public double DecQuantity { get; set; }
        public string? MainSupplyer { get; set; }
        public string? DecPicture { get; set; }
        public string TypeDec { get; set; } = null!;
        public double? DecPurchasePrice { get; set; }
        public string? DecWeight { get; set; }

        public virtual Supplyer? MainSupplyerNavigation { get; set; }
        public virtual ICollection<CakeDecorationSpecification> CakeDecorationSpecifications { get; set; }
    }
}
