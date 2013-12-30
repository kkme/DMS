using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSql
{
	class Program
	{
		static void Main(string[] args)
		{
			var qp = new QueryProvider();
			var q = new Query<string>(qp);

			var result = q.Where(o => 1 == 1);
			foreach (var r in result)
			{
				Console.WriteLine(r);
			}
		}
	}
	class Query<T> : IQueryable<T>
	{
		public IQueryProvider Provider
		{
			get;
			private set;
		}
		public Expression Expression
		{
			get;
			private set;
		}
		public Type ElementType
		{
			get { return typeof(T); }
		}

		public Query(IQueryProvider provider)
		{
			Provider = provider;
			Expression = Expression.Constant(this);
		}

		public Query(IQueryProvider provider, Expression express)
		{
			Provider = provider;
			Expression = express;
		}
		public IEnumerator<T> GetEnumerator()
		{
			return Provider.Execute<T>(Expression) as IEnumerator<T>;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}


	class QueryProvider : IQueryProvider
	{
		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			return new Query<TElement>(this, expression);
		}

		public IQueryable CreateQuery(Expression expression)
		{
			throw new NotImplementedException();
		}

		public TResult Execute<TResult>(Expression expression)
		{
			var l = Expression.Lambda(expression).Compile();
			return (TResult)l.DynamicInvoke();
		}

		public object Execute(Expression expression)
		{
			throw new NotImplementedException();
		}
	}


}
