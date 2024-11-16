using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
    public class City
    {
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public Country Country { get; set; } = null!;

		public int CountryId { get; set; }

		public ICollection<Locality>? Localities { get; set; }
	}
}
