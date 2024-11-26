using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
    public class Tiers
    {
        public int Id { get; set; }

      
        public byte TierNumber { get; set; }

   
        public double TierPrice { get; set; }


        public string TierName { get; set; } = null!;

    
        public double UpperLimit { get; set; }
    }
}
