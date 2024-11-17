using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class WaterMeterService
	{
		public int id { get; set; }
		public string serialNumber { get; set; }
		public int quantity { get; set; }
		public bool available { get; set; }
	}
}
