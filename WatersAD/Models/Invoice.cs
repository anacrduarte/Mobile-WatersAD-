using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class Invoice
	{
		public int id { get; set; }
		public DateTime invoiceDate { get; set; }
		public int clientId { get; set; }
		public bool issued { get; set; }
		public bool sent { get; set; }
		public double totalAmount { get; set; }
		public DateTime limitDate { get; set; }
	}
}
