using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
    public class RequestModel
    {
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Address { get; set; } = null!;
		public string Email { get; set; } = null!;	
		public string NIF { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string HouseNumber { get; set; } = null!;
		public string PostalCode { get; set; } = null!;
		public string RemainPostalCode { get; set; } = null!;
		public int LocalityId { get; set; }
		public int CityId { get; set; }
		public int CountryId { get; set; }
		public string AddressWaterMeter { get; set; } = null!;
		public string HouseNumberWaterMeter { get; set; } = null!;
		public string PostalCodeWaterMeter { get; set; } = null!;
		public string RemainPostalCodeWaterMeter { get; set; } = null!;
		public int LocalityWaterMeterId { get; set; }
		public int CityWaterMeterId { get; set; }
		public int CountryWaterMeterId { get; set; }
	}
}
