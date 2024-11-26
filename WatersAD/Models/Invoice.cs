using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Models
{
	public class Invoice
	{
		public int Id { get; set; }
		public DateTime InvoiceDate { get; set; }
		public int ClientId { get; set; }
		public bool Issued { get; set; }
		public bool Sent { get; set; }
		public double TotalAmount { get; set; }
		public DateTime LimitDate { get; set; }

	}
}
