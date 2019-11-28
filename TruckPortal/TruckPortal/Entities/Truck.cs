using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckPortal.Entities
{
    public enum Model
    {
        FH = 1,
        FM = 2
    }

    public class Truck
    {
        public int Id { get; set; }

        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public int DeliveryYear { get; set; }
        public int ModelYear { get; set; }
    }
}
