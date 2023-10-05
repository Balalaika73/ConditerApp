using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class Ingridient
    {
        public Ingridient()
        {
            IngridientsSpecifications = new HashSet<IngridientsSpecification>();
            SemimanufacturesSpecifications = new HashSet<SemimanufacturesSpecification>();
        }

        public string IngrVendorCode { get; set; } = null!;
        public string NameIngr { get; set; } = null!;
        public string? IngrUnit { get; set; }
        public double? IngrQuantity { get; set; }
        public string? MainSupplyer { get; set; }
        public string? IngrPicture { get; set; }
        public string? TypeIngr { get; set; }
        public double? IngrPurchasePrice { get; set; }
        public string? Gost { get; set; }
        public string? IngrPackaging { get; set; }
        public string? IngrCharacteristic { get; set; }

        public virtual Supplyer? MainSupplyerNavigation { get; set; }
        public virtual ICollection<IngridientsSpecification> IngridientsSpecifications { get; set; }
        public virtual ICollection<SemimanufacturesSpecification> SemimanufacturesSpecifications { get; set; }
    }
}
