using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Commodities
{
	public class SortApp
	{
		private readonly IRepository<Sort> _rep;
		private readonly SortService _ser;
		public SortApp(IRepository<Sort> repository)
		{
			_rep = repository;
			_ser = new SortService(_rep);
		}
		public void Add(Sort newer)
		{
			_ser.Add(newer);
			_rep.Submit();
		}

		public void Modify(Sort modified)
		{
			_ser.Modify(modified);
			_rep.Submit();
		}

		public void Remove(Sort removed)
		{
			_ser.Remove(removed);
			_rep.Submit();
		}
	}
}
