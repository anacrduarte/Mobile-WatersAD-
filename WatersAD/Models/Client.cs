using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class Client
	{
		public int id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string address { get; set; }
		public string houseNumber { get; set; }
		public string email { get; set; }
		public object user { get; set; }
		public string nif { get; set; }
		public string phoneNumber { get; set; }
		public int localityId { get; set; }
		public object locality { get; set; }
		public string postalCode { get; set; }
		public string remainPostalCode { get; set; }
		public int numberWaterMeters { get; set; }
		public bool isActive { get; set; }
		public string fullName { get; set; }
		public string fullPostalCode { get; set; }
		public string fullAdress { get; set; }
		public object oldEmail { get; set; }
	}
}
