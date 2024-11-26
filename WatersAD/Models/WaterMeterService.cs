using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class WaterMeterService
	{
		public int Id { get; set; }
		public string SerialNumber { get; set; } = null!;
		public int Quantity { get; set; }
		public bool Available { get; set; }

	}
}
