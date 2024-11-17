using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class Consumption
	{
		public int id { get; set; }
		public int tierId { get; set; }
		public object tier { get; set; }
		public DateTime consumptionDate { get; set; }
		public DateTime registrationDate { get; set; }
		public int consumptionValue { get; set; }
		public int waterMeterId { get; set; }
		public WaterMeter waterMeter { get; set; }
		public Invoice invoice { get; set; }
	}
}
