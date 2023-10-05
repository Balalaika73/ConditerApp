using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class CakeDecorationSpecification
    {
        public string NameProductDec { get; set; } = null!;
        public string CodeDec { get; set; } = null!;
        public double DecSpQuantity { get; set; }

        public virtual Decoration CodeDecNavigation { get; set; } = null!;
        public virtual Product NameProductDecNavigation { get; set; } = null!;
    }
}
