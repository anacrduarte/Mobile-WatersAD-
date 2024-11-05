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

        [Display(Name = "Nº")]
        public byte TierNumber { get; set; }

        [Display(Name = "Preço")]
        public double TierPrice { get; set; }

        [Display(Name = "Nome")]
        public string TierName { get; set; } = null!;

        [Display(Name = "Limite ")]
        public double UpperLimit { get; set; }
    }
}
