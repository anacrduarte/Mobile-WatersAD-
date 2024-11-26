namespace WatersAD.Models
{
	public class WaterMeter
	{

		public int Id { get; set; }
		public string Address { get; set; } = null!;
		public string HouseNumber { get; set; } = null!;
		public string PostalCode { get; set; } = null!;
		public string RemainPostalCode { get; set; } = null!;
		public int LocalityId { get; set; }
		public Locality? Locality { get; set; } 
		public bool IsActive { get; set; }
		public DateTime InstallationDate { get; set; }
		public int ClientId { get; set; }
		public Client? Client { get; set; }
		public int WaterMeterServiceId { get; set; }
		public WaterMeterService? WaterMeterService { get; set; }
		public string FullPostalCode { get; set; } = null!;
		public string FullAdress { get; set; } = null!;

	}
}
