using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class TypeEquipment
    {
        public TypeEquipment()
        {
            Equipment = new HashSet<Equipment>();
            OperationSpecifications = new HashSet<OperationSpecification>();
        }

        public string NameTypeEquipment { get; set; } = null!;

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<OperationSpecification> OperationSpecifications { get; set; }
    }
}
