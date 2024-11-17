using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class InvoiceDetails
	{
		public Client Client { get; set; }
		public WaterMeter WaterMeter { get; set; }
		public Invoice Invoice { get; set; }
		public Consumption Consumption { get; set; }
		public WaterMeterService WaterMeterService { get; set; }
		public Tiers Tier { get; set; }
	}
}
