using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class Supplyer
    {
        public Supplyer()
        {
            Decorations = new HashSet<Decoration>();
            Ingridients = new HashSet<Ingridient>();
        }

        public string NameSupplyer { get; set; } = null!;
        public string? SupplyerAddress { get; set; }
        public DateTime SupplyDeadline { get; set; }

        public virtual ICollection<Decoration> Decorations { get; set; }
        public virtual ICollection<Ingridient> Ingridients { get; set; }
    }
}
