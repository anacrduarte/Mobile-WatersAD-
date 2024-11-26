using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class AddConsumption
	{
		public int Id { get; set; }

		public int ConsumptionId { get; set; }

		public Consumption? Consumption { get; set; }

		public int ClientId { get; set; }

		public Client? Client { get; set; }

		public int WaterMeterId { get; set; }


		public DateTime ConsumptionDate { get; set; }

		public DateTime RegistrationDate { get; set; }

		public double ConsumptionValue { get; set; }

	}
}
