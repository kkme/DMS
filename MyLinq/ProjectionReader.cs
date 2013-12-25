using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
	internal class ProjectionReader<T> : IEnumerable<T>, IEnumerable
	{
		Enumerator enumerator;

		internal ProjectionReader(DbDataReader reader, Func<ProjectionRow, T> projector)
		{
			this.enumerator = new Enumerator(reader, projector);
		}

		public IEnumerator<T> GetEnumerator()
		{
			Enumerator e = this.enumerator;
			if (e == null)
			{
				throw new InvalidOperationException("Cannot enumerate more than once");
			}
			this.enumerator = null;
			return e;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		class Enumerator : ProjectionRow, IEnumerator<T>, IEnumerator, IDisposable
		{
			DbDataReader reader;
			T current;
			Func<ProjectionRow, T> projector;

			internal Enumerator(DbDataReader reader, Func<ProjectionRow, T> projector)
			{
				this.reader = reader;
				this.projector = projector;
			}

			public override object GetValue(int index)
			{
				if (index >= 0)
				{
					if (this.reader.IsDBNull(index))
					{
						return null;
					}
					else
					{
						return this.reader.GetValue(index);
					}
				}
				throw new IndexOutOfRangeException();
			}

			public T Current
			{
				get { return this.current; }
			}

			object IEnumerator.Current
			{
				get { return this.current; }
			}

			public bool MoveNext()
			{
				if (this.reader.Read())
				{
					this.current = this.projector(this);
					return true;
				}
				return false;
			}

			public void Reset()
			{
			}

			public void Dispose()
			{
				this.reader.Dispose();
			}
		}
	}
}
