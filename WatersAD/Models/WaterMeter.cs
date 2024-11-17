namespace WatersAD.Models
{
	public class WaterMeter
	{

		public int id { get; set; }
		public string address { get; set; }
		public string houseNumber { get; set; }
		public string postalCode { get; set; }
		public string remainPostalCode { get; set; }
		public int localityId { get; set; }
		public Locality locality { get; set; }
		public bool isActive { get; set; }
		public DateTime installationDate { get; set; }
		public int clientId { get; set; }
		public Client client { get; set; }
		public int waterMeterServiceId { get; set; }
		public WaterMeterService waterMeterService { get; set; }
		public string fullPostalCode { get; set; }
		public string fullAdress { get; set; }
	}
}
