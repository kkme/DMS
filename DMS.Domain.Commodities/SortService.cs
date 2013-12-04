using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Commodities
{
	public class SortService
	{
		private readonly IRepository<Sort> _rep;

		public SortService(IRepository<Sort> repository)
		{
			_rep = repository;
		}

		public void Add(Sort newer)
		{
			if (!Exist(newer))
			{
				_rep.Add(newer);
			}
		}

		public void Modify(Sort modified)
		{
			if (!Exist(modified))
			{
				_rep.Save(modified);
			}
		}

		public void Remove(Sort removed)
		{
			_rep.Remove(removed.Id);
		}

		private bool Exist(Sort current)
		{
			return _rep.Exist(o => o.ParentId == current.ParentId && o.Name == current.Name);
		}
	}
}
