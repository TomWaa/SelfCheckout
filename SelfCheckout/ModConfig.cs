using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfCheckout
{
    internal class ModConfig
    {
        public bool Pierre { get; set; }
        public bool Ranch { get; set; }
        public bool Carpenter { get; set; }
        public bool FishShop { get; set; }
        public bool Blacksmith { get; set; }
        public bool IceCreamStand { get; set; }
        public bool IceCreamInAllSeasons { get; set; }
        public bool ShopsAlwaysOpen { get; set; }

        public ModConfig()
        {
            Pierre = true;
            Ranch = true;
            Carpenter = true;
            FishShop = true;
            Blacksmith = true;
            IceCreamStand = true;
            IceCreamInAllSeasons = true;
            ShopsAlwaysOpen = true;
        }
    }
}
