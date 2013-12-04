using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Commodities
{
	public class Sort
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ParentId { get; set; }
	}
}
