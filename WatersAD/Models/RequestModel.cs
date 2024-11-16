using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
    public class RequestModel
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string NIF { get; set; }
		public string PhoneNumber { get; set; }
		public string HouseNumber { get; set; }
		public string PostalCode { get; set; }
		public string RemainPostalCode { get; set; }
		public int LocalityId { get; set; }
		public int CityId { get; set; }
		public int CountryId { get; set; }
		public string AddressWaterMeter { get; set; }
		public string HouseNumberWaterMeter { get; set; }
		public string PostalCodeWaterMeter { get; set; }
		public string RemainPostalCodeWaterMeter { get; set; }
		public int LocalityWaterMeterId { get; set; }
		public int CityWaterMeterId { get; set; }
		public int CountryWaterMeterId { get; set; }
	}
}
