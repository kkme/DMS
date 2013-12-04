using System;
using DMS.Domain.Commodities;
using DMS.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DMS.UnitTest
{
	[TestClass]
	public class SortAppTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var sortApp = new SortApp(new BaseRepository<Sort>(new DmsDbContext()));
			var sort1 = new Sort { Name = "男装" };
			var sort2 = new Sort { Name = "上装", ParentId = 1 };
			var sort3 = new Sort { Name = "下装", ParentId = 1 };
			var sort4 = new Sort { Name = "女装" };
			sortApp.Add(sort1);
			sortApp.Add(sort2);
			sortApp.Add(sort3);
			sortApp.Add(sort4);

			sort4.Name = "女人装";
			sortApp.Modify(sort4);

			sortApp.Remove(sort4);
		}
	}
}
