using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TruckPortal.Models
{
    public class TruckViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Model")]
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        [Display(Name = "Delivery Year")]
        public int DeliveryYear { get; set; }

        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }
    }
}
