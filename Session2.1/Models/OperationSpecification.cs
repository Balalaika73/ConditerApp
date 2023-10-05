using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class OperationSpecification
    {
        public string SpOpProduct { get; set; } = null!;
        public string NameOperation { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string? TypeEquip { get; set; }
        public DateTime TimeForOperation { get; set; }

        public virtual Product SpOpProductNavigation { get; set; } = null!;
        public virtual TypeEquipment? TypeEquipNavigation { get; set; }
    }
}
