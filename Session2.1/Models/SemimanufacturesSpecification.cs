using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class SemimanufacturesSpecification
    {
        public string NameProductIngr { get; set; } = null!;
        public string CodeIngr { get; set; } = null!;
        public double IngrSpQuantity { get; set; }

        public virtual Ingridient CodeIngrNavigation { get; set; } = null!;
        public virtual Product NameProductIngrNavigation { get; set; } = null!;
    }
}
