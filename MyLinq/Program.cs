using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
	public class Customers
	{
		public string CustomerID;
		public string ContactName;
		public string Phone;
		public string City;
		public string Country;
	}

	public class Orders
	{
		public int OrderID;
		public string CustomerID;
		public DateTime OrderDate;
	}

	public class Northwind
	{
		public Query<Customers> Customers;
		public Query<Orders> Orders;

		public Northwind(DbConnection connection)
		{
			QueryProvider provider = new DbQueryProvider(connection);
			this.Customers = new Query<Customers>(provider);
			this.Orders = new Query<Orders>(provider);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			LinqTest();
			return;
			string constr = @"Data Source=.;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
			using (SqlConnection con = new SqlConnection(constr))
			{
				con.Open();
				Northwind db = new Northwind(con);

				string city = "London";
				//var query = db.Customers.Where(c => c.City == city);
				//var query = db.Customers.Where(c => c.City == city).Select(c => new { Name = c.ContactName, Phone = c.Phone });
				var query = db.Customers.Select(c => new
				{
					Name = c.ContactName,
					Location = new
					{
						City = c.City,
						Country = c.Country
					}
				}).Where(x => x.Location.City == city);

				Console.WriteLine("Query:\n{0}\n", query);

				var list = query.ToList();
				foreach (var item in list)
				{
					Console.WriteLine("Name: {0}", item);
				}

				Console.ReadLine();
			}
		}

		static void LinqTest()
		{
			var group = typeof(string).GetMembers().GroupBy(o => o.MemberType);
			foreach (var g in group)
			{
				Console.WriteLine(g.Key);
				foreach (var gg in g)
				{
					Console.WriteLine("\t" + gg.Name);
				}
			}
			//--------------------
			List<Package> packages = new List<Package> { new Package { Company = "Coho Vineyard", Weight = 25.2, TrackingNumber = 89453312L },
                                                 new Package { Company = "Lucerne Publishing", Weight = 18.7, TrackingNumber = 89112755L },
                                                 new Package { Company = "Wingtip Toys", Weight = 6.0, TrackingNumber = 299456122L },
                                                 new Package { Company = "Contoso Pharmaceuticals", Weight = 9.3, TrackingNumber = 670053128L },
                                                 new Package { Company = "Wide World Importers", Weight = 33.8, TrackingNumber = 4665518773L } };

			ILookup<char, string> packageLookup = packages.ToLookup(
				p => Convert.ToChar(p.Company.Substring(0, 1)),
				p => p.Company + " " + p.TrackingNumber
				);

			foreach (var packageGroup in packageLookup)
			{
				Console.WriteLine(packageGroup.Key);
				foreach (string str in packageGroup)
					Console.WriteLine("    {0}", str);
			}
			//----------------------------
			string[] fruits = { "apricot", "orange", "banana", "mango", "apple", "grape", "strawberry" };

			IOrderedEnumerable<string> sortedFruits1 =
				fruits.OrderBy(fruit => fruit.Length).ThenBy(fruit => fruit);

			foreach (string fruit in sortedFruits1)
				Console.WriteLine(fruit);

		}
	}
	class Package
	{
		public string Company { get; set; }
		public double Weight { get; set; }
		public long TrackingNumber { get; set; }
	}
}
