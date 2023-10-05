using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class Equipment
    {
        public string Marking { get; set; } = null!;
        public string TypeEquip { get; set; } = null!;
        public string? EquipCharacteristic { get; set; }

        public virtual TypeEquipment TypeEquipNavigation { get; set; } = null!;
    }
}
