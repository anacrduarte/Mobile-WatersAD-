﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class Consumption
	{
		public int Id { get; set; }
		public int TierId { get; set; }
		public Tiers Tier { get; set; } 
		public DateTime ConsumptionDate { get; set; }
		public DateTime RegistrationDate { get; set; }
		public int ConsumptionValue { get; set; }
		public int WaterMeterId { get; set; }
		public WaterMeter WaterMeter { get; set; } 
		public Invoice Invoice { get; set; } 

	}
}
