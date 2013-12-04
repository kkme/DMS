using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DMS.Domain.Commodities
{
	public interface IRepository<TEntity> where TEntity : class
	{
		TEntity GetById(int id);
		void Add(TEntity entity);
		void Save(TEntity entity);
		void Remove(TEntity entity);
		void Remove(int id);

		IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
		IEnumerable<TEntity> Query(Expression<Func<TEntity, TEntity>> selector, Expression<Func<TEntity, bool>> predicate);

		TEntity Single(Expression<Func<TEntity, bool>> predicate);
		TEntity Single(Expression<Func<TEntity, TEntity>> selector, Expression<Func<TEntity, bool>> predicate);

		bool Exist(Expression<Func<TEntity, bool>> predicate);

		void Submit();
		void Rollback();
	}
}
