using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Domain.Commodities;

namespace DMS.Infrastructure.Repositories
{
	public class DmsDbContext : DbContext
	{
		public virtual IDbSet<Sort> Sorts { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Sort>().HasKey(o => o.Id);
		}
	}
}
