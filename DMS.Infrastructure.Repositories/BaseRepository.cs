using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DMS.Domain.Commodities;

namespace DMS.Infrastructure.Repositories
{
	public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly DbContext _db;
		private readonly IDbSet<TEntity> _dbSet;

		public BaseRepository(DbContext dbContext)
		{
			_db = dbContext;
			_dbSet = _db.Set<TEntity>();
		}

		public TEntity GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public void Add(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public void Save(TEntity entity)
		{
			_db.Entry(entity);
		}

		public void Remove(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public void Remove(int id)
		{
			_dbSet.Remove(GetById(id));
		}


		public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
		{
			return Query(o => o, predicate);
		}

		public IEnumerable<TEntity> Query(Expression<Func<TEntity, TEntity>> selector, Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Select(selector).Where(predicate);
		}

		public TEntity Single(Expression<Func<TEntity, bool>> predicate)
		{
			return Single(o => o, predicate);
		}

		public TEntity Single(Expression<Func<TEntity, TEntity>> selector, Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Select(selector).Where(predicate).Single();
		}


		public void Submit()
		{
			_db.SaveChanges();
		}

		public void Rollback()
		{
		}


		public bool Exist(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Any(predicate);
		}
	}
}
