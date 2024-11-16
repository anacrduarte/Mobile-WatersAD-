namespace WatersAD.Models
{
	public class Locality
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public City City { get; set; } = null!;

		public int CityId { get; set; }

	}
}
