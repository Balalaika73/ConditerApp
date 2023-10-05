using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class Product
    {
        public Product()
        {
            CakeDecorationSpecifications = new HashSet<CakeDecorationSpecification>();
            IngridientsSpecifications = new HashSet<IngridientsSpecification>();
            OperationSpecifications = new HashSet<OperationSpecification>();
            SemimanufacturesSpecifications = new HashSet<SemimanufacturesSpecification>();
        }

        public string NameProduct { get; set; } = null!;
        public string ProdictSize { get; set; } = null!;

        public virtual ICollection<CakeDecorationSpecification> CakeDecorationSpecifications { get; set; }
        public virtual ICollection<IngridientsSpecification> IngridientsSpecifications { get; set; }
        public virtual ICollection<OperationSpecification> OperationSpecifications { get; set; }
        public virtual ICollection<SemimanufacturesSpecification> SemimanufacturesSpecifications { get; set; }
    }
}
