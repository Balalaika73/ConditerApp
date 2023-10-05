using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class IngridientsSpecification
    {
        public string IngrCode { get; set; } = null!;
        public string NameProductSp { get; set; } = null!;
        public double IngrSpQuantity { get; set; }

        public virtual Ingridient IngrCodeNavigation { get; set; } = null!;
        public virtual Product NameProductSpNavigation { get; set; } = null!;
    }
}
