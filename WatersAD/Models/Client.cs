using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class Client
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Address { get; set; } = null!;
		public string HouseNumber { get; set; } = null!;
		public string Email { get; set; } = null!;
		public object User { get; set; } = null!;
		public string Nif { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public int LocalityId { get; set; }
		public Locality? Locality { get; set; } 
		public string PostalCode { get; set; } = null!;
		public string RemainPostalCode { get; set; } = null!;
		public int NumberWaterMeters { get; set; }
		public bool IsActive { get; set; }
		public string FullName { get; set; } = null!;
		public string FullPostalCode { get; set; } = null!;
		public string FullAdress { get; set; } = null!;
		public object OldEmail { get; set; } = null!;

	}
}
